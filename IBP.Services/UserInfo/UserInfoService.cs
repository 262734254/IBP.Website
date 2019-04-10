/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-11-27
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;
using IBP.Models;

namespace IBP.Services
{
	/// <summary>
	/// UserInfo业务逻辑类
	/// </summary>
	public partial class UserInfoService : BaseService
	{
		// 在此添加你的代码...

        public Dictionary<string, UserInfoModel> GetUserInfoList(bool clear)
        {
            string cacheKey = CacheKey.USER_INFO;
            Dictionary<string, UserInfoModel> dict = CacheUtil.Get<Dictionary<string, UserInfoModel>>(cacheKey);

            if (dict == null || clear)
            {
                List<UserInfoModel> list = RetrieveMultiple(new ParameterCollection(), OrderByCollection.Create("user_id", "asc"));

                if (list != null)
                {
                    dict = new Dictionary<string, UserInfoModel>();
                    foreach (UserInfoModel item in list)
                    {
                        dict[item.UserId] = item;
                    }

                    CacheUtil.Set(cacheKey, dict);
                }
            }

            return dict;
        }

        public bool UpdateUserInfoStatus(UserInfoModel info, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
       
            if (Update(info) > 0)
            {
                GetUserDomainModelById(info.UserId, true);
                message = "修改用户状态成功";
                
                result = true;           
            }
            return result;
        }

        /// <summary>
        /// 新建用户信息。李程
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool NewUserInfo(UserInfoModel info, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            #region 判断用户信息是否存在
            if (CheckExistUserLoginName(info.LoginName))
            {
                message = string.Format("操作失败，已经存在名为【{0}】登录账号", info.LoginName);
                return false;
            }
            if (CheckExistUserWorkId(info.WorkId))
            {
                message = string.Format("操作失败，已经存在名为【{0}】工号", info.WorkId);
                return false;
            }

            if (CheckExistUserCtiUserId(info.CtiUserId))
            {
                message = string.Format("操作失败，已经存在名为【{0}】CTI账号", info.CtiUserId);
                return false;
            } 
            #endregion

            info.UserId = GetGuid();
            if (Create(info) > 0)
            {
                message = "添加用户信息成功";
                result = true;            
            }

            return result;
        }
       /// <summary>
       /// 修改用户信息
       /// </summary>
       /// <param name="info"></param>
       /// <param name="message"></param>
       /// <returns></returns>
        public bool UpdateUserInfo(UserInfoModel info)
        {
            bool result = false;
         
            if (Update(info) > 0)
            {
                GetUserDomainModelById(info.UserId, true);
                return true;
            }

            return result;
   
        }

        /// <summary>
        /// 批量删除用户信息。
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BatchDeleteUserInfoById(List<string> userid, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            if (userid == null || userid.Count == 0)
            {
                return result;
            }

            try
            {
                BeginTransaction();

                for (int i = 0; i < userid.Count; i++)
                {
                    if (Delete(userid[i]) == 0)
                    {
                        RollbackTransaction();
                        result = false;
                        message = "删除用户信息失败";
                        return result;
                    }
                    else
                    {
                     
                    }
                }

                CommitTransaction();
           
                result = true;
                message = "成功批量删除选中的用户信息";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除用户信息异常", ex);
                throw ex;
            }

            return result;
        }
       /// <summary>
       /// 根据ID删除用户信息
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="message"></param>
       /// <returns></returns>
        public bool DeleteUserInfoById(string userId, out string message)
        {
  
            message = "操作失败，请与管理员联系";
            bool result = false;

            try
            {
                BeginTransaction();

                if (Delete(userId) > 0)
                {
                    CommitTransaction();
                    result = true;
                    message = "成功删除用户信息";
                }
                else
                {
                    RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除用户信息异常", ex);
                throw ex;
            }

            return result;
        }

        #region 判断用户信息是否存在

        /// <summary>
        /// 检查登录名是否存在。
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private bool CheckExistUserLoginName(string loginName)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    user_info 
WHERE 
    login_name = $login_name$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("login_name", loginName);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        /// <summary>
        /// 检查工号是否存在。
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        private bool CheckExistUserWorkId(string workID)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    user_info 
WHERE 
    work_id = $work_id$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("work_id", workID);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        /// <summary>
        /// 检查邮箱是否存在。
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        private bool CheckExistUserEmail(string Email)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    user_info 
WHERE 
    user_email = $user_email$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("user_email", Email);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        /// <summary>
        /// 检查CTI账号是否存在。
        /// </summary>
        /// <param name="CtiUserId"></param>
        /// <returns></returns>
        private bool CheckExistUserCtiUserId(string CtiUserId)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    user_info 
WHERE 
    cti_user_id = $cti_user_id$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("cti_user_id", CtiUserId);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        } 
        #endregion

        public bool CheckInProjectGroup77()
        {
            UserDomainModel user = GetUserDomainModelById(SessionUtil.Current.UserId, false);
            if (user != null && user.InGroupList != null)
            {
                Dictionary<string, UserGroupInfoModel> groupList = UserGroupInfoService.Instance.GetUserGroupList(false);

                foreach (string groupId in user.InGroupList)
                {
                    if (groupList.ContainsKey(groupId))
                    {
                        if (groupList[groupId].GroupName == "77项目组")
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// 设置访客的Session信息。
        /// </summary>
        public void SetVisitorsSessionInfo()
        {
            SessionInfo sessionInfo = SessionUtil.Current;

            if (sessionInfo == null)
            {
                // 从Cookie中获取Session对象
                // sessionInfo = JsonUtil.Deserialize<SessionInfo>(CookieUtil.GetValue(Consts.USER_COOKIE));
                if (sessionInfo == null)
                {
                    sessionInfo = new SessionInfo();
                    sessionInfo.LoginName = Guid.NewGuid().ToString();
                    sessionInfo.RoleId = "c397044b-d856-11db-a71b-a52ff954baa7";
                    sessionInfo.UserId = sessionInfo.LoginName;
                    sessionInfo.CnName = "访客";
                    sessionInfo.EnName = "Visitor";
                    sessionInfo.IsLogin = false;

                    // 保存到Cookies中
                    // CookieUtil.SetObj(Consts.USER_COOKIE, 86400, JsonUtil.Serialize(sessionInfo));
                }

                SessionUtil.Current = sessionInfo;
            }
        }

        public void Logout()
        {
            SessionUtil.Current = null;
            SetVisitorsSessionInfo();
        }

        public LoginStatusEnum UserLogin(string loginInput, string loginPwd, string validCode)
        {
            #region 如果用户尝试登录次数超出限制，则需要验证验证码

            // 初始Session中保存的为yyyyMMdd01
            ////if (Convert.ToInt32(SessionUtil.LoginTryCount.Substring(7, 2)) > ConfigUtil.GetLoginTryCountLimit())
            ////{
            //    if (validCode.ToLower() != SessionUtil.AuthorizationCode.ToLower())
            //    {
            //        //SessionUtil.LoginTryCount = (Convert.ToInt32(SessionUtil.LoginTryCount) + 1).ToString();
            //        return LoginStatusEnum.NameOrPwdErrorAndShowValidCode;
            //    }
            ////}

            return UserLogin(loginInput, loginPwd);

            #endregion
        }

        /// <summary>
        /// 根据ID获取用户领域模型。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public UserDomainModel GetUserDomainModelById(string userId, bool clear)
        {
            if (string.IsNullOrEmpty(userId))
                return null;

            string cacheKey = CacheKey.GetKeyDefine(CacheKey.USERDOMAIN_INFO, userId);
            UserDomainModel result = CacheUtil.Get<UserDomainModel>(cacheKey);

            if (result == null || clear)
            {
                UserInfoModel basicInfo = GetUserModelByIdFromDatabase(userId);
                if (basicInfo != null)
                {
                    result = new UserDomainModel();
                    result.BasicInfo = basicInfo;
                    result.InGroupList = UserGroupInfoService.Instance.GetUserInGroupListFromDatabase(basicInfo.UserId);

                    CacheUtil.Set(cacheKey, result);
                }
            }

            return result;
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        /// <remarks>
        /// 用户可通过登录名及工号登录系统。
        /// </remarks>
        public LoginStatusEnum UserLogin(string loginInput, string loginPwd)
        {
            #region 为减少数据库请求，用户实体保存在缓存中，可通过登录名及工号登录系统

            // 使用2个缓存KEY保存指向用户ID的缓存KEY，从而获取用户实体。
            string loginNameKey = CacheKey.GetKeyDefine(CacheKey.USER_LOGIN_NAME_KEY, loginInput);
            string loginWorkIdKey = CacheKey.GetKeyDefine(CacheKey.USER_WORKID_KEY, loginInput);

            // 通过登录名称获取指向用户实体的ID缓存KEY
            string userDomainInfoKey = CacheUtil.Get<string>(loginNameKey);
            if (userDomainInfoKey == null)
            {
                // 如果通过登录名称获取不到，说明可能输入的是工号，尝试通过工号获取指向用户实体的ID缓存KEY
                userDomainInfoKey = CacheUtil.Get<string>(loginWorkIdKey);
            }

            // 从缓存中获取用户领域模型
            UserDomainModel domainModel = CacheUtil.Get<UserDomainModel>(userDomainInfoKey);

            #endregion

            #region 如果缓存中没有用户领域模型，再从数据库中重新获取用户领域模型，并缓存

            if (domainModel == null)
            {
                domainModel = GetUserDomainModelFromDatabase(loginInput);

                if (domainModel == null)
                {
                    // 数据库中不存在该用户。
                    return LoginStatusEnum.NotExists;
                }

                userDomainInfoKey = CacheKey.GetKeyDefine(CacheKey.USERDOMAIN_INFO, domainModel.BasicInfo.UserId);

                // 保存缓存KEY。
                CacheUtil.Set(userDomainInfoKey, domainModel);
                CacheUtil.Set(CacheKey.USER_LOGIN_NAME_KEY.GetKeyDefine(domainModel.BasicInfo.LoginName), CacheKey.GetKeyDefine(CacheKey.USER_LOGIN_NAME_KEY, domainModel.BasicInfo.LoginName));
                CacheUtil.Set(CacheKey.USER_WORKID_KEY.GetKeyDefine(domainModel.BasicInfo.WorkId), CacheKey.GetKeyDefine(CacheKey.USER_WORKID_KEY, domainModel.BasicInfo.WorkId));
            }

            #endregion

            #region 判断用户登录密码

            if (domainModel.BasicInfo.LoginPwd != loginPwd)
            {
                // 记录用户登录次数
                SessionUtil.LoginTryCount = (Convert.ToInt32(SessionUtil.LoginTryCount) + 1).ToString();
                if (Convert.ToInt32(SessionUtil.LoginTryCount.Substring(7, 2)) > ConfigUtil.GetLoginTryCountLimit())
                {
                    return LoginStatusEnum.NameOrPwdErrorAndShowValidCode;
                }
                else
                {
                    return LoginStatusEnum.NameOrPwdError;
                }
            }

            #endregion

            #region 判断用户状态

            if (domainModel.BasicInfo.Status == 1)
            {
                return LoginStatusEnum.Disabled;
            }

            #endregion


            #region 保存Session信息

            SessionInfo sessionInfo = new SessionInfo();
            sessionInfo.UserId = domainModel.BasicInfo.UserId;
            sessionInfo.LoginName = domainModel.BasicInfo.LoginName;
            sessionInfo.CnName = domainModel.BasicInfo.CnName;
            sessionInfo.EnName = domainModel.BasicInfo.EnName;
            sessionInfo.IsLogin = true;
            sessionInfo.RoleId = domainModel.BasicInfo.RoleId;
            sessionInfo.LanguageCode = IBP.Common.Consts.LANGUAGE_CN;
            sessionInfo.UserGroup = domainModel.InGroupList;
            sessionInfo.ExtAttributes = new Dictionary<string, object>();

            // CTI 账号信息。
            sessionInfo.ExtAttributes["CtiUserId"] = domainModel.BasicInfo.CtiUserId;
            sessionInfo.ExtAttributes["CtiUserPwd"] = domainModel.BasicInfo.CtiUserPwd;


            SessionUtil.Current = sessionInfo;

            SessionUtil.ResetLoginTryCount();

            #endregion

            return LoginStatusEnum.Success;
        }

        /// <summary>
        /// 根据用户ID获取用户领域模型。
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="validCode"></param>
        /// <returns></returns>
        protected UserDomainModel GetUserDomainModelFromDatabase(string loginInput)
        {
            UserDomainModel model = null;

            UserInfoModel basicInfo = GetUserModelByLoginNameFromDatabase(loginInput);

            if (basicInfo != null)
            {
                model = new UserDomainModel();
                model.BasicInfo = basicInfo;
                model.InGroupList = UserGroupInfoService.Instance.GetUserInGroupListFromDatabase(basicInfo.UserId);
            }

            return model;
        }

        /// <summary>
        /// 根据用户ID从数据库获取用户数据模型。
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        protected UserInfoModel GetUserModelByLoginNameFromDatabase(string loginInput)
        {
            UserInfoModel model = null;

            string sql = @"
SELECT 
    * 
FROM 
	user_info
WHERE
	login_name = $login_name$ 
    OR
    work_id = $work_id$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("login_name", "LOGINNAME_" + loginInput);
            pc.Add("work_id", "WORKID_" + loginInput);

            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    // 用户登录名称与工号相同为异常情况。
                    throw new Exception(ErrorCode.EXCEPTION_001);
                }

                model = new UserInfoModel();
                ModelConvertFrom(model, dt, 0);
            }

            return model;
        }

        
        protected UserInfoModel GetUserModelByIdFromDatabase(string userId)
        {
            UserInfoModel model = null;

            string sql = @"
SELECT 
    * 
FROM 
	user_info
WHERE
	user_id = $user_id$ 
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("user_id", userId);

            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    // 用户登录名称与工号相同为异常情况。
                    throw new Exception(ErrorCode.EXCEPTION_001);
                }

                model = new UserInfoModel();
                ModelConvertFrom(model, dt, 0);
            }

            return model;
        }
        #region 查询用户信息
        /// <summary>
        /// 李程新添加查询用户信息代码
        /// </summary>
        /// <param name="queryCollection"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderDirection"></param>
        /// <param name="total"></param>
        /// <returns></returns>
   
        public List<string> GetUserInfoList(Dictionary<string, QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            total = 0;
            List<string> result = null;
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("FROM user_info WHERE 1=1 ");
            ParameterCollection pc = new ParameterCollection();
            int count = 0;

            #region 构造查询条件

            foreach (QueryItemDomainModel item in queryCollection.Values)
            {
                switch (item.Operation)
                {
                    case "equal":
                        sqlBuilder.AppendFormat(@" AND [{0}] = $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "notequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] <> $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "contain":
                        sqlBuilder.AppendFormat(@" AND [{0}] LIKE $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), "%" + item.SearchValue + "%");
                        break;

                    case "greater":
                        sqlBuilder.AppendFormat(@" AND [{0}] > $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "greaterequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] >= $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "less":
                        sqlBuilder.AppendFormat(@" AND [{0}] < $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "lessequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] <= $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "between":
                        sqlBuilder.AppendFormat(@" AND [{0}] BETWEEN $begin{1}$ AND $end{1}$", item.FieldType, count);
                        pc.Add("begin" + count.ToString(), item.BeginTime);
                        pc.Add("end" + count.ToString(), item.EndTime);
                        break;

                    case "today":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(DAY,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "week":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(WEEK,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "month":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(MONTH,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "quarter":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(QUARTER,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    case "year":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(YEAR,[{0}],GETDATE()) = 0", item.FieldType);
                        break;

                    default:
                        break;
                }

                count++;
            }

            #endregion

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + sqlBuilder.ToString(), pc));

            DataTable dt = ExecuteDataTable("SELECT User_id " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create(orderField, orderDirection));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.Add(dt.Rows[i][0].ToString());
                }
            }

            return result;
        }

        #endregion

	}
}

