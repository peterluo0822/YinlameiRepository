using System;

namespace NullableTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("------------以下内容是操作符重载-------------");
            UserInfo info = new UserInfo();
            info.Name = "Tom";
            UserInfo info2 = info++;
            info2.ID = 7;
            Console.WriteLine(info2.Name);

            UserInfo info3 = new UserInfo();
            info3.Name = "info3";
            info3.ID = 5;
            Console.WriteLine((info2+info3).Name);
            Console.WriteLine((info2 + info3).ID);

            UserInfo info4 = (UserInfo)20;
            UserInfo info5 = "Tom";

            Console.WriteLine("------------Nullable-------------");

            string greating=null ;
            Console.WriteLine(greating??"hello people");//空操作符

            int? o=5;

            Console.WriteLine(o.GetType().ToString());

            Console.WriteLine(((IComparable)o).Equals(5));

            Console.ReadLine();
        }
    }
}
