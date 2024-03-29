/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-11-27
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
	/// RoleInfo业务逻辑类
	/// </summary>
	public partial class RoleInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool UpdateRoleAndPremission(RoleInfoModel roleInfo, List<string> roleActionList, out string message)
        {
            bool result = false;
            message = "操作失败，更新角色信息异常，请与管理员联系";

            try
            {
                BeginTransaction();

                if (Update(roleInfo) > 0)
                {
                    if (RemoveRoleAction(roleInfo.RoleId, null))
                    {
                        if (CreateRolePremissions(roleInfo.RoleId, roleActionList))
                        {
                            CommitTransaction();
                            GetRoleDomainList(true);
                            result = true;
                            message = "成功更新角色信息";
                        }
                        else
                        {
                            RollbackTransaction();
                            result = false;
                            message = "操作失败，更新角色权限信息异常，请与管理员联系";
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        result = false;
                        message = "操作失败，移除角色权限信息异常，请与管理员联系";
                    }
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "操作失败，更新角色信息异常，请与管理员联系";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新角色信息异常", ex);
                throw ex;
            }

            return result;
        }

        public bool CreateNewRole(RoleInfoModel roleInfo, List<string> roleActionList, out string message)
        {
            bool result = false;
            message = "操作失败，创建角色信息异常，请与管理员联系";

            try
            {
                BeginTransaction();

                if (CreateNewRole(roleInfo, out message))
                {
                    if (CreateRolePremissions(roleInfo.RoleId, roleActionList))
                    {
                        CommitTransaction();
                        GetRoleDomainList(true);
                        result = true;
                        message = "成功创建角色信息";
                    }
                    else
                    {
                        RollbackTransaction();
                        result = false;
                        message = "操作失败，创建角色权限信息异常，请与管理员联系";
                    }
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建角色信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 移除指定角色权限操作。
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public bool RemoveRoleAction(string roleId, string actionId)
        {
            bool result = false;

            string sql = @"
DELETE FROM 
    rel_role_action
WHERE
    role_id = $role_id$";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("role_id", roleId);

            if (actionId != null)
            {
                sql = sql + " AND action_id = $action_id$";
                pc.Add("action_id", actionId);
            }

            result = ExecuteNonQuery(sql, pc) >= 0;

            return result;
        }

        /// <summary>
        /// 创建角色权限信息。
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleActionList"></param>
        /// <returns></returns>
        public bool CreateRolePremissions(string roleId, List<string> roleActionList)
        {
            bool result = false;
            if (roleActionList == null || roleActionList.Count == 0)
            {
                return false;
            }

            #region 如果最子级菜单被选中，其上级菜单也将被选中

            List<string> insAction = new List<string>();
            for (int i = 0; i < roleActionList.Count; i++)
            {
                insAction.Add(roleActionList[i]);
            }

            List<ActionDomainModel> actList = new List<ActionDomainModel>();
            DataTable actionTable = PermissionService.GetActionTableFromDatabase();
                 
            for (int i = 0; i < insAction.Count; i++)
            {
                PermissionService.GetActionParentActionList(insAction[i], null, actionTable, actList);
            }

            for (int i = 0; i < actList.Count; i++)
            {
                if (!roleActionList.Contains(actList[i].ActionId))
                {
                    roleActionList.Add(actList[i].ActionId);
                }
            }

            #endregion

            try
            {
                BeginTransaction();

                if (RemoveRoleAction(roleId, null))
                {
                    RelRoleActionModel model = null;
                    for (int i = 0; i < roleActionList.Count; i++)
                    {
                        model = new RelRoleActionModel();
                        model.ActionId = roleActionList[i];
                        model.RoleId = roleId;

                        if (RelRoleActionService.Instance.Create(model) < 1)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    model = new RelRoleActionModel();
                    model.ActionId = "7AE55462-7727-4F6D-8989-FE15CAF4DDC0";
                    model.RoleId = roleId;

                    if (RelRoleActionService.Instance.Create(model) < 1)
                    {
                        RollbackTransaction();
                        return false;
                    }

                    CommitTransaction();
                    PermissionService.InitRolePermissions(true);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("插入角色关联操作信息至rel_role_action表异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 创建角色信息。
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CreateNewRole(RoleInfoModel roleInfo, out string message)
        {
            bool result = false;
            message = "成功创建角色信息";
            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    role_info
WHERE
    role_name = $role_name$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("role_name", roleInfo.RoleName);

            if (Convert.ToInt32(ExecuteScalar(sql, pc)) > 0)
            {
                message = string.Format("操作失败，系统中存在名为【{0}】的角色信息", roleInfo.RoleName);
                return false;
            }

            if (Create(roleInfo) > 0)
            {
                result = true;
                GetRoleDomainList(true);
            }

            return result;
        }

        /// <summary>
        /// 批量增加角色成员。
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        public bool AddRoleUser(string roleId, List<string> userIdList)
        {
            bool result = false;

            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }

            if (userIdList != null && userIdList.Count > 0)
            {
                string sql = @"
UPDATE 
    user_info
SET
    role_id = $role_id$
WHERE
    user_id = $user_id$
";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("role_id", roleId);
                pc.Add("user_id", "");

                try
                {
                    BeginTransaction();

                    foreach (string id in userIdList)
                    {
                        if (string.IsNullOrEmpty(id))
                            continue;

                        pc["user_id"].Value = id;

                        if (ExecuteNonQuery(sql, pc) <= 0)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    result = true;
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("批量增加角色成员异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// 根据ID删除角色信息。
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteRoleById(string roleId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            try
            {
                BeginTransaction();

                RemoveRoleUserByRoleId(roleId);
                RemoveRoleAction(roleId, null);

                if (Delete(roleId) > 0)
                {
                    CommitTransaction();
                    GetRoleDomainList(true);
                    result = true;
                    message = "成功删除角色信息";
                }
                else
                {
                    RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除角色信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 移除角色所有隶属用户。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool RemoveRoleUserByRoleId(string roleId)
        {
            bool result = false;

            string sql = @"UPDATE user_info SET role_id = '' WHERE role_id = $role_id$";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("role_id", roleId);

            result = ExecuteNonQuery(sql, pc) > 0;

            return result;
        }

        /// <summary>
        /// 移除用户角色。
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveRoleUserByUserId(string userId)
        {
            bool result = false;

            string sql = @"UPDATE user_info SET role_id = '' WHERE user_id = $user_id$";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("user_id", userId);

            result = ExecuteNonQuery(sql, pc) > 0;

            return result;
        }

        /// <summary>
        /// 获取所有未定义角色用户列表。
        /// </summary>
        /// <returns></returns>
        public List<UserInfoModel> GetUnDefineRoleUserList()
        {
            string sql = @"SELECT * FROM user_info WHERE (role_id = '' OR role_id = null) AND work_status <> $work_status$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId);

            return ModelConvertFrom<UserInfoModel>(ExecuteDataTable(sql, pc));
        }

        /// <summary>
        /// 根据ID获取角色领域模型。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleDomainModel GetRoleDomainModel(string roleId)
        {
            Dictionary<string, RoleDomainModel> dict = GetRoleDomainList(false);

            return (dict.ContainsKey(roleId)) ? dict[roleId] : null;
        }

        /// <summary>
        /// 获取角色信息字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, RoleDomainModel> GetRoleDomainList(bool clear)
        {
            string cacheKey = CacheKey.ROLE_DICT;

            Dictionary<string, RoleDomainModel> dict = CacheUtil.Get<Dictionary<string, RoleDomainModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetRoleDomainListFromDatabse();
                if (dict != null)
                {
                    CacheUtil.Set(cacheKey, dict);
                }
                else
                {
                    CacheUtil.Remove(cacheKey);
                }
            }

            return dict;
        }

        /// <summary>
        /// 从数据库中获取角色信息。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, RoleDomainModel> GetRoleDomainListFromDatabse()
        {
            Dictionary<string, RoleDomainModel> dict = null;

            string sql = @"
SELECT
	role_id, role_name, cn_name, description, role_status
FROM 
	role_info
";

            DataTable dt = ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dict = new Dictionary<string, RoleDomainModel>();
                RoleDomainModel model = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new RoleDomainModel();
                    model.RoleId = dt.Rows[i]["role_id"].ToString();
                    model.RoleName = dt.Rows[i]["role_name"].ToString();
                    model.CnName = dt.Rows[i]["cn_name"].ToString();
                    model.Description = dt.Rows[i]["description"].ToString();
                    model.Status = Convert.ToInt32(dt.Rows[i]["role_status"]);

                    dict.Add(model.RoleId, model);
                }
            }

            return dict;
        }


        /// <summary>
        /// 获取角色成员信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<UserInfoModel> GetRoleUserList(string roleId)
        {
            string sql = @"SELECT * FROM user_info WHERE role_id = $role_id$ AND work_status <> $work_status$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId);
            pc.Add("role_id", roleId);

            return ModelConvertFrom<UserInfoModel>(ExecuteDataTable(sql, pc));
        }
	}
}

