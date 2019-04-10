/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-23
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;
using IBP.Models;

namespace IBP.Services
{
	/// <summary>
	/// VoteProjectItemInfo业务逻辑类
	/// </summary>
	public partial class VoteProjectItemInfoService : BaseService
	{
		// 在此添加你的代码...

        public List<VoteProjectItemInfoModel> GetVoteItemListByProjectId(string projectId)
        {
            string sql = "select * from [vote_project_item_info] where vote_project_id = $projectId$ order by sort_order asc";
            List<VoteProjectItemInfoModel> list = null;
            VoteProjectItemInfoModel mod = null;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("projectId", projectId);

            return ModelConvertFrom<VoteProjectItemInfoModel>(ExecuteDataTable(sql, pc));
        }

        public RelUserVoteitemModel GetUserVoteInfo(string voteProjectId, string voteItemId, string voteUserId)
        {
            return null;
        }

        public Dictionary<string, RelUserVoteitemModel> GetUserVoteList(string voteProjectId, string voteUserId)
        {
            Dictionary<string, RelUserVoteitemModel> dict = null;
            if (string.IsNullOrEmpty(voteUserId))
            {
                return null;
            }

            ParameterCollection pc = new ParameterCollection();
            pc.Add("vote_project_id", voteProjectId);
            pc.Add("created_by", voteUserId);

            List<RelUserVoteitemModel> list = RelUserVoteitemService.Instance.RetrieveMultiple(pc);
            dict = new Dictionary<string, RelUserVoteitemModel>();
            foreach (RelUserVoteitemModel item in list)
            {
                dict[item.VoteItemId] = item;
            }

            return dict;
        }

        public bool ProjectItemVote(RelUserVoteitemModel voteInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (voteInfo == null)
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            if (voteInfo.VoteProjectId == null || voteInfo.VoteItemId == null || voteInfo.Score == null)
            {
                message = "参数错误，未能正确获取投票项目ID, 请与管理员联系";
                return false;
            }

            VoteProjectInfoModel projectInfo = VoteProjectInfoService.Instance.Retrieve(voteInfo.VoteProjectId);
            if (projectInfo == null)
            {
                message = "未能正确获取投票项目信息，请与管理员联系";
                return false;
            }

            if (projectInfo.CanAnonymous == 1)
            {
                if (SessionUtil.Current == null || SessionUtil.Current.IsLogin == false)
                {
                    message = "本投票项目设置为不能进行匿名投票，请登录系统";
                    return false;
                }
            }

            VoteProjectItemInfoModel itemInfo = Retrieve(voteInfo.VoteItemId);
            if (itemInfo == null)
            {
                message = "未能正确获取投票栏目信息，请与管理员联系";
                return false;
            }

           

            string sql = "select * from rel_user_voteitem where [USER_ID]= $userId$ and vote_item_id = $voteId$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("userId", SessionUtil.Current.UserId);
            pc.Add("voteId", voteInfo.VoteItemId);

            List<RelUserVoteitemModel> userVoteList = ModelConvertFrom<RelUserVoteitemModel>(ExecuteDataTable(sql, pc));
            if (projectInfo.CanMuiltVote == 1 && projectInfo.CanModifyVote == 1 && userVoteList != null && userVoteList.Count > 0)
            {
                message = "操作中止，本项目设置不可重复投票评分";
                return false;
            }

            try
            {
                BeginTransaction();

                voteInfo.UserVoteId = GetGuid();
                voteInfo.UserId = SessionUtil.Current.UserId;

                // 如果可以重复投票
                if (projectInfo.CanMuiltVote == 0)
                {
                    if (RelUserVoteitemService.Instance.Create(voteInfo) != 1)
                    {
                        RollbackTransaction();
                        message = "保存用户投票信息失败，请与管理员联系";
                        return false;
                    }

                    itemInfo.VoteTotal++;
                    itemInfo.VoteScore += Convert.ToInt32(voteInfo.Score);

                    if (Update(itemInfo) != 1)
                    {
                        RollbackTransaction();
                        message = "更新投票栏目信息失败，请与管理员联系";
                        return false;
                    }
                }
                else
                {
                    if (userVoteList == null || userVoteList.Count == 0)
                    {
                        // 如果未还进行过投票
                        if (RelUserVoteitemService.Instance.Create(voteInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "保存用户投票信息失败，请与管理员联系";
                            return false;
                        }

                        itemInfo.VoteTotal++;
                        itemInfo.VoteScore += Convert.ToInt32(voteInfo.Score);

                        if (Update(itemInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "更新投票栏目信息失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        if (projectInfo.CanModifyVote == 0)
                        {
                            // 如果已经投过票，并允许修改
                            voteInfo.UserVoteId = userVoteList[0].UserVoteId;

                            if (RelUserVoteitemService.Instance.Update(voteInfo) != 1)
                            {
                                RollbackTransaction();
                                message = "更新用户投票信息失败，请与管理员联系";
                                return false;
                            }

                            itemInfo.VoteScore = itemInfo.VoteScore - Convert.ToInt32(userVoteList[0].Score);
                            itemInfo.VoteScore = itemInfo.VoteScore + Convert.ToInt32(voteInfo.Score);

                            if (Update(itemInfo) != 1)
                            {
                                RollbackTransaction();
                                message = "更新投票栏目信息失败，请与管理员联系";
                                return false;
                            }
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "操作中止，栏目设置不可修改投票评分";
                            return false;
                        }
                    }
                }

                CommitTransaction();
                message = "操作成功";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                message = string.Format("提交投票评分信息异常! {0}。请与管理员联系", ex.Message);
                result = false;
            }

            return result;
        }
	}
}

