using System;
using System.Collections.Generic;
using System.Text;

namespace NullableTest
{
    class UserInfo
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public static explicit operator UserInfo(int id)
        {
            Console.WriteLine("获取用户ID为【{0}】的User对象", id);
            return new UserInfo { ID = id };
        }

        public static implicit operator UserInfo(string name)
        {
            Console.WriteLine("获取用户名为【{0}】的User对象", name);
            return new UserInfo { Name = name };
        }
        public static  UserInfo operator ++(UserInfo info)
        {
            info.Name = info.Name + "122335";;
            return info;
        }

        public static UserInfo operator +(UserInfo info1, UserInfo info2)
        {
            return new UserInfo { ID=info1.ID+info2.ID,Name=info1.Name+info2.Name};
        }

    }
}
