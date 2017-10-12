using System;
using System.Reflection;
using static AttributeTest.Peoduct;

namespace AttributeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = typeof(Peoduct);

            var classAttribute = (NumberAttribute)Attribute.GetCustomAttribute(info, typeof(NumberAttribute));
            Console.WriteLine(classAttribute.CanEqualToZero);
            Console.WriteLine(classAttribute.CanGreaterThanZero);
            Console.WriteLine(classAttribute.CanLessThanZero);

            Peoduct produc = new Peoduct();
            produc.Name = "4001669410014";
            produc.ValidateMaxLength();

            Console.WriteLine("Name 长度验证： "+ produc.ValidateMaxLength());


            Validate(produc);
            Console.ReadLine();
    }

        
        public static void Validate(object obj)
        {
            var t = obj.GetType();

            //由于我们只在Property设置了Attibute,所以先获取Property
            var properties = t.GetProperties();
            foreach (var property in properties)
            {

                //这里只做一个stringlength的验证，这里如果要做很多验证，需要好好设计一下,千万不要用if elseif去链接
                //会非常难于维护，类似这样的开源项目很多，有兴趣可以去看源码。
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
                        throw new Exception("exception info");//这里可以自定义，也可以用具体系统异常类

                    if (propertyValue.Length > maxinumLength)
                        throw new Exception(string.Format("属性{0}的值{1}的长度超过了{2}", property.Name, propertyValue, maxinumLength));
                }
            }
        }

        }
    }
