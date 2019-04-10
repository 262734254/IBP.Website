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

        public bool ImportSalePackageFromExcel(DataSet ds, out string importLogs, out string message)
        {
            bool result = false;
            importLogs = message = "操作失败，请与管理员联系";

            if (ds == null && ds.Tables.Count == 0)
            {
                message = "Excel数据文件异常，请检查";
                return false;
            }

            Dictionary<string, ProductCategoryInfoModel> productCategoryList = ProductCategoryInfoService.Instance.GetProductCategoryList(false);
            if (productCategoryList == null || productCategoryList.Count == 0)
            {
                message = "数据库中无产品类型信息，请检查";
                return false;
            }

            ParameterCollection pc = new ParameterCollection();
            CustomDataDomainModel SaleCityList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销售城市", false);
            SalesPackageInfoModel salePackInfo = null;
            ProductCategoryInfoModel proCatInfo = null;
            List<string> proCatList = new List<string>();

            try
            {
                BeginTransaction();
                for (int t = 0; t < ds.Tables.Count; t++)
                {
                    if (ds.Tables[t].TableName.Contains("-") == false)
                    {
                        continue;
                    }

                    if (ds.Tables[t].TableName.Split('-')[0] != "营销计划")
                    {
                        continue;
                    }

                    string saleCityName = ds.Tables[t].TableName.Split('-')[1];
                    CustomDataValueDomainModel saleCityInfo = SaleCityList.GetCustomDataValueDomainByDataValue(saleCityName);
                    if (saleCityInfo == null)
                    {
                        RollbackTransaction();
                        message = string.Format("数据库中不存在销售城市为【{0}】的营销计划，数据导入失败", saleCityName);
                        return false;
                    }

                    for (int i = 0; i < ds.Tables[t].Rows.Count; i++)
                    {
                        salePackInfo = new SalesPackageInfoModel();
                        salePackInfo.BeginTime = Convert.ToDateTime(ds.Tables[t].Rows[i]["有效起始时间"]);
                        salePackInfo.EndTime = Convert.ToDateTime(ds.Tables[t].Rows[i]["有效截止时间"]);
                        salePackInfo.Location = ds.Tables[t].Rows[i]["产品定位"].ToString();
                        salePackInfo.MonthKeepPrice = Convert.ToDecimal(ds.Tables[t].Rows[i]["每月补存"]);
                        salePackInfo.MonthReturnPrice = Convert.ToDecimal(ds.Tables[t].Rows[i]["每月返还"]);
                        salePackInfo.PackageName = ds.Tables[t].Rows[i]["项目名称"].ToString();
                        salePackInfo.PriceTotal = Convert.ToDecimal(ds.Tables[t].Rows[i]["业务总额"]);
                        salePackInfo.Remark = ds.Tables[t].Rows[i]["备注信息"].ToString();
                        salePackInfo.ReturnMonths = Convert.ToInt32(ds.Tables[t].Rows[i]["返还月数"]);
                        salePackInfo.SalePrice = Convert.ToDecimal(ds.Tables[t].Rows[i]["购机金额"]);
                        salePackInfo.SalesCityId = saleCityInfo.ValueId;
                        salePackInfo.SalesCityName = saleCityInfo.DataValue;
                        salePackInfo.SalesPackageId = GetGuid();
                        salePackInfo.StagePrice = Convert.ToDecimal(ds.Tables[t].Rows[i]["每期金额"]);
                        salePackInfo.Stages = Convert.ToInt32(ds.Tables[t].Rows[i]["分期数"]);
                        salePackInfo.Status = 0;
                        salePackInfo.StoredPrice = Convert.ToDecimal(ds.Tables[t].Rows[i]["预存话费"]);


                        proCatList.Clear();

                        string[] catNameList = ds.Tables[t].Rows[i]["包含产品"].ToString().Split(',');
                        if (catNameList == null || catNameList.Length == 0)
                        {
                            RollbackTransaction();
                            message = string.Format("销售城市为【{0}】的营销计划中，营销项目{1}中没有设置包含产品类型名称，数据导入失败", saleCityName, salePackInfo.PackageName);
                            return false;
                        }

                        
                        for (int j = 0; j < catNameList.Length; j++)
                        {
                            proCatInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoByName(catNameList[j]);
                            if (proCatInfo == null)
                            {
                                RollbackTransaction();
                                message = string.Format("销售城市为【{0}】的营销计划中，营销项目{1}中设置包含产品类型名称{2}不存在于数据库，数据导入失败", saleCityName, salePackInfo.PackageName,catNameList[j]);
                                return false;
                            }

                            proCatList.Add(proCatInfo.ProductCategoryId);
                        }


                        if (SalesPackageInfoService.Instance.CreateSalePackageInfo(salePackInfo, proCatList, out message) == false)
                        {
                            RollbackTransaction();
                            message = string.Format("销售城市为【{0}】的营销计划中，营销项目{1}数据导入失败", saleCityName, salePackInfo.PackageName);
                            return false;
                        }
                    }
                }

                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("从Excel导入产品数据异常", ex);
                throw ex;
            }

            return result;
        }
    }
}
