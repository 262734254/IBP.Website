/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-14
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
	/// WorkorderTypeInfo业务逻辑类
	/// </summary>
	public partial class WorkorderTypeInfoService : BaseService
	{
		// 在此添加你的代码...

        /// <summary>
        /// 获取工单状态名称字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetWorkorderStatusNameList(bool clear)
        {
            string cacheKey = CacheKey.WORKORDER_STATUSNAME_DICT;
            Dictionary<string, string> dict = CacheUtil.Get<Dictionary<string, string>>(cacheKey);
            if (dict == null || clear)
            {
                dict = new Dictionary<string, string>();
                Dictionary<string, WorkorderTypeInfoModel> typeList = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(false);
                foreach (WorkorderTypeInfoModel typeItem in typeList.Values)
                {
                    WorkOrderTypeDomainModel typeDomainModel = GetTypeDomainModelById(typeItem.WorkorderTypeId, false);
                    foreach (WorkorderStatusInfoModel statusModel in typeDomainModel.StatusList.Values)
                    {
                        dict[statusModel.WorkorderStatusId] = statusModel.StatusName;
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// 获取工单结果名称字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetWorkorderResultNameList(bool clear)
        {
            string cacheKey = CacheKey.WORKORDER_RESULTNAME_DICT;
            Dictionary<string, string> dict = CacheUtil.Get<Dictionary<string, string>>(cacheKey);
            if (dict == null || clear)
            {
                dict = new Dictionary<string, string>();
                Dictionary<string, WorkorderTypeInfoModel> typeList = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(false);
                foreach (WorkorderTypeInfoModel typeItem in typeList.Values)
                {
                    WorkOrderTypeDomainModel typeDomainModel = GetTypeDomainModelById(typeItem.WorkorderTypeId, false);
                    foreach (WorkorderResultInfoModel resultModel in typeDomainModel.ResultList.Values)
                    {
                        dict[resultModel.WorkorderResultId] = resultModel.ResultName;
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// 下移工单类型状态值。
        /// </summary>
        /// <param name="typeStatusId"></param>
        /// <returns></returns>
        public bool MoveDownTypeStatusSortOrder(string typeId, string typeStatusId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderTypeDomainModel typeInfo = GetTypeDomainModelById(typeId, false);

            if (typeInfo == null)
            {
                message = "操作失败，该工单类型不存在";
                return false;
            }

            if (typeInfo.StatusList == null || typeInfo.StatusList.ContainsKey(typeStatusId) == false)
            {
                message = "操作失败，该工单类型状态值不存在";
                return false;
            }
            WorkorderStatusInfoModel statusInfo = typeInfo.StatusList[typeStatusId];

            if (statusInfo.SortOrder == typeInfo.StatusList.Count)
            {
                message = "操作失败，该工单类型状态值已为最下序列号";
                return false;
            }

            WorkorderStatusInfoModel downValueInfo = null;
            foreach (WorkorderStatusInfoModel item in typeInfo.StatusList.Values)
            {
                if (item.SortOrder == statusInfo.SortOrder + 1)
                {
                    downValueInfo = item;
                }
            }

            if (downValueInfo != null)
            {
                string sql = @"UPDATE workorder_status_info SET sort_order = $sort_order$ WHERE workorder_status_id = $workorder_status_id$";

                try
                {
                    BeginTransaction();
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("workorder_status_id", statusInfo.WorkorderStatusId);
                    pc.Add("sort_order", statusInfo.SortOrder + 1);

                    if (ExecuteNonQuery(sql, pc) > 0)
                    {
                        pc.Clear();
                        pc.Add("workorder_status_id", downValueInfo.WorkorderStatusId);
                        pc.Add("sort_order", downValueInfo.SortOrder - 1);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                            GetTypeDomainModelById(typeId, true);
                            message = "成功下移该工单类型状态值";
                            return true;
                        }
                    }

                    RollbackTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("下移工单类型状态值排序索引异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        public bool MoveDownTypeResultSortOrder(string typeId, string typeResultId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderTypeDomainModel typeInfo = GetTypeDomainModelById(typeId, false);

            if (typeInfo == null)
            {
                message = "操作失败，该工单类型不存在";
                return false;
            }

            if (typeInfo.ResultList == null || typeInfo.ResultList.ContainsKey(typeResultId) == false)
            {
                message = "操作失败，该工单类型处理结果不存在";
                return false;
            }
            WorkorderResultInfoModel resultInfo = typeInfo.ResultList[typeResultId];

            if (resultInfo.SortOrder == typeInfo.ResultList.Count)
            {
                message = "操作失败，该工单类型处理结果已为最下序列号";
                return false;
            }

            WorkorderResultInfoModel downValueInfo = null;
            foreach (WorkorderResultInfoModel item in typeInfo.ResultList.Values)
            {
                if (item.SortOrder == resultInfo.SortOrder + 1)
                {
                    downValueInfo = item;
                }
            }

            if (downValueInfo != null)
            {
                string sql = @"UPDATE workorder_result_info SET sort_order = $sort_order$ WHERE workorder_result_id = $workorder_result_id$";

                try
                {
                    BeginTransaction();
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("workorder_result_id", resultInfo.WorkorderResultId);
                    pc.Add("sort_order", resultInfo.SortOrder + 1);

                    if (ExecuteNonQuery(sql, pc) > 0)
                    {
                        pc.Clear();
                        pc.Add("workorder_result_id", downValueInfo.WorkorderResultId);
                        pc.Add("sort_order", downValueInfo.SortOrder - 1);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                            GetTypeDomainModelById(typeId, true);
                            message = "成功下移该工单类型处理结果";
                            return true;
                        }
                    }

                    RollbackTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("下移工单类型处理结果排序索引异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// 上移工单类型处理结果值。
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="typeResultId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool MoveUpTypeResultSortOrder(string typeId, string typeResultId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderTypeDomainModel typeInfo = GetTypeDomainModelById(typeId, false);

            if (typeInfo == null)
            {
                message = "操作失败，该工单类型不存在";
                return false;
            }

            if (typeInfo.ResultList == null || typeInfo.ResultList.ContainsKey(typeResultId) == false)
            {
                message = "操作失败，该工单类型处理结果不存在";
                return false;
            }
            WorkorderResultInfoModel resultInfo = typeInfo.ResultList[typeResultId];

            if (resultInfo.SortOrder == typeInfo.ResultList.Count)
            {
                message = "操作失败，该工单类型处理结果已为最上序列号";
                return false;
            }

            WorkorderResultInfoModel upValueInfo = null;
            foreach (WorkorderResultInfoModel item in typeInfo.ResultList.Values)
            {
                if (item.SortOrder == resultInfo.SortOrder - 1)
                {
                    upValueInfo = item;
                }
            }

            if (upValueInfo != null)
            {
                string sql = @"UPDATE workorder_result_info SET sort_order = $sort_order$ WHERE workorder_result_id = $workorder_result_id$";

                try
                {
                    BeginTransaction();
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("workorder_result_id", resultInfo.WorkorderResultId);
                    pc.Add("sort_order", resultInfo.SortOrder - 1);

                    if (ExecuteNonQuery(sql, pc) > 0)
                    {
                        pc.Clear();
                        pc.Add("workorder_result_id", upValueInfo.WorkorderResultId);
                        pc.Add("sort_order", upValueInfo.SortOrder + 1);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                            GetTypeDomainModelById(typeId, true);
                            message = "成功上移该工单类型处理结果";
                            return true;
                        }
                    }

                    RollbackTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("上移工单类型处理结果排序索引异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// 上移工单类型状态值。
        /// </summary>
        /// <param name="typeStatusId"></param>
        /// <returns></returns>
        public bool MoveUpTypeStatusSortOrder(string typeId, string typeStatusId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderTypeDomainModel typeInfo = GetTypeDomainModelById(typeId, false);

            if (typeInfo == null)
            {
                message = "操作失败，该工单类型不存在";
                return false;
            }

            if (typeInfo.StatusList == null || typeInfo.StatusList.ContainsKey(typeStatusId) == false)
            {
                message = "操作失败，该工单类型状态值不存在";
                return false;
            }
            WorkorderStatusInfoModel statusInfo = typeInfo.StatusList[typeStatusId];

            if (statusInfo.SortOrder == 1)
            {
                message = "操作失败，该工单类型状态值已为最上序列号";
                return false;
            }

            WorkorderStatusInfoModel upValueInfo = null;
            foreach (WorkorderStatusInfoModel item in typeInfo.StatusList.Values)
            {
                if (item.SortOrder == statusInfo.SortOrder - 1)
                {
                    upValueInfo = item;
                }
            }

            if (upValueInfo != null)
            {
                string sql = @"UPDATE workorder_status_info SET sort_order = $sort_order$ WHERE workorder_status_id = $workorder_status_id$";

                try
                {
                    BeginTransaction();
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("workorder_status_id", statusInfo.WorkorderStatusId);
                    pc.Add("sort_order", statusInfo.SortOrder - 1);

                    if (ExecuteNonQuery(sql, pc) > 0)
                    {
                        pc.Clear();
                        pc.Add("workorder_status_id", upValueInfo.WorkorderStatusId);
                        pc.Add("sort_order", upValueInfo.SortOrder + 1);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                            GetTypeDomainModelById(typeId, true);
                            message = "成功上移该工单类型状态值";
                            return true;
                        }
                    }

                    RollbackTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("上移工单类型状态值排序索引异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// 新建工单处理结果值。
        /// </summary>
        /// <param name="resultInfo"></param>
        /// <returns></returns>
        public bool CreateTypeResultInfo(WorkorderResultInfoModel resultInfo)
        {
            bool result = false;
            WorkOrderTypeDomainModel domain = GetTypeDomainModelById(resultInfo.WorkorderTypeId, false);
            if (domain == null)
            {
                return false;
            }

            resultInfo.SortOrder = domain.ResultList.Count + 1;

            if (WorkorderResultInfoService.Instance.Create(resultInfo) > 0)
            {
                result = true;
                GetTypeDomainModelById(resultInfo.WorkorderTypeId, true);
            }

            return result;
        }
        /// <summary>
        /// 修改工单处理结果值。
        /// </summary>
        /// <param name="resultInfo"></param>
        /// <returns></returns>
        public bool UpdateTypeResultInfo(WorkorderResultInfoModel resultInfo)
        {
            bool result = false;
            WorkOrderTypeDomainModel domain = GetTypeDomainModelById(resultInfo.WorkorderTypeId, false);
            if (domain == null)
            {
                return false;
            }

           

            if (WorkorderResultInfoService.Instance.Update(resultInfo) > 0)
            {
                result = true;
                GetTypeDomainModelById(resultInfo.WorkorderTypeId, true);
            }

            return result;
        }
        /// <summary>
        /// 编辑工单类型状态操作。
        /// </summary>
        /// <param name="statusInfo"></param>
        /// <returns></returns>
        public bool EditTypeStatusInfo(WorkorderStatusInfoModel statusInfo)
        {
            bool result = false;

            WorkOrderTypeDomainModel domain = GetTypeDomainModelById(statusInfo.WorkorderTypeId, false);
            if (domain == null)
            {
                return false;
            }

        

            if (WorkorderStatusInfoService.Instance.Update(statusInfo) > 0)
            {
                result = true;
                GetTypeDomainModelById(statusInfo.WorkorderTypeId, true);
            }

            return result;
        }

        /// <summary>
        /// 新建工单类型状态值。
        /// </summary>
        /// <param name="statusInfo"></param>
        /// <returns></returns>
        public bool CreateTypeStatusInfo(WorkorderStatusInfoModel statusInfo)
        {
            bool result = false;

            WorkOrderTypeDomainModel domain = GetTypeDomainModelById(statusInfo.WorkorderTypeId, false);
            if (domain == null)
            {
                return false;
            }

            statusInfo.SortOrder = domain.StatusList.Count + 1;

            if (WorkorderStatusInfoService.Instance.Create(statusInfo) > 0)
            {
                result = true;
                GetTypeDomainModelById(statusInfo.WorkorderTypeId, true);
            }

            return result;
        }

        public bool DeleteTypeStatusInfo(string typeId, string statusId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            try
            {
                BeginTransaction();

                if (WorkorderStatusInfoService.Instance.Delete(statusId) > 0)
                {
                    WorkOrderTypeDomainModel dataInfo = GetTypeDomainModelById(typeId, true);
                    string sql = @"UPDATE workorder_status_info SET sort_order = $sort_order$ WHERE workorder_status_id = $workorder_status_id$";
                    ParameterCollection pc = new ParameterCollection();

                    int index = 1;
                    foreach (WorkorderStatusInfoModel item in dataInfo.StatusList.Values)
                    {
                        item.SortOrder = index;
                        index++;

                        pc.Clear();
                        pc.Add("workorder_status_id", item.WorkorderStatusId);
                        pc.Add("sort_order", item.SortOrder);

                        if (ExecuteNonQuery(sql, pc) != 1)
                        {
                            RollbackTransaction();
                            return false;
                        }

                        // TASK:检查是否有该工单类型状态值引用
                    }

                    CommitTransaction();
                    message = "成功删除指定工单类型状态值";
                    GetTypeDomainModelById(typeId, true);
                    result = true;
                }

                RollbackTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除工单类型状态值异常", ex);
                throw ex;
            }

            return result;
        }

        public bool DeleteTypeResultInfo(string typeId, string resultId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            try
            {
                BeginTransaction();

                if (WorkorderResultInfoService.Instance.Delete(resultId) > 0)
                {
                    WorkOrderTypeDomainModel dataInfo = GetTypeDomainModelById(typeId, true);
                    string sql = @"UPDATE workorder_result_info SET sort_order = $sort_order$ WHERE workorder_result_id = $workorder_result_id$";
                    ParameterCollection pc = new ParameterCollection();

                    int index = 1;
                    foreach (WorkorderResultInfoModel item in dataInfo.ResultList.Values)
                    {
                        item.SortOrder = index;
                        index++;

                        pc.Clear();
                        pc.Add("workorder_result_id", item.WorkorderResultId);
                        pc.Add("sort_order", item.SortOrder);

                        if (ExecuteNonQuery(sql, pc) != 1)
                        {
                            RollbackTransaction();
                            return false;
                        }

                        // TASK:检查是否有该工单类型状态值引用
                    }

                    CommitTransaction();
                    message = "成功删除指定工单类型处理结果值";
                    GetTypeDomainModelById(typeId, true);
                    result = true;
                }

                RollbackTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除工单类型处理结果值异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 根据ID获取工单类型领域模型。
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public WorkOrderTypeDomainModel GetTypeDomainModelById(string typeId, bool clear)
        {
            if (string.IsNullOrEmpty(typeId) || typeId == "All")
            {
                return null;
            }

            string cacheKey = CacheKey.WORKORDER_TYPE_DOMAINMODEL.GetKeyDefine(typeId);

            WorkOrderTypeDomainModel model = CacheUtil.Get<WorkOrderTypeDomainModel>(cacheKey);
            if (model == null || clear)
            {
                model = GetWorkOrderTypeDomainModelFromDatabase(typeId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
                //else
                //{
                //    CacheUtil.Remove(cacheKey);
                //}
            }

            return model;
        }

        /// <summary>
        /// 根据ID从数据库获取工单类型领域模型。
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public WorkOrderTypeDomainModel GetWorkOrderTypeDomainModelFromDatabase(string typeId)
        {
            WorkOrderTypeDomainModel result = null;

            WorkorderTypeInfoModel typeInfo = Retrieve(typeId);
            if (typeInfo != null)
            {
                result = new WorkOrderTypeDomainModel();
                result.StatusList = new Dictionary<string, WorkorderStatusInfoModel>();
                result.ResultList = new Dictionary<string, WorkorderResultInfoModel>();
               
                result.TypeInfo = typeInfo;

                ParameterCollection pc = new ParameterCollection();
                pc.Add("workorder_type_id", typeId);

                List<WorkorderStatusInfoModel> statusList = WorkorderStatusInfoService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("sort_order", "asc"));
                if (statusList != null && statusList.Count > 0)
                {
                    foreach (WorkorderStatusInfoModel status in statusList)
                    {
                        if (status.StatusTag == 0)
                        {
                            result.BeginStatusInfo = status;
                        }
                        if (status.StatusTag == 2)
                        {
                            result.EndStatusInfo = status;
                        }

                        if (status.CustomStatus == "待审批")
                        {
                            result.ApprovalStatusInfo = status;
                        }

                        if (status.CustomStatus == "待质检")
                        {
                            result.QuilityCheckedStatusInfo = status;
                        }

                        result.StatusList.Add(status.WorkorderStatusId, status);
                    }
                }

                List<WorkorderResultInfoModel> resultList = WorkorderResultInfoService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("sort_order", "asc"));
                if (resultList != null && resultList.Count > 0)
                {
                    foreach (WorkorderResultInfoModel resultItem in resultList)
                    {
                        if (resultItem.IsBegin == 0)
                        {
                            result.BeginResultInfo = resultItem;
                        }

                        result.ResultList.Add(resultItem.WorkorderResultId, resultItem);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取工单类型字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, WorkorderTypeInfoModel> GetWorkOrderDictionary(bool clear)
        {
            string cacheKey = CacheKey.WORKORDER_TYPE_DICT;

            Dictionary<string, WorkorderTypeInfoModel> dict = CacheUtil.Get<Dictionary<string, WorkorderTypeInfoModel>>(cacheKey);
            if (dict == null || clear)
            {
                dict = GetWorkOrderDictionaryFromDatabase();
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

        public WorkorderTypeInfoModel GetWorkorderTypeInfoModelByName(string workorderTypeName)
        {
            WorkorderTypeInfoModel result = null;
            Dictionary<string, WorkorderTypeInfoModel> dict = GetWorkOrderDictionary(false);
            if (dict != null)
            {
                foreach (WorkorderTypeInfoModel item in dict.Values)
                {
                    if (item.TypeName == workorderTypeName)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        public WorkorderTypeInfoModel GetWorkorderTypeInfoModelById(string workorderTypeId)
        {
            WorkorderTypeInfoModel result = null;
            Dictionary<string, WorkorderTypeInfoModel> dict = GetWorkOrderDictionary(false);
            if (dict != null)
            {
                foreach (WorkorderTypeInfoModel item in dict.Values)
                {
                    if (item.WorkorderTypeId == workorderTypeId)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从数据库获取工单类型字典。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, WorkorderTypeInfoModel> GetWorkOrderDictionaryFromDatabase()
        {
            Dictionary<string, WorkorderTypeInfoModel> result = null;

            List<WorkorderTypeInfoModel> list = RetrieveMultiple(new ParameterCollection(), OrderByCollection.Create("sort_order", "asc"));
            if (list != null && list.Count > 0)
            {
                result = new Dictionary<string, WorkorderTypeInfoModel>();
                foreach (WorkorderTypeInfoModel model in list)
                {
                    result.Add(model.WorkorderTypeId, model);
                }
            }

            return result;
        }

        public WorkOrderTypeTreeDomainModel GetWorkOrderTypeTree(bool clearCache)
        {
            string cacheKey = CacheKey.WORKORDER_TYPE_TREE;
            WorkOrderTypeTreeDomainModel tree = CacheUtil.Get<WorkOrderTypeTreeDomainModel>(cacheKey);

            if (tree == null || clearCache)
            {
                tree = GetWorkOrderTypeTreeFromDatabase();
                if (tree != null)
                {
                    CacheUtil.Set(cacheKey, tree);
                }
            }

            return tree;
        }

        public WorkOrderTypeTreeDomainModel GetWorkOrderTypeTreeFromDatabase()
        {
            WorkOrderTypeTreeDomainModel tree = new WorkOrderTypeTreeDomainModel();
            tree.BasicInfo = new WorkorderTypeInfoModel();
            tree.BasicInfo.TypeName = "根节点";
            tree.BasicInfo.WorkorderTypeId = null;
            tree.BasicInfo.ParentId = null;

            GetWorkOrderTypeByParentIdFromDatabase(tree, null);

            return tree;
        }

        public void GetWorkOrderTypeByParentIdFromDatabase(WorkOrderTypeTreeDomainModel tree, string parentId)
        {
            List<WorkorderTypeInfoModel> childList = GetWorkOrderTypeListByParentIdFromDatabase(parentId);

            if (childList != null && childList.Count > 0)
            {
                tree.ChildTypeList = new Dictionary<string, WorkOrderTypeTreeDomainModel>();
                WorkOrderTypeTreeDomainModel childInfo = null;

                foreach (WorkorderTypeInfoModel item in childList)
                {
                    childInfo = new WorkOrderTypeTreeDomainModel();
                    childInfo.BasicInfo = item;

                    GetWorkOrderTypeByParentIdFromDatabase(childInfo, item.WorkorderTypeId);

                    tree.ChildTypeList[childInfo.BasicInfo.WorkorderTypeId] = childInfo;
                }
            }
            else
            {
                tree.ChildTypeList = null;
            }
        }

 
        public List<WorkorderTypeInfoModel> GetWorkOrderTypeListByParentIdFromDatabase(string parentId)
        {
            List<WorkorderTypeInfoModel> list = null;

            string sql = "select * from workorder_type_info where parent_id = $ParentId$ order by sort_order asc";
            if (parentId == null)
            {
                sql = "select * from workorder_type_info where parent_id is null order by sort_order asc";
                list = ModelConvertFrom<WorkorderTypeInfoModel>(ExecuteDataTable(sql));
            }
            else
            {
                ParameterCollection pc = new ParameterCollection();
                pc.Add("ParentId", parentId);

                list = ModelConvertFrom<WorkorderTypeInfoModel>(ExecuteDataTable(sql, pc));
            }

            return list;
        }
	}
}

