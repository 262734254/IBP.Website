/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-27
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
	/// SalesPackageInfo业务逻辑类
	/// </summary>
	public partial class SalesPackageInfoService : BaseService
	{
		// 在此添加你的代码...

        public SalePackageDomainModel GetSalePackageDomainModelById(string salePackageId, bool clear)
        {
            if (string.IsNullOrEmpty(salePackageId))
                return null;

            string cacheKey = CacheKey.SALE_PACKAGE_DOMAINMODEL.GetKeyDefine(salePackageId);
            SalePackageDomainModel result = CacheUtil.Get<SalePackageDomainModel>(cacheKey);

            if (result == null || clear)
            {
                result = GetSalePackageDomainModelByIdFromDatabase(salePackageId);

                if (result != null)
                {
                    CacheUtil.Set(cacheKey, result);
                }
            }

            return result;
        }

        public SalePackageDomainModel GetSalePackageDomainModelByIdFromDatabase(string salePackageId)
        {
            if (salePackageId == null)
                return null;

            SalePackageDomainModel result = null;
            SalesPackageInfoModel basicInfo = Retrieve(salePackageId);
            if (basicInfo != null)
            {
                string sql = "SELECT * FROM [product_sales_group_info] WHERE sale_package_id = $sale_package_id$";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("sale_package_id", salePackageId);

                DataTable dt = ExecuteDataTable(sql, pc);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = new SalePackageDomainModel();
                    result.BasicInfo = basicInfo;
                    result.ProductCategoryList = ModelConvertFrom<ProductSalesGroupInfoModel>(dt, "product_category_id");
                }
            }

            return result;
        }

        public bool BatchDeleteSalePackages(List<string> salePackageIdList, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (salePackageIdList == null || salePackageIdList.Count == 0)
            {
                message = "缺少营销项目信信息ID参数，请检查输入";
                return false;
            }

            try
            {
                int failed = 0;
                ParameterCollection pc = new ParameterCollection();
                string sql = "";
                foreach (string packageId in salePackageIdList)
                {
                    BeginTransaction();
                    if (Delete(packageId) == 1)
                    {
                        sql = "DELETE FROM [product_sales_group_info] WHERE sale_package_id = $sale_package_id$";
                        pc.Clear();
                        pc.Add("sale_package_id", packageId);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                        }
                        else
                        {
                            failed++;
                            RollbackTransaction();
                        }
                    }
                    else
                    {
                        failed++;
                        RollbackTransaction();
                    }
                }

                message = (failed == 0) ? "成功删除选中营销项目信息" : "操作成功，部分关联订单的营销项目未能删除";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除营销项目信息异常", ex);
                message = "删除营销项目信息异常";
                throw ex;
            }

            return result;
        }

        public bool UpdateSalePackageInfo(SalesPackageInfoModel packageInfo, List<string> productCategoryList, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            if (productCategoryList == null || productCategoryList.Count < 1)
            {
                message = "营销项目至少包含一个产品类型，请检查输入";
                return false;
            }

            try
            {
                BeginTransaction();

                if (Update(packageInfo) == 1)
                {
                    string sql = "DELETE FROM [product_sales_group_info] WHERE sale_package_id = $sale_package_id$";
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("sale_package_id", packageInfo.SalesPackageId);

                    if (ExecuteNonQuery(sql, pc) >= 0)
                    {
                        ProductSalesGroupInfoModel groupInfo = null;
                        foreach (string catId in productCategoryList)
                        {
                            groupInfo = new ProductSalesGroupInfoModel();
                            groupInfo.ProductCategoryId = catId;
                            groupInfo.SaleGroupId = GetGuid();
                            groupInfo.SalePackageId = packageInfo.SalesPackageId;
                            groupInfo.SaleGroupName = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(catId).CategoryName;
                            groupInfo.SaleCityId = packageInfo.SalesCityId;
                            groupInfo.BeginTime = packageInfo.BeginTime;
                            groupInfo.EndTime = packageInfo.EndTime;

                            if (ProductSalesGroupInfoService.Instance.Create(groupInfo) != 1)
                            {
                                RollbackTransaction();
                                message = "更新营销项目产品类型信息失败";
                                return false;
                            }
                        }

                        CommitTransaction();
                        GetSalePackageDomainModelById(packageInfo.SalesPackageId, true);
                        message = "成功更新营销项目信息";
                        result = true;
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新营销项目产品类型信息失败，请与管理员联系";
                        result = false;
                    }
                }
                else
                {
                    RollbackTransaction();
                    message = "更新营销项目失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新营销项目信息异常", ex);
                throw ex;
            }

            return result;
        }

        public bool CreateSalePackageInfo(SalesPackageInfoModel packageInfo, List<string> productCategoryList, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            if (productCategoryList == null || productCategoryList.Count < 1)
            {
                message = "营销项目至少包含一个产品类型，请检查输入";
                return false;
            }

            try
            {
                BeginTransaction();

                packageInfo.SalesPackageId = GetGuid();

                if (Create(packageInfo) == 1)
                {
                    ProductSalesGroupInfoModel groupInfo = null;

                    foreach (string catId in productCategoryList)
                    {
                        groupInfo = new ProductSalesGroupInfoModel();
                        groupInfo.ProductCategoryId = catId;
                        groupInfo.SaleGroupId = GetGuid();
                        groupInfo.SalePackageId = packageInfo.SalesPackageId;
                        groupInfo.SaleGroupName = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(catId).CategoryName;
                        groupInfo.SaleCityId = packageInfo.SalesCityId;
                        groupInfo.BeginTime = packageInfo.BeginTime;
                        groupInfo.EndTime = packageInfo.EndTime;

                        if (ProductSalesGroupInfoService.Instance.Create(groupInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "创建营销项目产品类型信息失败";
                            return false;
                        }
                    }

                    CommitTransaction();
                    message = "成功创建营销项目信息";
                    result = true;
                }
                else
                {
                    RollbackTransaction();
                    message = "创建营销项目失败，请与管理员联系";
                    result = false;
                }                
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建营销项目信息异常", ex);
                throw ex;
            }

            return result;
        }

        public List<SalesPackageInfoModel> GetSalePackageList(string cityId, string packageName, bool getValidPackages, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            DataTable dt = null;
            List<SalesPackageInfoModel> result = null;
            ParameterCollection pc = new ParameterCollection();
            OrderByCollection obc = OrderByCollection.Create(orderField, orderDirection);

            string sql = string.Format("FROM sales_package_info WHERE 1 = 1 {0} {1} {2} ",
                string.IsNullOrEmpty(cityId) ? "" : " AND sales_city_id = $sales_city_id$ ",
                string.IsNullOrEmpty(packageName) ? "" : " AND package_name LIKE $package_name$",
                (getValidPackages) ? " AND GETDATE() >= begin_time AND GETDATE() < end_time " : ""
                );

            if (!string.IsNullOrEmpty(cityId))
            {
                pc.Add("sales_city_id", cityId);
            }
            if (!string.IsNullOrEmpty(packageName))
            {
                pc.Add("package_name", "%" + packageName + "%");
            }

            string totalSQL = "SELECT COUNT(1) " + sql;
            total = Convert.ToInt32(ExecuteScalar(totalSQL, pc));


            dt = ExecuteDataTable("SELECT * " + sql, pc, pageIndex, pageSize, obc);

            result = ModelConvertFrom<SalesPackageInfoModel>(dt);
            
            return result;
        }
	}
}

