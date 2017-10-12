using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeTest
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class NumberAttribute : Attribute
    {
        public NumberAttribute()
        {
        }

        public bool CanGreaterThanZero { get; set; }
        public bool CanLessThanZero { get; set; }
        public bool CanEqualToZero { get; set; }
    }
    
}
