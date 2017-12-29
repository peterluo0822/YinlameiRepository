using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FineEx.Func
{
    public class Utility
    {
        public static  bool IsNumber(string str)
        {
            string pattern = @"^\d+(\.\d)?$";
            if (!Regex.IsMatch(str, pattern))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断字符串是否问整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串为整数返回True,否则返回False</returns>
        public static bool IsIntNumber(string str)
        {
            string pattern = @"^\d*$";
            if (!Regex.IsMatch(str, pattern))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNullText(object  str)
        {
            if (str == null) return true;
            if (str == DBNull.Value) return true;
            if (str.ToString().Length == 0) return true;
            return false;
        }


        public static decimal GetDecimalData(object data)
        {
            if (data == null) return 0M;
            if (data == DBNull.Value) return 0M;
            try
            {
                return decimal.Parse(data.ToString());
            }
            catch
            {
                return 0M;
            }
           
        }

        public static int GetIntData(object data)
        {
            if (data == null) return 0;
            if (data == DBNull.Value) return 0;
            try
            {
                return int.Parse(data.ToString());
            }
            catch
            {
                return 0;
            }

        }
    }
}
