/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-19
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;
using IBP.Models;
using System.Text;
using System.Data.OleDb;

namespace IBP.Services
{
	/// <summary>
	/// ProductInfo业务逻辑类
	/// </summary>
	public partial class ProductInfoService : BaseService
	{
        private int _ReturnStatus;
        private string _ReturnMessage;



        private const string CREATE_ERROR = "Can not create excel file, may be your computer has not installed excel!";
        private const string IMPORT_ERROR = "Your excel file is open, please save and close it.";
        private const string EXPORT_ERROR = "This is an error that the excel file may be open when it be exported. /n";



        public int ReturnStatus
        {
            get { return _ReturnStatus; }
        }



        public string ReturnMessage
        {
            get { return _ReturnMessage; }
        }


		// 在此添加你的代码...

        public DataSet ImportProductCategoryFromExcel(string fileName)
        {
            Excel.Application xlApp = new Excel.ApplicationClass();

            if (xlApp == null)
            {
                _ReturnStatus = -1;
                _ReturnMessage = CREATE_ERROR;
                return null;
            }
            
            Excel.Workbook workbook;

            try
            {
                workbook = xlApp.Workbooks.Open(fileName, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, 1, 0);
            }
            catch
            {
                _ReturnStatus = -1;
                _ReturnMessage = IMPORT_ERROR;
                return null;
            }



            int n = workbook.Worksheets.Count;
            string[] SheetSet = new string[n];
            System.Collections.ArrayList al = new System.Collections.ArrayList();

            for (int i = 1; i <= n; i++)
            {
                SheetSet[i - 1] = ((Excel.Worksheet)workbook.Worksheets[i]).Name;
            }



            workbook.Close(null, null, null);
            xlApp.Quit();

            if (workbook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
            }

            if (xlApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
            GC.Collect();



            DataSet ds = new DataSet();
            string connStr = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + fileName + ";Extended Properties=Excel 8.0";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbDataAdapter da;

                for (int i = 1; i <= n; i++)
                {
                    string sql = "select * from [" + SheetSet[i - 1] + "$] ";
                    da = new OleDbDataAdapter(sql, conn);
                    da.Fill(ds, SheetSet[i - 1]);
                    da.Dispose();

                }

                conn.Close();
                conn.Dispose();
            }

            return ds;
        }

        public bool ImportProductCategories(DataTable categoryTable, out string importLogs, out string message)
        {
            message = "操作失败，请与管理员联系";
            importLogs = "";
            bool result = false;

            if (categoryTable == null && categoryTable.Rows.Count == 0)
            {
                message = "产品类型信息表为空，请检查Excel文件是否正确";
                importLogs = message;
                return false;
            }

            try
            {
                ProductCategoryInfoService.Instance.GetProductCategoryList(true);
                CustomDataDomainModel SaleCity = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销售城市", false);
                
                BeginTransaction();
                ProductCategoryInfoModel catInfo = null;
                ProductCategoryGroupInfoModel groupInfo  = null;

                for (int i = 0; i < categoryTable.Rows.Count; i++)
                {
                    catInfo = new ProductCategoryInfoModel();
                    catInfo.CategoryName = categoryTable.Rows[i]["产品类型名称"].ToString();
                    catInfo.CategoryCode = categoryTable.Rows[i]["产品类型编码"].ToString();
                    catInfo.Description = categoryTable.Rows[i]["描述信息"].ToString();

                    groupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupByName(categoryTable.Rows[i]["所属分组名称"].ToString());
                    if (groupInfo != null)
                    {
                        catInfo.GroupName = groupInfo.ProductCategoryGroupId;
                    }
                    else
                    {
                        groupInfo = new ProductCategoryGroupInfoModel();
                        groupInfo.Description = string.Format("【{0}】导入数据创建产品分组【{1}】", DateTime.Now, categoryTable.Rows[i]["所属分组名称"].ToString());
                        groupInfo.GroupName = categoryTable.Rows[i]["所属分组名称"].ToString();
                        groupInfo.IsItemPrice = (categoryTable.Rows[i]["是否独立价格"].ToString() == "是") ? 0 : 1;
                        groupInfo.Status = 0;
                        groupInfo.ProductCategoryGroupId = GetGuid();

                        if (ProductCategoryGroupInfoService.Instance.CreateProductCategoryGroupInfo(groupInfo, out message))
                        {
                            catInfo.GroupName = groupInfo.ProductCategoryGroupId;
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "创建产品分组失败";
                            return false;
                        }
                    }

                    catInfo.ProductCategoryId = GetGuid();
                    catInfo.ItemPrice = Convert.ToDecimal(categoryTable.Rows[i]["本类产品价格"]);
                    
                    catInfo.Status = (categoryTable.Rows[i]["状态"].ToString() == "启用") ? 0 : 1;

                    foreach (CustomDataValueDomainModel item in SaleCity.ValueList.Values)
                    {
                        if (item.DataValue == categoryTable.Rows[i]["销售城市"].ToString())
                        {
                            catInfo.SaleCity = item.ValueId;
                            break;
                        }
                    }

                    //if (string.IsNullOrEmpty(catInfo.SaleCity))
                    //{
                    //    RollbackTransaction();
                    //    message = "Excel中存在未定义的销售城市";
                    //    return false;
                    //}

                    if (categoryTable.Rows[i]["操作"].ToString() == "新建")
                    {
                        if (ProductCategoryInfoService.Instance.CreateProductCategory(catInfo, out message) == false)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }
                }

                CommitTransaction();
                message = "成功导入产品类型信息表";
                ProductCategoryInfoService.Instance.GetProductCategoryList(true);
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("导入产品类型信息异常", ex);
                throw ex;
            }

            return result;
        }

        public bool BatchDeleteAllProductCategories()
        {
            Dictionary<string, ProductCategoryInfoModel> categoryList = ProductCategoryInfoService.Instance.GetProductCategoryList(true);
            string message = "";
            if (categoryList == null)
                return true;

            foreach (ProductCategoryInfoModel catInfo in categoryList.Values)
            {
                ProductCategoryInfoService.Instance.DeleteProductCategory(catInfo.ProductCategoryId, out message);
            }

            return true;
        }

        public bool ImportProductCategoryAttributes(DataTable attTable, out string importLogs, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            importLogs = "";

            if (attTable == null && attTable.Rows.Count == 0)
            {
                message = "产品类型属性信息表为空，请检查Excel文件是否正确";
                importLogs = message;
                return false;
            }

            Dictionary<string, ProductCategoryInfoModel> categoryList = ProductCategoryInfoService.Instance.GetProductCategoryList(true);
            //Dictionary<string, ProductCategoryGroupInfoModel> groupList = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupList(true);
            ProductCategoryGroupInfoModel groupInfo = null;
            if (categoryList == null || categoryList.Count == 0)
            {
                message = "数据库中产品类型信息表为空，请检查";
                importLogs = message;
                return false;
            }

            try
            {
                BeginTransaction();
                ProductCategoryAttributesModel attInfo = null;
                string[] defaultListValue = null;
                for (int i = 0; i < attTable.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(attTable.Rows[i]["操作"].ToString()))
                    {
                        continue;
                    }

                   
                    try
                    {
                        attInfo = new ProductCategoryAttributesModel();
                        defaultListValue = attTable.Rows[i]["属性名称"].ToString().Split(',');
                        attInfo.AttributeName = attTable.Rows[i]["属性名称"].ToString();

                        if (attInfo.AttributeName == "销售状态" || attInfo.AttributeName == "产品代码" || attInfo.AttributeName == "产品名称")
                        {
                            continue;
                        }
                        
                        
                        attInfo.Description = attTable.Rows[i]["描述信息"].ToString();
                        attInfo.FieldMaxLength = (attTable.Rows[i]["最大长度"] == DBNull.Value) ? 50 : Convert.ToInt32(attTable.Rows[i]["最大长度"]);
                        attInfo.FieldMinLength = (attTable.Rows[i]["最小长度"] == DBNull.Value) ? 4 : Convert.ToInt32(attTable.Rows[i]["最小长度"]);
                        
                        attInfo.IsDisplay = 0;
                        attInfo.IsRequest = (attTable.Rows[i]["是否必填项"].ToString() == "是") ? 0 : 1;
                        attInfo.Status = (attTable.Rows[i]["状态"].ToString() == "启用") ? 0 : 1;
                        attInfo.NodeId = 0;
                        attInfo.ParentNode = 0;


                        attInfo.GroupName = attTable.Rows[i]["产品类型分组名称"].ToString();

                        switch (attTable.Rows[i]["字段类型"].ToString())
                        {
                            case "字符串":
                                attInfo.FieldType = "string";
                                //attInfo.DefaultValue = (string.IsNullOrEmpty(attTable.Rows[i]["默认值"].ToString()) || attTable.Rows[i]["默认值"] == DBNull.Value) ? null : attTable.Rows[i]["默认值"].ToString();
                                if (attInfo.FieldMaxLength <= attInfo.FieldMinLength)
                                {
                                    RollbackTransaction();
                                    message = string.Format("操作失败，Excel中产品属性【{0}】【{1}】最大长度与最小长度设置错误", attTable.Rows[i]["产品类型分组名称"], attTable.Rows[i]["属性名称"]);
                                    return false;
                                }
                                break;

                            case "数值":
                                attInfo.FieldType = "decimal";
                                 attInfo.FieldMaxLength = (attTable.Rows[i]["最大长度"] == DBNull.Value) ? 12 : Convert.ToInt32(attTable.Rows[i]["最大长度"]);
                                attInfo.FieldMinLength = (attTable.Rows[i]["最小长度"] == DBNull.Value) ? 4 : Convert.ToInt32(attTable.Rows[i]["最小长度"]);
                                
                                break;

                            case "日期时间":
                                attInfo.FieldType = "datetime";
                                break;

                            case "自定义枚举":
                                attInfo.FieldType = "custom";
                                attInfo.FieldMaxLength = -1;
                                break;

                            case "长文本":
                                attInfo.FieldType = "text";
                                attInfo.FieldMaxLength = -1;
                                break;

                            default:
                                break;
                        }

                        if (defaultListValue != null && defaultListValue.Length > 0)
                        {
                            for (int j = 0; j < defaultListValue.Length; j++)
                            {
                                attInfo.CustomValue += defaultListValue[j] + "\n";
                            }
                        }



                        if (attTable.Rows[i]["操作"].ToString() == "新建")
                        {
                            foreach (ProductCategoryInfoModel catInfo in categoryList.Values)
                            {
                                if (catInfo.GroupName == ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupByName(attTable.Rows[i]["产品类型分组名称"].ToString()).ProductCategoryGroupId)
                                {
                                    attInfo.CategoryAttributeId = GetGuid();
                                    attInfo.ProductCategoryId = catInfo.ProductCategoryId;

                                    #region 创建产品类型属性

                                    Dictionary<string, ProductCategoryAttributesModel> dict = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(attInfo.ProductCategoryId, false);
                                    if (dict == null)
                                    {
                                        message = "操作失败，不存在的产品类型ID";
                                        return false;
                                    }
                                    bool execute = true;
                                    foreach (ProductCategoryAttributesModel item in dict.Values)
                                    {
                                        if (item.AttributeName == attInfo.AttributeName)
                                        {
                                            message = "操作失败，本产品类型存在相同名称属性";
                                            execute = false;
                                            break;
                                        }
                                    }
                                    if (execute)
                                    {
                                        if (ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(attInfo, out message) == false)
                                        {
                                            RollbackTransaction();
                                            return false;
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                    catch (Exception ex2)
                    {
                        RollbackTransaction();
                        throw ex2;
                    }
                }

                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("导入产品类型属性异常", ex);
                throw ex;
            }

            return result;
                    
        }

        public bool ImportProductInfoList(DataSet productDataSet, out string importLogs, out string message)
        {
            bool result = false;
            importLogs = "";
            message = "操作失败，请与管理员联系";

            if (productDataSet == null && productDataSet.Tables.Count == 0)
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

            string productCategoryCode = null, importSQL = null;
            Dictionary<string, ProductCategoryAttributesModel> proAttList = null;
            ProductCategoryInfoModel catInfo = null;
            ParameterCollection pc = new ParameterCollection();
            ProductInfoModel proInfo = null;
            try
            {
                BeginTransaction();
                for (int t = 0; t < productDataSet.Tables.Count; t++)
                {
                    if (productDataSet.Tables[t].TableName.Contains("-") == false)
                    {
                        continue;
                    }

                    if (productDataSet.Tables[t].TableName.Split('-')[1] != "产品信息表")
                    {
                        continue;
                    }




                    for (int i = 0; i < productDataSet.Tables[t].Rows.Count; i++)
                    {
                        productCategoryCode = productDataSet.Tables[t].Rows[i]["商品编号"].ToString();
                        catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoByCategoryCode(productCategoryCode);

                        if (productDataSet.Tables[t].Rows[i]["操作"].ToString() == "")
                            break;

                        if (catInfo == null)
                        {
                            RollbackTransaction();
                            message = string.Format("数据库中不存在产品类型编码为【{0}】的产品类型，数据导入失败", productCategoryCode);
                            return false;
                        }

                        proAttList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(catInfo.ProductCategoryId, false);
                        if (proAttList == null)
                        {
                            RollbackTransaction();
                            message = string.Format("数据库中不存在产品类型编码为【{0}】的产品类型属性数据，数据导入失败", productCategoryCode);
                            return false;
                        }

                        Dictionary<string, ProductCategorySalesStatusModel> statusList = ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(catInfo.ProductCategoryId, false);
                        if (proAttList == null)
                        {
                            RollbackTransaction();
                            message = string.Format("数据库中不存在产品类型编码为【{0}】的产品类型销售状态数据，数据导入失败", productCategoryCode);
                            return false;
                        }


                        importSQL = GetImportProductInfoSQL(catInfo, proAttList);
                        pc.Clear();

                        foreach (ProductCategoryAttributesModel attInfo in proAttList.Values)
                        {                            
                            if (attInfo.FieldType == "decmial" && productDataSet.Tables[t].Rows[i][attInfo.AttributeName].ToString() == "")
                            {
                                pc.Add(string.Format("{0}", CharacterUtil.ConvertToPinyin(attInfo.AttributeName)), 0);
                            }
                            else
                            {
                                if (attInfo.AttributeName == "销售状态")
                                {
                                    pc.Add(string.Format("{0}", CharacterUtil.ConvertToPinyin(attInfo.AttributeName)),GetProductSaleStatusId(statusList,productDataSet.Tables[t].Rows[i][attInfo.AttributeName].ToString()));
                                }
                                else
                                {
                                    pc.Add(string.Format("{0}", CharacterUtil.ConvertToPinyin(attInfo.AttributeName)), productDataSet.Tables[t].Rows[i][attInfo.AttributeName].ToString());
                                }
                            }
                        }

                        proInfo = new ProductInfoModel();
                        proInfo.ProductId = GetGuid();
                        proInfo.CreatedOn = DateTime.Now;
                        proInfo.CategoryId = catInfo.ProductCategoryId;
                        proInfo.ItemPrice = Convert.ToDecimal(productDataSet.Tables[t].Rows[i]["销售价格"].ToString());
                        proInfo.ProductCode = productDataSet.Tables[t].Rows[i]["产品代码"].ToString();
                        proInfo.ProductName = productDataSet.Tables[t].Rows[i]["产品名称"].ToString();
                        proInfo.SalesStatus = productDataSet.Tables[t].Rows[i]["销售状态"].ToString();// productDataSet.Tables[t].Rows[i]["销售状态"].ToString();
                        // proInfo.SalesStatus = GetProductSaleStatusId(statusList, productDataSet.Tables[t].Rows[i]["销售状态"].ToString());// productDataSet.Tables[t].Rows[i]["销售状态"].ToString();
                        proInfo.Status = 0;

                        pc.Add("product_id", proInfo.ProductId);
                        pc.Add("product_category_id",proInfo.CategoryId);
                        pc.Add("category_group_id", catInfo.GroupName);
                        pc.Add("created_on", proInfo.CreatedOn);
                        pc.Add("created_by",  "C792D747-6B74-4A58-BB5B-D98EF420F99F"  );

                        if (Create(proInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "导入Excel数据失败";
                            return false;
                        }
                        else
                        {
                            if (ExecuteNonQuery(importSQL, pc) != 1)
                            {
                                RollbackTransaction();
                                message = "导入Excel数据失败";
                                return false;
                            }
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

        protected string GetImportProductInfoSQL(ProductCategoryInfoModel catInfo, Dictionary<string, ProductCategoryAttributesModel> proAttList)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("INSERT INTO [{0}] ( ", catInfo.TableName);
            foreach (ProductCategoryAttributesModel item in proAttList.Values)
            {
                sb.AppendFormat(" [{0}],", item.AttributeName);
            }
            sb.Append("product_id, product_category_id, category_group_id, created_on, created_by, status_code ) VALUES ( ");
            foreach (ProductCategoryAttributesModel item in proAttList.Values)
            {
                sb.AppendFormat(" ${0}$,", CharacterUtil.ConvertToPinyin(item.AttributeName));
            }

            sb.AppendFormat("$product_id$, $product_category_id$, $category_group_id$, $created_on$, $created_by$, 0 ) ");

            return sb.ToString();
        }

        protected string GetProductSaleStatusId(Dictionary<string, ProductCategorySalesStatusModel> source, string statusName)
        {
            string result = statusName;

            if (source != null)
            {
                foreach (ProductCategorySalesStatusModel item in source.Values)
                {
                    if (item.SalestatusName == statusName)
                    {
                        return item.SalesStatusId;
                    }
                }
            }

            return result;
        }
    }
}
