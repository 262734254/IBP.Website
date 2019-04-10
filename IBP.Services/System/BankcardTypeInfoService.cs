/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-13
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
	/// BankcardTypeInfo业务逻辑类
	/// </summary>
	public partial class BankcardTypeInfoService : BaseService
	{
        private DataTable _bankCardTypeTable = null;
		// 在此添加你的代码...

        private DataTable BankCardTypeTable
        {
            get
            {
                if (_bankCardTypeTable == null)
                {
                    _bankCardTypeTable = ExecuteDataTable("SELECT * FROM bankcard_type_info");
                }

                return _bankCardTypeTable;
            }
            set
            {
                _bankCardTypeTable = value;
            }
        }

        /// <summary>
        /// 批量删除银行卡信息。
        /// </summary>
        /// <param name="cardIdList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BatchDeleteBankCardsById(List<string> cardIdList, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            if (cardIdList == null || cardIdList.Count == 0)
            {
                return result;
            }

            try
            {
                BeginTransaction();

                for (int i = 0; i < cardIdList.Count; i++)
                {
                    if (Delete(cardIdList[i]) == 0)
                    {
                        RollbackTransaction();
                        result = false;
                        message = "删除银行卡信息失败";
                        return result;
                    }
                    else
                    {
                        // TASK： 这里还需要判断其他地方有没有引用该银行卡信息，如果有，则不能删除。
                        // if(此ID有引用)
                        // {
                        //      RollbackTransaction();
                        //      result = false;
                        //      message = "操作失败，银行卡信息存在其他数据引用";
                        //      return result;
                        // }
                    }
                }

                CommitTransaction();
                BankCardTypeTable = null;
                result = true;
                message = "成功批量删除选中的银行卡信息";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除银行卡信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 根据ID删除银行卡信息。
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteBankCardInfoById(string cardId, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            try
            {
                BeginTransaction();

                if (Delete(cardId) > 0)
                {
                    CommitTransaction();
                    BankCardTypeTable = null;
                    result = true;
                    message = "成功删除银行卡信息";

                    // TASK： 这里还需要判断其他地方有没有引用该银行卡信息，如果有，则不能删除。
                }
                else
                {
                    RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除银行卡信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 更新银行卡信息。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateBankCardInfo(BankcardTypeInfoModel info)
        {
            bool result = false;

            if (Update(info) > 0)
            {
                result = true;
                BankCardTypeTable = null;
            }

            return result;
        }
        

        /// <summary>
        /// 根据ID获取银行卡信息。
        /// </summary>
        /// <param name="cardTypeId"></param>
        /// <returns></returns>
        public BankcardTypeInfoModel GetBankCardInfoById(string cardTypeId)
        {
            if (cardTypeId == null)
                return null;

            BankcardTypeInfoModel result = null;

            string filterSQL = string.Format("bankcard_type_id = " + cardTypeId);
            DataRow[] hasRows = BankCardTypeTable.Select(filterSQL);

            if (hasRows.Length > 0)
            {
                result = new BankcardTypeInfoModel();
                ModelConvertFrom(result, hasRows[0]);
            }

            return result;
        }

        public BankcardTypeInfoModel GetBankCardInfoByBinCode(string binCode)
        {
            if (binCode == null)
                return null;

            BankcardTypeInfoModel result = null;

            string filterSQL = string.Format("card_bin_code = " + binCode);
            DataRow[] hasRows = BankCardTypeTable.Select(filterSQL);

            if (hasRows.Length > 0)
            {
                result = new BankcardTypeInfoModel();
                ModelConvertFrom(result, hasRows[0]);
            }

            return result;
        }


        /// <summary>
        /// 新建银行卡信息。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool NewBankCardTypeInfo(BankcardTypeInfoModel info)
        {
            bool result = false;

            if (Create(info) > 0)
            {
                result = true;
                BankCardTypeTable = null;
            }

            return result;
        }

        /// <summary>
        /// 根据开户行ID获取银行卡信息总数。
        /// </summary>
        /// <param name="openBankId"></param>
        /// <returns></returns>
        public int GetBankCardTotalByOpenBankId(string openBankId)
        {
            string sql = "";
            int result = 0;

            if (string.IsNullOrEmpty(openBankId))
            {
                sql = "SELECT COUNT(1) FROM bankcard_type_info";
                result = Convert.ToInt32(ExecuteScalar(sql));
            }
            else
            {
                sql = @"SELECT COUNT(1) FROM bankcard_type_info WHERE bank_enum_value = $bank_enum_value$;";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("bank_enum_value", openBankId);

                result = Convert.ToInt32(ExecuteScalar(sql,pc));
            }

            return result;
        }



        /// <summary>
        /// 根据开户行ID获取银行卡信息列表。
        /// </summary>
        /// <param name="openBankId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<BankcardTypeInfoModel> GetBankCardListByOpenBankId(string openBankId, string cardBinNum, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            string sql = "";
            DataTable dt = null;
            List<BankcardTypeInfoModel> result = null;
            ParameterCollection pc = new ParameterCollection();
            OrderByCollection obc = OrderByCollection.Create(orderField, orderDirection);

            sql = string.Format("SELECT * FROM bankcard_type_info WHERE 1 = 1 {0} {1} ",
                string.IsNullOrEmpty(openBankId) ? "" : " AND bank_enum_value = $bank_enum_value$ ",
                string.IsNullOrEmpty(cardBinNum) ? "" : " AND card_bin_code = $card_bin_code$"
                );

            if (!string.IsNullOrEmpty(openBankId))
            {
                pc.Add("bank_enum_value", openBankId);
            }
            if (!string.IsNullOrEmpty(cardBinNum))
            {
                pc.Add("card_bin_code", cardBinNum);
            }

            dt = ExecuteDataTable(sql, pc, pageIndex, pageSize, obc);
            result = ModelConvertFrom<BankcardTypeInfoModel>(dt);

            return result;
        }
	}
}

