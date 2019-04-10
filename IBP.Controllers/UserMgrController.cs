using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IBP.Services;
using IBP.Common;
using Framework.Utilities;
using IBP.Models;

namespace IBP.Controllers
{
    public class UserMgrController : BaseController
    {
        #region 用户信息管理
        #region 用户列表
        /// <summary>
        /// 用户列表。  李程
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult UserList()
        {
            InitPagerForm();
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            queryItem = new QueryItemDomainModel();

            string work_status = GetFormData("work_status");
            if (string.IsNullOrEmpty(work_status) || work_status == "working")
            {
                queryItem.FieldType = "work_status";
                queryItem.SearchValue = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId;
                queryItem.Operation = "notequal";
            }
            else
            {
                queryItem.FieldType = "work_status";
                queryItem.SearchValue = work_status;
                queryItem.Operation = "equal";
            }
            queryCollection[queryItem.FieldType] = queryItem;
            ViewBag.WorkStatus = string.IsNullOrEmpty(work_status) ? "working" : work_status;

            string cn_name = GetFormData("cn_name");
            if (string.IsNullOrEmpty(cn_name) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "cn_name";
                queryItem.SearchValue = cn_name;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string work_id = GetFormData("work_id");
            if (string.IsNullOrEmpty(work_id) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "work_id";
                queryItem.SearchValue = work_id;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string today = GetFormData("entry_date");
            if (string.IsNullOrEmpty(today) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "entry_date";
                queryItem.SearchValue = today;
                queryItem.Operation = "today";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.UserInfoList = UserInfoService.Instance.GetUserInfoList(queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.UsersTotal = total;

            return View();

        }

        #endregion
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// 批量修改状态
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult UpdateUserInfoStatus()
        {
            return View();
        }




        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>

        [AuthorizeFlag]
        public ActionResult EditUser()
        {
            return View();
        }

        /// <summary>
        /// 新建用户信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddUserInfo()
        {
            UserInfoModel model = new UserInfoModel();
           
            model.WorkId ="WORKID_"+ GetFormData("work_id");
            model.CnName = GetFormData("cn_name");
            model.EnName = GetFormData("en_name");
            model.EntryDate = Convert.ToDateTime(GetFormData("entry_date"));
            model.LoginName = "LOGINNAME_" + GetFormData("LoginName");
            model.LoginPwd = GetFormData("login_pwd");
            model.CtiUserId = GetFormData("cti_user_id");
            model.CtiUserPwd = GetFormData("cti_user_pwd");
            model.UserEmail = GetFormData("user_email");
            model.RoleId = GetFormData("rolseid");
            model.TeamName = GetFormData("team_name");
            model.WorkStatus = GetFormData("work_status");
            model.PostName=GetFormData("PostName");
            model.Status = Convert.ToInt32(GetFormData("Status"));
            model.DepartmentId = GetFormData("departmentid");
            if (model.WorkId == null ||
                model.CnName == null ||
                model.EnName == null ||
                model.EntryDate == null ||
                model.LoginName == null ||
                model.LoginPwd == null ||
                model.CtiUserId == null ||
                model.CtiUserPwd == null ||
                model.UserEmail == null ||
                model.RoleId==null||
                model.TeamName == null ||
                model.Status==null||
                model.PostName==null||
                model.WorkStatus == null)
            {
                LogUtil.Debug("新用户信息操作失败，提交数据异常，请与管理员联系");
                return FailedJson("操作失败，提交数据异常，请与管理员联系");
            }
            string message = "";


            if (UserInfoService.Instance.NewUserInfo(model, out message))
            {
                return SuccessedJson(message, "UserMgr_UserList", "UserMgr_UserList", "closeCurrent", "/UserMgr/UserList");
            }
            else
            {
                return FailedJson(message);
            }
        }
        /// <summary>
        /// 批量修改用户状态
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public JsonResult DoUpdateUserInfoStatus()
        {
          
            string  message = "操作失败，请与管理员联系";
            string userId = GetFormData("userId");
            string roleType = GetFormData("roleId");
            string workType = GetFormData("workType");
            string statusType = GetFormData("statusType");

            List<string> userIdList = userId.Split(',').ToList();
            if (userIdList == null && userIdList.Count == 0)
            {
                return FailedJson(message);
            }
            else
            {
                UserInfoModel model = new UserInfoModel();
                if (roleType == "roseId")
                {
                    model.RoleId = GetFormData("rolseid");
                }
                if (workType == "work")
                {
                    model.WorkStatus = GetFormData("work_status");
                }
                if (statusType == "status")
                {
                    model.Status = Convert.ToInt32(GetFormData("status"));
                }
                foreach (string userInfoId in userIdList)
                {
                   
                    model.UserId = userInfoId;
                   
                    UserInfoService.Instance.UpdateUserInfoStatus(model, out message);
                }
            }
            return SuccessedJson(message, "UserMgr_UserList", "UserMgr_UserList", "closeCurrent", "/UserMgr/UserList");
            
          
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateUserInfo()
        {
            UserInfoModel model = new UserInfoModel();
            model.UserId = GetFormData("user_id");
            model.WorkId = "WORKID_" + GetFormData("work_id");
            model.CnName = GetFormData("cn_name");
            model.EnName = GetFormData("en_name");
            model.EntryDate = Convert.ToDateTime(GetFormData("entry_date"));
            model.LoginName = "LOGINNAME_" + GetFormData("LoginName");
            model.LoginPwd = GetFormData("login_pwd");
            model.CtiUserId = GetFormData("cti_user_id");
            model.CtiUserPwd = GetFormData("cti_user_pwd");
            model.UserEmail = GetFormData("user_email");
            model.RoleId = GetFormData("rolseid");
            model.TeamName = GetFormData("team_name");
            model.WorkStatus = GetFormData("work_status");
            model.PostName = GetFormData("PostName");
            model.Status = Convert.ToInt32(GetFormData("Status"));
            model.DepartmentId = GetFormData("departmentid");
            if (UserInfoService.Instance.UpdateUserInfo(model))
            {
                return SuccessedJson("成功更新用户信息", "UserMgr_UserList", "UserMgr_UserList", "closeCurrent", "/UserMgr/UserList");
            }
            else
            {
    
                return FailedJson("操作失败，请与管理员联系");
            }
        }
        
        #endregion

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDelUserInfo()
        {

            string message = "操作失败，请与管理员联系";
            return FailedJson("请与管理员联系删除用户信息。");

            //List<string> idList = Request.Form.GetValues("ids").ToList();

            //if (UserInfoService.Instance.BatchDeleteUserInfoById(idList, out message))
            //{
            //    return SuccessedJson(message, "UserMgr_UserList", "UserMgr_UserList", "forward", "/UserMgr/UserList");
            //}
            //else
            //{
            //    return FailedJson(message);
            //}
        }




        #region 部门信息管理

        /// <summary>
        /// 部门管理。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DepList()
        {
            return View();
        }

        /// <summary>
        /// 部门用户管理。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DepUserList()
        {
            return View();
        }

        /// <summary>
        /// 添加部门成员操作。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public JsonResult DoAddDepartmentUser()
        {
            string idstring = GetFormData("selectedValue");
            string departmentId = GetFormData("depId");

            if (string.IsNullOrEmpty(departmentId))
            {
                return FailedJson("操作失败，请先在左侧选择部门信息");
            }

            if (idstring.Length > 10)
            {
                List<string> arr = idstring.Split('|').ToList();

                if (DepartmentInfoService.Instance.AddDepartmentUser(departmentId, arr))
                {
                    return SuccessedJson("成功添加部门成员", "usermgr_deplist", "usermgr_deplist", "closeCurrent", "/usermgr/deplist?did=" + departmentId);
                }
            }

            return FailedJson("操作失败，请与管理员联系");
        }

        /// <summary>
        /// 删除部门用户操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoRemoveDepartmentUser()
        {
            string removeUserId = Request.QueryString["uid"];
            string departmentId = Request.QueryString["did"];

            if (DepartmentInfoService.Instance.RemoveDepartmentUser(departmentId, removeUserId))
            {
                return SuccessedJson("成功从该部门移除成员", "jbsxBox3", "", "forward", "/UserMgr/DepList?did=" + departmentId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        /// <summary>
        /// 编辑部门信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditDepartment()
        {
            return View();
        }

        /// <summary>
        /// 新建部门。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewDepartment()
        {
            return View();
        }

        /// <summary>
        /// 新建部门信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewDepartment()
        {
            string departmentName = GetFormData("depName");
            string departmentDescription = GetFormData("depDesc");
            string srcDepartmentId = GetFormData("srcDepId");
            DepartmentLevel level = (GetFormData("depLevel") == "1") ? DepartmentLevel.SameLevel : DepartmentLevel.LowerLevel;
            string message = "";

            if (DepartmentInfoService.Instance.CreateNewDepartment(departmentName, departmentDescription, srcDepartmentId, level, out message))
            {
                return SuccessedJson(message, "usermgr_deplist", "usermgr_deplist", "closeCurrent", "");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新部门信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateDepartment()
        {
            string departmentName = GetFormData("depName");
            string departmentDescription = GetFormData("depDesc");
            string departmentId = GetFormData("depId");

            if (DepartmentInfoService.Instance.UpdateDepartmentById(departmentId, departmentName, departmentDescription))
            {
                return SuccessedJson("成功更新部门信息", "usermgr_deplist", "usermgr_deplist", "closeCurrent", "");
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        /// <summary>
        /// 删除部门信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteDepartment()
        {
            string departmentId = GetQueryString("did");
            string resultMessage = "";
            if (DepartmentInfoService.Instance.DeleteDepartmentById(departmentId, out resultMessage))
            {
                return SuccessedJson(resultMessage, "usermgr_deplist", "usermgr_deplist", "", "");
            }
            else
            {
                return FailedJson(resultMessage);
            }
        }

        /// <summary>
        /// 添加部门用户视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddDepUser()
        {
            return View();
        }

        #endregion

        #region 角色权限信息管理

      

        /// <summary>
        /// 权限管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult Premission()
        {
            return View();
        }

        /// <summary>
        /// 角色用户列表视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult RoleUserList()
        {
            return View();
        }

        /// <summary>
        /// 添加角色用户视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddRoleUser()
        {
            return View();
        }

        /// <summary>
        /// 新建角色视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewRole()
        {
            return View();
        }

        /// <summary>
        /// 编辑角色信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditRole()
        {
            return View();
        }

        /// <summary>
        /// 新建角色操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewRole()
        {
            string roleName = GetFormData("roleName");
            string roleDesc = GetFormData("roleDesc");
            List<string> actionIdList = Request.Form.GetValues("chkPmBox").ToList();

            RoleInfoModel model = new RoleInfoModel();
            model.RoleId = Guid.NewGuid().ToString();
            model.RoleName = roleName;
            model.CnName = roleName;
            model.Description = roleDesc;
            model.RoleStatus = 0;

            string message = "";
            if (RoleInfoService.Instance.CreateNewRole(model, actionIdList, out message))
            {
                return SuccessedJson(message, "UserMgr_Premission", "UserMgr_Premission", "closeCurrent", "/usermgr/premission?rid=" + model.RoleId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新角色及权限信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateRole()
        {
            string roleId = GetFormData("roleId");
            string roleName = GetFormData("roleName");
            string roleDesc = GetFormData("roleDesc");
            List<string> actionIdList = Request.Form.GetValues("chkPmBox").ToList();

            RoleInfoModel model = new RoleInfoModel();
            model.RoleId = roleId;
            model.RoleName = roleName;
            model.CnName = roleName;
            model.Description = roleDesc;
            model.RoleStatus = 0;

            string message = "";
            if (RoleInfoService.Instance.UpdateRoleAndPremission(model, actionIdList, out message))
            {
                return SuccessedJson(message, "UserMgr_Premission", "UserMgr_Premission", "closeCurrent", "/usermgr/premission?rid=" + model.RoleId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 删除角色信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteRole()
        {
            string roleId = GetQueryString("rid");
            string message = "";

            if (RoleInfoService.Instance.DeleteRoleById(roleId, out message))
            {
                return SuccessedJson(message, "UserMgr_Premission", "UserMgr_Premission", "forward", "/usermgr/premission");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 添加角色成员操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddRoleUserList()
        {
            string idstring = GetFormData("selectedRoleUser");
            string roleId = GetFormData("roleId");

            if (string.IsNullOrEmpty(roleId))
            {
                return FailedJson("操作失败，请先在左侧选择角色信息");
            }

            if (idstring.Length > 10)
            {
                List<string> arr = idstring.Split('|').ToList();

                if (RoleInfoService.Instance.AddRoleUser(roleId, arr))
                {
                    return SuccessedJson("成功添加角色成员", "UserMgr_Premission", "UserMgr_Premission", "closeCurrent", "/usermgr/premission?rid=" + roleId);
                }
            }

            return FailedJson("操作失败，请与管理员联系");
        }

        /// <summary>
        /// 移除角色成员操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoRemoveRoleUser()
        {
            string removeUserId = Request.QueryString["uid"];
            string roleId = Request.QueryString["rid"];

            if (RoleInfoService.Instance.RemoveRoleUserByUserId(removeUserId))
            {
                return SuccessedJson("成功从该角色移除成员", "roleUserBox", "", "forward", "/UserMgr/Premission?rid=" + roleId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        #endregion

        #region 用户组管理

        /// <summary>
        /// 获取用户组所有用户JSON对象。
        /// </summary>
        /// <returns></returns>
        //[AuthorizeFlag]
        public JsonResult GetGroupUserListJson()
        {
            string groupId = GetQueryString("gid");
            List<UserInfoModel> userList = UserGroupInfoService.Instance.GetGroupUserList(groupId);

            return Json(userList);
        }

        /// <summary>
        /// 用户组管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult UserGroupMgr()
        {
            return View();
        }

        /// <summary>
        /// 新建用户组视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewUserGroup()
        {
            return View();
        }

        /// <summary>
        /// 用户组内用户列表视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult GroupUserList()
        {
            return View();
        }

        /// <summary>
        /// 添加用户组用户视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddUserGroupUser()
        {
            return View();
        }

        /// <summary>
        /// 编辑用户组信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditUserGroup()
        {
            return View();
        }

        /// <summary>
        /// 删除用户组用户操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteUserGroup()
        {
            string message = "操作失败，请与管理员联系";
            string groupId = GetQueryString("gid");

            if (UserGroupInfoService.Instance.DeleteUserGroupInfo(groupId, out message))
            {
                return SuccessedJson(message, "UserMgr_UserGroupMgr", "UserMgr_UserGroupMgr", "forward", "/UserMgr/usergroupmgr?gid" + groupId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 设定本用户组负责人。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSetGroupManagerUser()
        {
            string message = "操作失败，请与管理员联系";
            string groupId = GetQueryString("rid");
            string userId = GetQueryString("uid");

            if (UserGroupInfoService.Instance.SetGroupUserRoleInGroup(1, groupId, userId, out message))
            {
                return SuccessedJson(message, "groupUserBox", "groupUserBox", "forward", "/UserMgr/usergroupmgr?rid" + groupId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新用户组信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateUserGroupInfo()
        {
            string message = "操作失败，请与管理员联系";
            UserGroupInfoModel groupInfo = new UserGroupInfoModel();
            groupInfo.UserGroupId = GetFormData("groupId");
            groupInfo.GroupName = GetFormData("groupName");
            groupInfo.Description = GetFormData("groupDesc");

            List<string> premissionList = (Request.Form["wopBox"] != null) ? Request.Form.GetValues("wopBox").ToList() : null;
            //List<string> premissionList = Request.Form.GetValues("wopBox").ToList();
            if (premissionList == null)
            {
                return FailedJson("请选择相相对应权限");
            }
            if (UserGroupInfoService.Instance.UpdateUserGroupInfo(groupInfo, premissionList, out message))
            {
                return SuccessedJson(message, "UserMgr_UserGroupMgr", "UserMgr_UserGroupMgr", "forward", "/UserMgr/usergroupmgr?gid" + groupInfo.UserGroupId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 移除用户组内用户操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoRemoveGroupUser()
        {
            string message = "操作失败，请与管理员联系";
            string groupId = GetQueryString("rid");
            string userId = GetQueryString("uid");

            if (UserGroupInfoService.Instance.RemoveGroupUserById(groupId, userId, out message))
            {
                return SuccessedJson(message, "groupUserBox", "groupUserBox", "forward", "/UserMgr/usergroupmgr?rid" + groupId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 添加用户组用户操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddGroupUser()
        {
            string idstring = GetFormData("selectedGroupUser");
            string groupId = GetFormData("groupId");
            string message = "操作失败，请与管理员联系";

            if (string.IsNullOrEmpty(groupId))
            {
                return FailedJson("操作失败，请先在左侧选择用户组信息");
            }

            if (idstring.Length > 10)
            {
                List<string> arr = idstring.Split('|').ToList();

                if (UserGroupInfoService.Instance.BatchAddGroupUser(groupId,arr,out message))
                {
                    return SuccessedJson(message, "UserMgr_UserGroupMgr", "UserMgr_UserGroupMgr", "closeCurrent", "/usermgr/usergroupmgr?rid=" + groupId);
                }
            }

            return FailedJson("操作失败，请与管理员联系");
        }

        /// <summary>
        /// 新建用户组操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewUserGroupInfo()
        {
            string message = "操作失败，请与管理员联系";
            UserGroupInfoModel groupInfo = new UserGroupInfoModel();
            groupInfo.GroupName = GetFormData("groupName");
            groupInfo.Description = GetFormData("groupDesc");
            List<string> premissionList = (Request.Form["wopBox"] != null) ? Request.Form.GetValues("wopBox").ToList() : null;

            if (premissionList == null)
            {
                return FailedJson("请选择相相对应权限");
            }
            if (UserGroupInfoService.Instance.CreateNewGroupInfo(groupInfo, premissionList, out message))
            {
                return SuccessedJson(message, "UserMgr_UserGroupMgr", "UserMgr_UserGroupMgr", "closeCurrent", "/UserMgr/usergroupmgr?gid" + groupInfo.UserGroupId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        #endregion





    }
}
