using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extend.Tool
{
  public static  class ExtendClass
    {
        /// <summary>
        /// 返回一个集合，为了避免装箱
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static TEnum[] GetColorValues<TEnum>( this TEnum t) where TEnum : struct
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
        public static void Print<TEnum>(this TEnum t) where TEnum : struct
        {
           foreach(string name in   Enum.GetNames(typeof(TEnum)))
                Console.WriteLine(name);
        }
    }
}
