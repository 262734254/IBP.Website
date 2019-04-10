using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Utilities;
using System.Diagnostics;
using System.Data;
using IBP.Common;
using Framework.Common;
using IBP.Models;

namespace IBP.Services
{
    public class PermissionService : BaseService
    {
        ///<summary>
        /// 根据用户角色及所属用户组，判断他是否有访问 指定controller 下面 action 的权限。
        ///</summary>
        ///<param name="roleId">用户角色ID</param>
        ///<param name="userGroupList">用户组ID</param>
        ///<param name="controllerName">控制器完整名称，需要包含命名空间</param>
        ///<param name="actionName">动作名称</param>
        ///<returns>返回用户是否具有访问 指定controller 下面 action 的权限</returns>
        public static bool HasPermission(string roleId, List<string> userGroupList, string controllerName, string actionName)
        {
            if (userGroupList == null)
            {
                userGroupList = new List<string>();
            }

            LogUtil.Debug(string.Format("根据用户角色({0})及用户组({1})，判断否有访问controller({2})下面action({3})的权限。"
                , roleId, userGroupList.ToString(), controllerName, actionName));

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            DataTable rolePermissions = InitRolePermissions(false);

            string fielterSQL = string.Format("RoleId = '{0}' AND ControllerName = '{1}' AND ActionName = '{2}'",
                roleId, controllerName, actionName);
            DataRow[] hasRows = rolePermissions.Select(fielterSQL);

            LogUtil.Debug(string.Format("角色{0}{1}有访问controller{2}下面{3}的权限"
                , roleId, hasRows.Length == 0 ? "没" : "", controllerName, actionName));

            sw.Stop();
            LogUtil.Debug(string.Format("判断角色权限执行的时间为{0}毫秒", sw.ElapsedMilliseconds));

            if (hasRows.Length > 0)
            {
                return true;
            }

            sw.Reset();
            sw.Start();

            DataTable groupPermissions = InitUserGroupPermissions(false);

            DataRow[] hasGroupRows = null;
            for (int i = 0; i < userGroupList.Count; i++)
            {
                fielterSQL = string.Format("UserGroupId = '{0}' AND ControllerName = '{1}' AND ActionName = '{2}'",
                  userGroupList[i], controllerName, actionName);

                hasGroupRows = groupPermissions.Select(fielterSQL);
                if (hasGroupRows.Length > 0)
                    break;
            }

            sw.Stop();
            LogUtil.Debug(string.Format("判断用户组权限执行的时间为{0}毫秒", sw.ElapsedMilliseconds));

            return  (hasGroupRows == null) ? false : hasGroupRows.Length > 0;
        }

        /// <summary>
        /// 初始化用户组权限。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        /// <remarks>
        /// 初始化权限，保存角色没有权限访问的 Action 列表。
        /// 该表中包含 3 列：用户组Id(UserGroupId)，控制器名称(ControllerName)，动作名称(ActionName)。
        /// </remarks>
        public static DataTable InitUserGroupPermissions(bool clear)
        {
            string cacheKey = CacheKey.USERGROUP_PERMISSIONS;
            DataTable permissions = CacheUtil.Get<DataTable>(cacheKey);
            if (clear || permissions == null)
            {
                LogUtil.Debug("初始化用户组权限");
                string sql = @"
SELECT 
	g.user_group_id			AS UserGroupId, 
	g.group_name			AS GroupName,		
    a.action_id             AS ActionId, 
    a.node_id				AS NodeId,
    a.parent_node			AS ParentNode,
    a.action_group			AS ActionGroup,
    a.controller_name       AS ControllerName, 
    a.action_name           AS ActionName, 
    a.display_name			AS DisplayName, 
    a.action_type           AS ActionType, 
    a.sort_order            AS SortOrder
FROM 
	user_group_info g
INNER JOIN 
	rel_usergroup_action ga 
	ON ga.user_group_id = g.user_group_id
INNER JOIN 
	action_info a 
	ON ga.action_id = a.action_id
ORDER BY
	a.sort_order
";
                permissions = DbUtil.Current.IData.ExecuteDataTable(sql, CommandTypeEnum.Text, null);
                CacheUtil.Set(cacheKey, permissions);
                LogUtil.Debug("初始化用户组权限结束");
            }
            return permissions;
        }

        /// <summary>
        /// 初始化角色权限。
        /// </summary>
        /// <param name="clear">是否需要刷新权限列表。</param>
        /// <returns></returns>
        /// <remarks>
        /// 初始化权限，保存角色没有权限访问的 Action 列表。
        /// 该表中包含 3 列：角色Id(RoleId)，控制器名称(ControllerName)，动作名称(ActionName)。
        /// </remarks>
        public static DataTable InitRolePermissions(bool clear)
        {
            string cacheKey = CacheKey.ROLE_PERMISSIONS;
            DataTable permissions = CacheUtil.Get<DataTable>(cacheKey);

            if (clear || permissions == null)
            {
                LogUtil.Debug("初始化角色权限");
                string sql = @"
SELECT 
	r.role_id               AS RoleId, 	
    a.action_id             AS ActionId, 
    a.node_id				AS NodeId,
    a.parent_node			AS ParentNode,
    a.action_group			AS ActionGroup,
    a.controller_name       AS ControllerName, 
    a.action_name           AS ActionName, 
    a.display_name			AS DisplayName, 
    a.action_type           AS ActionType, 
    a.sort_order            AS SortOrder
FROM 
	role_info r 
INNER JOIN 
	rel_role_action ra 
	ON ra.role_id = r.role_id
INNER JOIN 
	action_info a 
	ON ra.action_id = a.action_id
ORDER BY
	a.sort_order
";
                permissions = DbUtil.Current.IData.ExecuteDataTable(sql, CommandTypeEnum.Text, null);
                CacheUtil.Set(cacheKey, permissions);
                LogUtil.Debug("初始化角色权限结束");
            }
            return permissions;
        }

        #region 权限管理设置

        /// <summary>
        /// 获取完整菜单树。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public static ActionDomainModel GetMenuTree(bool clear)
        {
            string cacheKey = CacheKey.ACTION_TREE;

            ActionDomainModel tree = CacheUtil.Get<ActionDomainModel>(cacheKey);
            if (tree == null || clear)
            {
                tree = GetActionDomainModelFromDatabase();
                if (tree != null && tree.ChildActionList != null)
                {
                    CacheUtil.Set(cacheKey, tree);
                }
                else
                {
                    CacheUtil.Remove(cacheKey);
                }
            }

            return tree;
        }

        public static ActionDomainModel GetMenuTreeByRoleId(string roleId)
        {
            ActionDomainModel tree = new ActionDomainModel();
            SetChildActionList(tree, "0", InitRolePermissions(false), roleId);

            return tree;
        }

        /// <summary>
        /// 获取指定操作的上级操作信息领域模型。
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="actionTable"></param>
        /// <param name="srcList"></param>
        public static void GetActionParentActionList(string actionId, string parentNode, DataTable actionTable,List<ActionDomainModel> srcList)
        {
            string filterSQL = (parentNode == null) 
                ? string.Format("ActionId = '{0}' AND ActionType = 0", actionId)
                : string.Format("NodeId = '{0}' AND ActionType = 0 ", parentNode);

            DataRow[] hasRows = actionTable.Select(filterSQL);

            if (hasRows != null && hasRows.Length == 1)
            {
                ActionDomainModel currAction = GetActionDomainModelFromDataRow(hasRows[0]);
                if (srcList.Contains(currAction) == false)
                {
                    srcList.Add(currAction);
                }

                if (currAction.ParentNode != "0")
                {
                    GetActionParentActionList(currAction.ActionId, currAction.ParentNode, actionTable, srcList);
                }
            }
        }

        /// <summary>
        /// 从数据库生成操作菜单树。
        /// </summary>
        /// <returns></returns>
        protected static ActionDomainModel GetActionDomainModelFromDatabase()
        {
            ActionDomainModel tree = new ActionDomainModel();
            SetChildActionList(tree, "0", GetActionTableFromDatabase(), null);

            return tree;
        }

        /// <summary>
        /// 从数据库获取操作信息表。
        /// </summary>
        /// <returns></returns>
        public static DataTable GetActionTableFromDatabase()
        {
            string sql = @"
SELECT 
    a.action_id             AS ActionId, 
    a.node_id				AS NodeId,
    a.parent_node			AS ParentNode,
    a.action_group			AS ActionGroup,
    a.controller_name       AS ControllerName, 
    a.action_name           AS ActionName, 
    a.display_name			AS DisplayName, 
    a.action_type           AS ActionType, 
    a.sort_order            AS SortOrder
FROM
	action_info a 
ORDER BY 
	sort_order
";

            return  ExecuteDataTable(sql);
        }

        /// <summary>
        /// 获取指定角色所有操作ID列表。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static List<string> GetRoleActionIdList(string roleId)
        {
            List<string> result = new List<string>();

            List<ActionDomainModel> list = GetRoleActionList(roleId);
            if (list != null)
            {
                foreach (ActionDomainModel item in list)
                {
                    result.Add(item.ActionId);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取指定角色所有操作实体领域模型。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static List<ActionDomainModel> GetRoleActionList(string roleId)
        {
            List<ActionDomainModel> list = null;

            string sql = @"
SELECT 
    a.action_id             AS ActionId, 
    a.node_id				AS NodeId,
    a.parent_node			AS ParentNode,
    a.action_group			AS ActionGroup,
    a.controller_name       AS ControllerName, 
    a.action_name           AS ActionName, 
    a.display_name			AS DisplayName, 
    a.action_type           AS ActionType, 
    a.sort_order            AS SortOrder
FROM
    rel_role_action r
INNER JOIN
	action_info a  ON r.action_id = a.action_id
WHERE
	r.role_id = $role_id$
ORDER BY 
	sort_order
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("role_id", roleId);

            DataTable dt = ExecuteDataTable(sql, pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<ActionDomainModel>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(GetActionDomainModelFromDataRow(dt.Rows[i]));
                }
            }

            return list;
        }

        /// <summary>
        /// 设置操作菜单树。
        /// </summary>
        /// <param name="srcAction"></param>
        /// <param name="parentNode"></param>
        /// <param name="actionTable"></param>
        protected static void SetChildActionList(ActionDomainModel srcAction, string parentNode, DataTable actionTable, string roleId)
        {
            if (actionTable == null) return;

            string filterSQL = null;
            string getGroupSQL = null;

            if (roleId != null)
            {
                filterSQL = string.Format("ParentNode = '{0}' AND ActionType = 0 AND RoleId = '{1}'", parentNode, roleId);
                getGroupSQL = "ActionGroup = '{0}' AND ActionType = 1 AND RoleId = '" + roleId + "'";
            }
            else
            {
                filterSQL = string.Format("ParentNode = '{0}' AND ActionType = 0", parentNode);
                getGroupSQL = "ActionGroup = '{0}' AND ActionType = 1";
            }

            DataRow[] hasRows = actionTable.Select(filterSQL, "SortOrder ASC");
            DataRow[] groupRows = null;


            if (hasRows != null && hasRows.Length > 0)
            {
                srcAction.ChildActionList = new Dictionary<string, ActionDomainModel>();
                ActionDomainModel model = null;
                ActionDomainModel groupItem = null;

                for (int i = 0; i < hasRows.Length; i++)
                {
                    model = GetActionDomainModelFromDataRow(hasRows[i]);
                    model.ActionGroupList = new Dictionary<string, ActionDomainModel>();
                    model.ActionGroupList.Add(model.ActionId, model);

                    groupRows = actionTable.Select(string.Format(getGroupSQL, model.ActionGroup));
                    if (groupRows != null && groupRows.Length > 0)
                    {
                        for (int j = 0; j < groupRows.Length; j++)
                        {
                            groupItem = GetActionDomainModelFromDataRow(groupRows[j]);
                            model.ActionGroupList.Add(groupItem.ActionId, groupItem);
                        }
                    }

                    SetChildActionList(model, model.NodeId, actionTable, roleId);

                    srcAction.ChildActionList.Add(model.ActionId, model);
                }
            }
        }

        /// <summary>
        /// 根据数据行生成操作领域模型。
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected static ActionDomainModel GetActionDomainModelFromDataRow(DataRow row)
        {
            ActionDomainModel model = new ActionDomainModel();

            model.ActionId = row["ActionId"].ToString();
            model.NodeId = row["NodeId"].ToString();
            model.ParentNode = row["ParentNode"].ToString();
            model.ActionType = Convert.ToInt32(row["ActionType"]);
            model.ActionName = row["ActionName"].ToString();
            model.ActionGroup = row["ActionGroup"].ToString();
            model.ControllerName = row["ControllerName"].ToString();
            model.DisplayName = row["DisplayName"].ToString();
            model.SortOrder = Convert.ToInt32(row["SortOrder"]);

            return model;
        }

        #endregion

    }
}