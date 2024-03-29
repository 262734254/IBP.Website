/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-2-7
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
	/// UserGroupInfo业务逻辑类
	/// </summary>
	public partial class UserGroupInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool DeleteUserGroupInfo(string userGroupId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, UserGroupInfoModel> groupList = GetUserGroupList(false);
            if (groupList.ContainsKey(userGroupId) == false)
            {
                message = "操作失败，不存在的用户组ID";
                return false;
            }

            try
            {
                BeginTransaction();

                string RemoveGroupUserSQL = @"DELETE FROM rel_user_group WHERE group_id = $groupId$";
                string RemoveGroupPremissionSQL = "DELETE FROM rel_usergroup_premission WHERE usergroup_id = $groupId$";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("groupId", userGroupId);

                ExecuteNonQuery(RemoveGroupUserSQL, pc);
                ExecuteNonQuery(RemoveGroupPremissionSQL, pc);

                if (Delete(userGroupId) == 1)
                {
                    CommitTransaction();
                    message = "成功删除用户组";
                    result = true;
                    GetUserGroupList(true);
                }
                else
                {
                    RollbackTransaction();
                    message = "删除用户组信息失败";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除用户组异常", ex);
                throw ex;
            }

            return result;
        }

        public bool UpdateUserGroupInfo(UserGroupInfoModel groupInfo, List<string> premissionList, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (groupInfo == null)
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            Dictionary<string, UserGroupInfoModel> groupList = GetUserGroupList(false);
            if (groupList.ContainsKey(groupInfo.UserGroupId) == false)
            {
                message = "操作失败，不存在的用户组ID";
                return false;
            }

            try
            {
                BeginTransaction();

                if (Update(groupInfo) == 1)
                {
                    string[] preArr = null;

                    #region 生成工单权限实体列表

                    Dictionary<string, WorkorderTypeInfoModel> WorkOrderTypeList = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(false);
                    Dictionary<string, RelUsergroupPremissionModel> WorkOrderPremissionList = new Dictionary<string, RelUsergroupPremissionModel>();
                    foreach (WorkorderTypeInfoModel item in WorkOrderTypeList.Values)
                    {
                        RelUsergroupPremissionModel pre = new RelUsergroupPremissionModel();
                        pre.UsergroupId = groupInfo.UserGroupId;
                        pre.WorkorderTypeId = item.WorkorderTypeId;
                        pre.PremissionType = "WORKORDER";
                        pre.Status = 0;
                        pre.CanSelect = 1;
                        pre.CanProcess = 1;
                        pre.CanClose = 1;
                        pre.CanDelete = 1;
                        pre.IsManage = 1;

                        WorkOrderPremissionList[pre.WorkorderTypeId] = pre;
                    }

                    #endregion

                    foreach (string premission in premissionList)
                    {
                        preArr = premission.Split('_');

                        #region 处理工单权限

                        if (preArr[0].ToUpper() == "WORKORDER")
                        {
                            switch (preArr[2])
                            {
                                case "SELECT":
                                    WorkOrderPremissionList[preArr[1]].CanSelect = 0;
                                    break;
                                case "PROCESS":
                                    WorkOrderPremissionList[preArr[1]].CanProcess = 0;
                                    break;
                                case "CLOSE":
                                    WorkOrderPremissionList[preArr[1]].CanClose = 0;
                                    break;
                                case "DELETE":
                                    WorkOrderPremissionList[preArr[1]].CanDelete = 0;
                                    break;
                                case "MANAGE":
                                    WorkOrderPremissionList[preArr[1]].IsManage = 0;
                                    break;
                                default:
                                    break;
                            }
                        }

                        #endregion

                    }


                    #region 更新工单权限至数据库

                    string DeleteWorkOrderPremissionSQL = "DELETE FROM rel_usergroup_premission WHERE usergroup_id = $usergroup_id$ AND premission_type = $premission_type$";
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("usergroup_id", groupInfo.UserGroupId);
                    pc.Add("premission_type", "WORKORDER");

                    if (ExecuteNonQuery(DeleteWorkOrderPremissionSQL, pc) >= 0)
                    {
                        foreach (RelUsergroupPremissionModel item in WorkOrderPremissionList.Values)
                        {
                            if (RelUsergroupPremissionService.Instance.Create(item) != 1)
                            {
                                RollbackTransaction();
                                message = "更新工单权限信息失败";
                                return false;
                            }
                        }
                    }
                    #endregion

                    CommitTransaction();
                    GetUserGroupList(true);
                    GetUserGroupDomainByGroupId(groupInfo.UserGroupId, true);
                    message = "成功更新用户组信息";
                    result = true;
                }
                else
                {
                    RollbackTransaction();
                    message = "更新用户组基本信息失败";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新用户组信息异常", ex);
                throw ex;
            }

            return result;
        }

        public UserGroupPremissionDomainModel GetUserGroupDomainByGroupId(string userGroupId, bool clear)
        {
            if (userGroupId == null)
                return null;

            string cacheKey = CacheKey.USERGROUP_DOMAINMODEL.GetKeyDefine(userGroupId);
            UserGroupPremissionDomainModel model = CacheUtil.Get<UserGroupPremissionDomainModel>(cacheKey);
            if (model == null || clear)
            {
                model = GetUserGroupDomainByGroupIdFromDatabase(userGroupId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
            }

            return model;
        }

        public UserGroupPremissionDomainModel GetUserGroupDomainByGroupIdFromDatabase(string userGroupId)
        {
            if (userGroupId == null)
                return null;

            UserGroupPremissionDomainModel result = null;

            UserGroupInfoModel groupInfo = Retrieve(userGroupId);
            if (groupInfo != null)
            {
                result = new UserGroupPremissionDomainModel();
                result.WorkOrderPremissionList = new Dictionary<string, RelUsergroupPremissionModel>();
                result.UserGroupInfo = groupInfo;

                #region 获取工单权限

                string GetWorkOrderPremissionSQL = "SELECT * FROM rel_usergroup_premission WHERE premission_type = 'WORKORDER' AND usergroup_id = $groupId$";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("groupId", userGroupId);
                DataTable WOPremissin = ExecuteDataTable(GetWorkOrderPremissionSQL, pc);
                if (WOPremissin != null && WOPremissin.Rows.Count > 0)
                {
                    RelUsergroupPremissionModel pre = null;
                    for (int i = 0; i < WOPremissin.Rows.Count; i++)
                    {
                        pre = new RelUsergroupPremissionModel();
                        ModelConvertFrom(pre, WOPremissin, i);
                        result.WorkOrderPremissionList[pre.WorkorderTypeId] = pre;
                    }
                }
                
                #endregion
            }

            return result;
        }

        /// <summary>
        /// 设置用户在组内的角色
        /// </summary>
        /// <param name="roleInGroup">0:成员，1：负责人</param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SetGroupUserRoleInGroup(int roleInGroup, string groupId, string userId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(userId))
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            if (GetUserGroupList(false).ContainsKey(groupId) == false)
            {
                message = "参数错误，不存在的用户组ID";
                return false;
            }

            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(userId, false);
            if (user == null)
            {
                message = "操作失败，尝试操作不存在的用户ID";
                return false;
            }

            string sql = "UPDATE rel_user_group SET role_in_group = 0 WHERE  group_id = $group_id$";
            string sql2 = "UPDATE rel_user_group SET role_in_group = 1 WHERE  user_id = $user_id$ AND group_id = $group_id$";

            ParameterCollection pc = new ParameterCollection();
            pc.Clear();
            pc.Add("user_id", userId);
            pc.Add("group_id", groupId);

            try
            {
                BeginTransaction();

                ExecuteNonQuery(sql, pc);

                if (ExecuteNonQuery(sql2, pc) == 1)
                {
                    CommitTransaction();
                    message = "成功设置选中用户在本组内角色";
                    return true;
                }

                RollbackTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("设置用户在组内角色异常", ex);
                throw ex;
            }

            return result;
        }

        public bool RemoveGroupUserById(string groupId, string userId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(userId))
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            if (GetUserGroupList(false).ContainsKey(groupId) == false)
            {
                message = "参数错误，不存在的用户组ID";
                return false;
            }

            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(userId, false);
            if (user == null)
            {
                message = "操作失败，尝试操作不存在的用户ID";
                return false;
            }

            string sql = "DELETE FROM rel_user_group WHERE  user_id = $user_id$ AND group_id = $group_id$";

            ParameterCollection pc = new ParameterCollection();
            pc.Clear();
            pc.Add("user_id", userId);
            pc.Add("group_id", groupId);

            try
            {
                BeginTransaction();

                if (ExecuteNonQuery(sql, pc) == 1)
                {
                    CommitTransaction();
                    message = "成功移除选中用户";
                    result = true;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("移除用户组用户异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 批量添加用户组用户。
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userIdList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BatchAddGroupUser(string groupId, List<string> userIdList, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (groupId == null || userIdList == null || userIdList.Count == 0)
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            if (GetUserGroupList(false).ContainsKey(groupId) == false)
            {
                message = "参数错误，不存在的用户组ID";
                return false;
            }

            string sql = "SELECT COUNT(1) FROM rel_user_group WHERE  user_id = $user_id$ AND group_id = $group_id$";
            string insSql = @"INSERT INTO [rel_user_group]
                                       ([user_id],[group_id],[user_status_in_group],[created_on],[created_by],[modified_on],[modified_by],[status_code])
                                 VALUES
                                       ($user_id$,$group_id$,0,GETDATE(),$created_by$,null,null,0)";

            ParameterCollection pc = new ParameterCollection();

            try
            {
                BeginTransaction();
                UserDomainModel user = null;

                foreach (string userId in userIdList)
                {
                    if (string.IsNullOrEmpty(userId))
                        continue;

                    user = UserInfoService.Instance.GetUserDomainModelById(userId, false);
                    if (user == null)
                    {
                        RollbackTransaction();
                        message = "操作失败，尝试插入不存在的用户ID";
                        return false;
                    }

                    pc.Clear();
                    pc.Add("user_id", userId);
                    pc.Add("group_id", groupId);

                    if (ExecuteScalar(sql, pc).ToString() == "0")
                    {
                        pc.Add("created_by", SessionUtil.Current.UserId);

                        if (ExecuteNonQuery(insSql, pc) != 1)
                        {
                            RollbackTransaction();
                            message = string.Format("插入用户名【{0}】至工作组失败，请与管理员联系", user.BasicInfo.CnName);
                            return false;
                        }
                        else
                        {
                            UserInfoService.Instance.GetUserDomainModelById(userId, true);
                        }
                    }
                }

                CommitTransaction();
                message = "成功添加选中用户至用户组";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("批量添加用户组用户异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 创建用户组信息。
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CreateNewGroupInfo(UserGroupInfoModel groupInfo, List<string> premissionBox, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (groupInfo == null)
            {
                message = "参数错误，请检查输入";
                return false;
            }

            groupInfo.UserGroupId = GetGuid();

            try
            {
                BeginTransaction();

                if (Create(groupInfo) == 1)
                {
                    string[] preArr = null;

                    #region 生成工单权限实体列表

                    Dictionary<string, WorkorderTypeInfoModel> WorkOrderTypeList = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(false);
                    Dictionary<string, RelUsergroupPremissionModel> WorkOrderPremissionList = new Dictionary<string, RelUsergroupPremissionModel>();
                    foreach (WorkorderTypeInfoModel item in WorkOrderTypeList.Values)
                    {
                        RelUsergroupPremissionModel pre = new RelUsergroupPremissionModel();
                        pre.UsergroupId = groupInfo.UserGroupId;
                        pre.WorkorderTypeId = item.WorkorderTypeId;
                        pre.PremissionType = "WORKORDER";
                        pre.Status = 0;
                        pre.CanSelect = 1;
                        pre.CanProcess = 1;
                        pre.CanClose = 1;
                        pre.CanDelete = 1;
                        pre.IsManage = 1;

                        WorkOrderPremissionList[pre.WorkorderTypeId] = pre;
                    }

                    #endregion

                    foreach (string premission in premissionBox)
                    {
                        preArr = premission.Split('_');

                        #region 处理工单权限

                        if (preArr[0].ToUpper() == "WORKORDER")
                        {
                            switch (preArr[2])
                            {
                                case "SELECT":
                                    WorkOrderPremissionList[preArr[1]].CanSelect = 0;
                                    break;
                                case "PROCESS":
                                    WorkOrderPremissionList[preArr[1]].CanProcess = 0;
                                    break;
                                case "CLOSE":
                                    WorkOrderPremissionList[preArr[1]].CanClose = 0;
                                    break;
                                case "DELETE":
                                    WorkOrderPremissionList[preArr[1]].CanDelete = 0;
                                    break;
                                case "MANAGE":
                                    WorkOrderPremissionList[preArr[1]].IsManage = 0;
                                    break;
                                default:
                                    break;
                            }
                        }                        

                        #endregion

                    }


                    #region 更新工单权限至数据库

                    string DeleteWorkOrderPremissionSQL = "DELETE FROM rel_usergroup_premission WHERE usergroup_id = $usergroup_id$ AND premission_type = $premission_type$";
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("usergroup_id", groupInfo.UserGroupId);
                    pc.Add("premission_type", "WORKORDER");

                    if (ExecuteNonQuery(DeleteWorkOrderPremissionSQL, pc) >= 0)
                    {
                        foreach (RelUsergroupPremissionModel item in WorkOrderPremissionList.Values)
                        {
                            if (RelUsergroupPremissionService.Instance.Create(item) != 1)
                            {
                                RollbackTransaction();
                                message = "更新工单权限信息失败";
                                return false;
                            }
                        }
                    }
                    #endregion

                    CommitTransaction();
                    GetUserGroupList(true);
                    message = "成功创建用户组";
                    result = true;
                }
                else
                {
                    RollbackTransaction();
                    message = "创建用户组信息失败";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建用户组信息异常", ex);
                throw ex;
            }

            return result;
        }

        public List<UserInfoModel> GetGroupUserList(string groupId)
        {
            List<UserInfoModel> list = null;
            if (groupId == null)
            {
                return null;
            }

            string sql = "SELECT * from user_info u RIGHT JOIN rel_user_group g on u.user_id = g.user_id WHERE g.group_id = $group_id$ AND u.work_status <> $work_status$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("group_id", groupId);
            pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId);

            DataTable dt = ExecuteDataTable(sql, pc);
            if (dt != null)
            {
                list = new List<UserInfoModel>();
                UserInfoModel user = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    user = new UserInfoModel();
                    ModelConvertFrom(user, dt, i);
                    user.RoleIdInGroup = (dt.Rows[i]["role_in_group"] == DBNull.Value) ? "0" : dt.Rows[i]["role_in_group"].ToString();
                    list.Add(user);
                }
            }

            return list;
        }

        public List<UserInfoModel> GetUnDefineGroupUserList(string groupId)
        {
            string sql = null;
            ParameterCollection pc = new ParameterCollection();

            if (groupId == null)
            {
                sql = "SELECT * FROM user_info WHERE work_status <> $work_status$";
                pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId);
            }
            else
            {
                 sql = @"SELECT * FROM user_info WHERE work_status <> $work_status$ AND user_id NOT IN (SELECT user_id FROM rel_user_group WHERE group_id = $group_id$)";
                 pc.Add("group_id", groupId);
                 pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId);
            }

            return ModelConvertFrom<UserInfoModel>(ExecuteDataTable(sql, pc));
        }

        /// <summary>
        /// 获取用户组列表字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, UserGroupInfoModel> GetUserGroupList(bool clear)
        {
            string cacheKey = CacheKey.USERGROUP_DICT;
            Dictionary<string, UserGroupInfoModel> dict = CacheUtil.Get<Dictionary<string, UserGroupInfoModel>>(cacheKey);
            if (dict == null || clear)
            {
                dict = GetUserGroupListFromDatabase();
                if (dict != null)
                {
                    CacheUtil.Set(cacheKey, dict);
                }
            }

            return dict;
        }

        public List<string> GetUserInGroupListFromDatabase(string userId)
        {
            List<string> list = null;

            string sql = "select group_id from rel_user_group r left join user_info u on r.user_id = u.user_id WHERE  r.user_id = $user_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("user_id", userId);

            DataTable dt = ExecuteDataTable(sql, pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
            }

            return list;
        }

        /// <summary>
        /// 从数据库获取用户组列表。
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, UserGroupInfoModel> GetUserGroupListFromDatabase()
        {
            Dictionary<string, UserGroupInfoModel> dict = null;
            List<UserGroupInfoModel> list = RetrieveMultiple(new ParameterCollection(), OrderByCollection.Create("created_on", "desc"));
            if (list != null)
            {
                dict = new Dictionary<string,UserGroupInfoModel>();
                foreach (UserGroupInfoModel item in list)
                {
                    dict[item.UserGroupId] = item;
                }
            }

            return dict;
        }
	}
}

