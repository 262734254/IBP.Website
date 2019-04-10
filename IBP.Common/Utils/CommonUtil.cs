using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IBP.Common
{
    public static class CommonUtil
    {
        public static string GetBase64(string srouce)
        {
            if (srouce == null)
            {
                srouce = "";
            }

            byte[] bytes = Encoding.Default.GetBytes(srouce);
            //编码
            return Convert.ToBase64String(bytes);
        }

        public static string SetCheckBoxSelected(string src, object target)
        {
            return "";
        }

        public static string SetSortOrder(string orderFeild, string currentOrderFeild, string orderDirection)
        {
            string result = "";
            if (orderFeild.ToLower() == currentOrderFeild.ToLower())
            {
                result = string.Format(@" orderField={0} class={1}", orderFeild, orderDirection);
            }
            else
            {
                result = string.Format(@" orderField={0} ", orderFeild, orderDirection);
            }

            return result;
        }

        public static string SetValue(object src)
        {
            return (src == null) ? "" : src.ToString();
        }

        public static string SetComboxSelected(object src, object current)
        {
            if (src == null || current == null)
            {
                return "";
            }

            return (src.ToString().ToLower() == current.ToString().ToLower()) ? "selected=selected" : "";
        }

        public static string GetFieldTypeName(object src)
        {
            if (src == null)
            {
                return "未知";
            }

            switch (src.ToString())
            {
                case "string": return "字符串";
                case "text": return "长文本";
                case "datetime": return "日期时间";
                case "decimal": return "数值";
                case "custom": return "自定义枚举";

                default: return "未知";
            }
        }

        public static string SetReadOnly(bool? isReadOnly, string tagName)
        {
            bool isReadOnly2 = true;

            if (isReadOnly == null)
                isReadOnly2 = true;
            else
                isReadOnly2 = Convert.ToBoolean(isReadOnly);

            if (isReadOnly2)
            {
                switch (tagName.ToLower())
                {
                    case "text":
                        return " readonly='readonly' ";

                    case "select":
                        return "disabled='disabled'";

                    default:
                        break;
                }
            }

            return "";
        }
        /// <summary>
        /// 银行卡号码
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static string CreditCardCardNumber(string cardNumber)
        {
       
            if (string.IsNullOrEmpty(cardNumber))
                return "";

            if (cardNumber.Length == 16)
            {
                return string.Format("{0} {1} {2} {3}", cardNumber.Substring(0, 4), cardNumber.Substring(4, 4), cardNumber.Substring(8, 4), cardNumber.Substring(cardNumber.Length - 4));
            }
            if (cardNumber.Length == 17)
            {
                return string.Format("{0} {1} {2} {3} {4}", cardNumber.Substring(0, 4), cardNumber.Substring(4, 4), cardNumber.Substring(8, 4), cardNumber.Substring(cardNumber.Length - 5, 4), cardNumber.Substring(cardNumber.Length - 1));
            }
            if (cardNumber.Length == 18)
            {
                return string.Format("{0} {1} {2} {3} {4}", cardNumber.Substring(0, 4), cardNumber.Substring(4, 4), cardNumber.Substring(8, 4), cardNumber.Substring(cardNumber.Length - 6, 4), cardNumber.Substring(cardNumber.Length - 2));
            }
            if (cardNumber.Length == 19)
            {
                return string.Format("{0} {1} {2} {3} {4}", cardNumber.Substring(0, 4), cardNumber.Substring(4, 4), cardNumber.Substring(8, 4), cardNumber.Substring(cardNumber.Length - 7, 4), cardNumber.Substring(cardNumber.Length - 3));
            }

            return cardNumber;
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static string CreditCard(string cardNumber)
        {
          
            if (string.IsNullOrEmpty(cardNumber))
                return "";

            if (cardNumber.Length == 15)
            {
                return string.Format("{0} {1} {2} {3}", cardNumber.Substring(0, 4), cardNumber.Substring(4, 4), cardNumber.Substring(8, 4), cardNumber.Substring(cardNumber.Length - 3));
            }

            if (cardNumber.Length == 18)
            {
                return string.Format("{0} {1} {2} {3} {4}", cardNumber.Substring(0, 3), cardNumber.Substring(3, 3), cardNumber.Substring(7, 4), cardNumber.Substring(cardNumber.Length - 8,4),cardNumber.Substring(cardNumber.Length - 4));
            }

            return cardNumber;
        }





        public static string MarkCreditCard(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return "";

            return string.Format("{0}******{1}", cardNumber.Substring(0, 6), cardNumber.Substring(cardNumber.Length - 4, 4));
        }

        public static string MarkIdCard(string idCardNumber)
        {
            if (string.IsNullOrEmpty(idCardNumber))
                return "";

            return string.Format("{0}******{1}", idCardNumber.Substring(0, 5), idCardNumber.Substring(idCardNumber.Length - 4, 4));
        }

        public static string GetBirthDayFromIdCardNumber(string idCardNumber)
        {
            string result = null;

            if (string.IsNullOrEmpty(idCardNumber))
                return null;

            if (idCardNumber.Length == 15)
            {
                return string.Format("{0}-{1}-{2}",
                    (idCardNumber.Substring(6, 1) == "0") ? "20" + idCardNumber.Substring(6, 2) : "19" + idCardNumber.Substring(6, 2), 
                    idCardNumber.Substring(8, 2), 
                    idCardNumber.Substring(10, 2));
            }

            if (idCardNumber.Length == 18)
            {
                return string.Format("{0}-{1}-{2}", idCardNumber.Substring(6, 4), idCardNumber.Substring(10, 2), idCardNumber.Substring(12, 2));
            }

            return result;
        }
    }
}
