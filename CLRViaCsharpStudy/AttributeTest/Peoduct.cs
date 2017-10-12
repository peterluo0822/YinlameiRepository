using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeTest
{
    [Number(CanEqualToZero = true, CanGreaterThanZero = true)]
    public class Peoduct
    {
         public int StockAmont = 0;

        [StringLength(MaxLength = 5)]
        public String Name { get; set; }

        [Flags]
        public enum Color
        {
            red,
            blue,
            green,
            yello,
            orange
        };

    }
}
