using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AttributeTest
{
  public static  class ExtendClass
    {

        public static bool ValidateMaxLength<T>( this T obj)
        {
            var t = obj.GetType();

            //由于我们只在Property设置了Attibute,所以先获取Property
            var properties = t.GetProperties();
            foreach (var property in properties)
            {

                //这里只做一个stringlength的验证，这里如果要做很多验证，需要好好设计一下,千万不要用if elseif去链接
                //会非常难于维护
                if (!property.IsDefined(typeof(StringLengthAttribute), false)) continue;

                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    //这里的MaximumLength 最好用常量去做
                    var maxinumLength = (int)attribute.GetType().
                      GetProperty("MaxLength").
                      GetValue(attribute);

                    //获取属性的值
                    var propertyValue = property.GetValue(obj) as string;
                    if (propertyValue == null)
                        return true;//没有设定限制值

                    if (propertyValue.Length > maxinumLength)
                        return false;
                    else
                        return true;//长度范围在控制之内
                }
            }
            return true;//没有设置限制
        }
    }
}
