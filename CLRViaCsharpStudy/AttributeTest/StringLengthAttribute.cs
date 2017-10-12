using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeTest
{
    [AttributeUsage(AttributeTargets.Property)]
   public class StringLengthAttribute:Attribute
    {
        public int MaxLength { get; set; }
    }
}
