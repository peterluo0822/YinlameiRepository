using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumTest
{
  public  class Fruit
    {
        public enum Color
        {
            red,
            green,
            purple,
            orange
        }

        public static TEnum[] GetColorValues<TEnum>( ) where TEnum : struct
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

    }
}
