using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IBP.Services;
using IBP.Models;
using Framework.Utilities;

namespace IBP.DataImporter
{
    public partial class frmProductMgr : Form
    {
        public frmProductMgr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\projects\\InssinBusinessPlatform\\trunk\\06-程序源码\\01-源码\\IBP.Website\\uploads\\templates\\产品信息导入表2.xls");
            //UserInfoService.Instance.SetVisitorsSessionInfo();
            string importLogs = null, message = null;
            ProductInfoService.Instance.BatchDeleteAllProductCategories();
            ProductInfoService.Instance.ImportProductCategories(ds.Tables["产品类型信息表"], out importLogs, out message);
            ProductInfoService.Instance.ImportProductCategoryAttributes(ds.Tables["产品类型分组属性信息表"], out importLogs, out message);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\projects\\InssinBusinessPlatform\\trunk\\06-程序源码\\01-源码\\IBP.Website\\uploads\\templates\\产品信息导入表2.xls");

            string importLogs = null, message = null;
            ProductInfoService.Instance.ImportProductInfoList(ds, out importLogs, out message);
            //ProductInfoService.Instance.BatchDeleteAllProductCategories();
            //ProductInfoService.Instance.ImportProductCategories(ds.Tables["产品类型信息表"], out importLogs, out message);
            //ProductInfoService.Instance.ImportProductCategoryAttributes(ds.Tables["产品类型分组属性信息表"], out importLogs, out message);
        }
    }
}
