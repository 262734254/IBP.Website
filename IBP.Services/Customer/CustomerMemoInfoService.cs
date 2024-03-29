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
	/// CustomerMemoInfo业务逻辑类
	/// </summary>
	public partial class CustomerMemoInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool CreateMemoInfo(CustomerMemoInfoModel memoInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (memoInfo == null)
                return false;

            memoInfo.MemoId = GetGuid();
            if (Create(memoInfo) == 1)
            {
                result = true;
                message = "成功创建客户备注信息";
                CustomerInfoService.Instance.GetCustomerDomainModelById(memoInfo.CustomerId, true);
            }

            return result;
        }
	}
}

