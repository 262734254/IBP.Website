/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-3
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
	/// DepartmentInfo业务逻辑类
	/// </summary>
	public partial class DepartmentInfoService : BaseService
	{
        // 在此添加你的代码...


        /// <summary>
        /// 更新部门信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool UpdateDepartmentById(string departmentId, string departmentName, string description)
        {
            bool result = false;
            DepartmentInfoModel model = Retrieve(departmentId);

            if (model != null)
            {
                model.DepartmentName = departmentName;
                model.Description = description;

                if (Update(model) > 0)
                {
                    GetDepartmentTree(true);
                    result = true;
                }
            }

            return result;
        }


        /// <summary>
        /// 根据ID删除部门信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteDepartmentById(string departmentId, out string message)
        {
            bool result = false;
            message = "成功删除该部门";

            DepartmentDomainModel model = GetDepartmentDomainInfoById(departmentId);

            if (model == null)
            {
                message = "该部门信息不存在";
                return false;
            }

            if (model.ChildDepartment != null && model.ChildDepartment.Count > 0)
            {
                message = "操作失败，该部门包含下级部门，请移除后再试";
                return false;
            }

            if (model.MemberTotal > 0)
            {
                message = "操作失败，该部门包含成员用户，请移除后再试";
                return false;
            }

            try
            {
                BeginTransaction();

                string sql = @"
UPDATE user_info SET
    department_id = NULL
WHERE
    department_id = $department_id$
";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("department_id", departmentId);

                if (Delete(departmentId) > 0)
                {
                    result = ExecuteNonQuery(sql, pc) >= 0;

                    if (result)
                    {
                        CommitTransaction();
                        GetDepartmentTree(true);
                    }
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除部门信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 检查部门名称是否存在。
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        private bool ExistDepartmentName(string departmentName)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    department_info 
WHERE 
    department_name = $department_name$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("department_name", departmentName);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }

        /// <summary>
        /// 创建部门信息。
        /// </summary>
        /// <param name="departmentName">部门名称。</param>
        /// <param name="description">部门描述。</param>
        /// <param name="srcDepartmentId">源部门ID。</param>
        /// <param name="level">级别：1同级部门，2下级部门。</param>
        /// <returns></returns>
        public bool CreateNewDepartment(string departmentName, string description, string srcDepartmentId, DepartmentLevel level, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (ExistDepartmentName(departmentName))
            {
                message = string.Format("操作失败，已经存在名为【{0}】的部门", departmentName);
                return false;
            }

            string sql = @"
INSERT INTO [department_info]
    ([department_id],
    [parent_id],
    [department_name],
    [description],
    [sort_order],
    [created_on],
    [created_by],
    [modified_on],
    [modified_by],
    [status_code])
VALUES
    (NEWID(),
    $parendId$,
    $departmentName$,
    $description$,
    0,
    GETDATE(),
    $createdBy$,
    NULL,
    NULL,
    0)
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("departmentName", departmentName);
            pc.Add("description", description);
            pc.Add("createdBy", SessionUtil.Current.UserId);

            DepartmentDomainModel srcDepartmentInfo = GetDepartmentDomainInfoById(srcDepartmentId);

            if (srcDepartmentInfo == null)
            {
                pc.Add("parendId", DBNull.Value);
            }
            else
            {
                switch (level)
                {
                    case DepartmentLevel.SameLevel:
                        if (srcDepartmentInfo.ParentId == null)
                        {
                            pc.Add("parendId", DBNull.Value);
                        }
                        else
                        {
                            pc.Add("parentId", srcDepartmentInfo.ParentId);
                        }
                        break;
                    case DepartmentLevel.LowerLevel:
                        pc.Add("parendId", srcDepartmentInfo.DepartmentId);
                        break;
                    default:
                        break;
                }
            }

            result = ExecuteNonQuery(sql, pc) > 0;

            if (result)
            {
                GetDepartmentTree(true);
                message = "成功创建部门信息";
            }

            return result;
        }

        /// <summary>
        /// 批量增加部门成员。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        public bool AddDepartmentUser(string departmentId, List<string> userIdList)
        {
            bool result = false;

            if (string.IsNullOrEmpty(departmentId))
            {
                return false;
            }

            if (userIdList != null && userIdList.Count > 0)
            {
                string sql = @"
UPDATE 
    user_info
SET
    department_id = $department_id$
WHERE
    user_id = $user_id$
";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("department_id", departmentId);
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
                    LogUtil.Error("批量增加部门成员异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取部门成员信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<UserInfoModel> GetDepartmentUserList(string departmentId)
        {
            string sql = "SELECT * FROM user_info WHERE department_id = $department_id$ AND work_status <> $work_status$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("department_id", departmentId);
            pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态",false).GetCustomDataValueDomainByDataValue("离职").ValueId);

            return ModelConvertFrom<UserInfoModel>(ExecuteDataTable(sql, pc));
        }

        /// <summary>
        /// 获取未定义隶属部门的用户列表。
        /// </summary>
        /// <returns></returns>
        public List<UserInfoModel> GetUnDefineDepartmentUserList()
        {
            string sql = @"SELECT * FROM user_info WHERE department_id IS NULL AND work_status <> $work_status$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("work_status", CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工作状态", false).GetCustomDataValueDomainByDataValue("离职").ValueId);

            return ModelConvertFrom<UserInfoModel>(ExecuteDataTable(sql, pc));
        }

        /// <summary>
        /// 根据ID从缓存中获取部门领域模型。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public DepartmentDomainModel GetDepartmentDomainInfoById(string departmentId)
        {
            DepartmentDomainModel tree = GetDepartmentTree(false);

            return GetDepartmentDomainModelFromTree(tree, departmentId);
        }

        /// <summary>
        /// 移除部门用户。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveDepartmentUser(string departmentId, string userId)
        {
            bool result = false;

            string sql = @"
UPDATE 
    user_info
SET
    department_id = null
WHERE
    user_id = $user_id$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("user_id", userId);

            result = ExecuteNonQuery(sql, pc) > 0;
            if (result)
            {
                GetDepartmentTree(true);
            }

            return result;
        }

        /// <summary>
        /// 获取部门结构树。
        /// </summary>
        /// <param name="clearCache"></param>
        /// <returns></returns>
        public DepartmentDomainModel GetDepartmentTree(bool clearCache)
        {
            string cacheKey = CacheKey.DEPARTMENT_TREE;
            DepartmentDomainModel tree = CacheUtil.Get<DepartmentDomainModel>(cacheKey);

            if (tree == null || clearCache)
            {
                tree = GetDepartmentTreeFromDatabase();
                if (tree != null)
                {
                    CacheUtil.Set(cacheKey, tree);
                }
            }

            return tree;
        }


        /// <summary>
        /// 根据部门ID从缓存的部门结构树中获取实体。
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        private DepartmentDomainModel GetDepartmentDomainModelFromTree(DepartmentDomainModel tree, string departmentId)
        {
            DepartmentDomainModel result = null;
            if (tree.DepartmentId == departmentId)
            {
                return tree;
            }

            if (tree.ChildDepartment != null && tree.ChildDepartment.Count > 0)
            {
                foreach (DepartmentDomainModel child in tree.ChildDepartment.Values)
                {
                    result = GetDepartmentDomainModelFromTree(child, departmentId);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 从数据库获取部门结构树。
        /// </summary>
        /// <returns></returns>
        private DepartmentDomainModel GetDepartmentTreeFromDatabase()
        {
            DepartmentDomainModel tree = new DepartmentDomainModel();
            tree.DepartmentName = "根节点";
            tree.DepartmentId = null;
            tree.ParentId = null;

            GetDepartmentTreeByParentIdFromDatabase(tree, null);

            return tree;
        }

        /// <summary>
        /// 从数据库获取部门结构树。
        /// </summary>
        /// <returns></returns>
        private void GetDepartmentTreeByParentIdFromDatabase(DepartmentDomainModel tree, string parentId)
        {
            List<DepartmentInfoModel> list = GetDepartmentInfoByParentIdFromDatabase(parentId);

            if (list != null && list.Count > 0)
            {
                tree.ChildDepartment = new Dictionary<string, DepartmentDomainModel>();

                DepartmentDomainModel node = null;
                foreach (DepartmentInfoModel item in list)
                {
                    node = new DepartmentDomainModel();
                    node.DepartmentId = item.DepartmentId;
                    node.MemberTotal = GetDepartmentMemberTotal(item.DepartmentId);
                    node.ParentId = item.ParentId;
                    node.DepartmentName = item.DepartmentName;
                    node.Description = item.Description;
                    node.ChildDepartment = new Dictionary<string, DepartmentDomainModel>();

                    GetDepartmentTreeByParentIdFromDatabase(node, node.DepartmentId);

                    tree.ChildDepartment.Add(node.DepartmentId, node);
                }
            }
            else
            {
                tree.ChildDepartment = null;
            }
        }

        /// <summary>
        /// 获取指定部门成员总数。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        private int GetDepartmentMemberTotal(string departmentId)
        {
            int result = 0;
            string sql = @"
SELECT 
    COUNT(1)
FROM
    user_info
WHERE
    department_id = $department_id$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("department_id", departmentId);

            result = Convert.ToInt32(ExecuteScalar(sql, pc));

            return result;
        }

        /// <summary>
        /// 根据ID从数据库中获取部门信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        private DepartmentInfoModel GetDepartmentInfoByIdFromDatabase(string departmentId)
        {
            return Retrieve(departmentId);
        }

        /// <summary>
        /// 根据上级部门ID获取部门列表。
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<DepartmentInfoModel> GetDepartmentInfoByParentIdFromDatabase(string parentId)
        {
            List<DepartmentInfoModel> list = null;
            string sql = null;
            DataTable dt = null;

            if (string.IsNullOrEmpty(parentId))
            {
                sql = @"
SELECT * 
FROM
    department_info 
WHERE
    parent_id IS NULL
ORDER BY
    sort_order asc
";
              dt = ExecuteDataTable(sql);                
            }
            else
            {
                sql = @"
SELECT * 
FROM
    department_info 
WHERE
    parent_id = $parent_id$
ORDER BY
    sort_order asc
";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("parent_id", parentId);

                dt = ExecuteDataTable(sql, pc);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<DepartmentInfoModel>();
                DepartmentInfoModel model = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new DepartmentInfoModel();
                    ModelConvertFrom(model, dt, i);
                    list.Add(model);
                }
            }

            return list;
        }
	}
}

