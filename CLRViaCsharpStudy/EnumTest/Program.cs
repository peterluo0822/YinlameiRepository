using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extend.Tool;
using System.IO;

namespace EnumTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //返回Fruit.Color的数据类型
            Console.WriteLine(Enum.GetUnderlyingType(typeof(Fruit.Color)));


            string[] Names = Enum.GetNames(typeof(Fruit.Color));
            foreach (string item in Names)
                Console.WriteLine(item);

            Array Values = Enum.GetValues(typeof(Fruit.Color));
            foreach (int item in Values)
                Console.WriteLine(item);

            //静态方法
            Fruit.Color[] colors = Fruit.GetColorValues<Fruit.Color>();
            foreach (Fruit.Color item in colors)
                Console.WriteLine(string.Format(string.Format("name={0},value={1}", Enum.GetName(typeof(Fruit.Color), item), item.ToString("D"))));

            //扩张方法
            Fruit.Color[] colors2 = new Fruit.Color().GetColorValues();
            foreach (Fruit.Color item in colors2)
                Console.WriteLine(string.Format(string.Format("name={0},value={1}", Enum.GetName(typeof(Fruit.Color), item), item.ToString("D"))));


            Console.WriteLine(Fruit.Color.purple.ToString());
            Console.WriteLine(Fruit.Color.purple.ToString("D"));
            Console.WriteLine(Enum.Format(typeof(Fruit.Color), Fruit.Color.purple,"D"));

            //实作接口
            Console.WriteLine("Color test");
            Color color = new Color();
            while (color.MoveNext())
            {
                KeyValuePair<string, int> item = color.Current;
                Console.WriteLine(string.Format("key={0},value={1}",item.Key,item.Value));
            }

            Fruit.Color red = (Fruit.Color)Enum.Parse(typeof(Fruit.Color), "red");

            Fruit.Color black;
            //TryParse,当转换失败时，默认值时第一个，譬如black=Fruit.Color.red
            bool result = Enum.TryParse<Fruit.Color>("black",out black);
            Console.WriteLine(string.Format(string.Format("name={0},value={1},change={2}", Enum.GetName(typeof(Fruit.Color), black), black.ToString("D"),result)));
    
            //此处会报错，不成的转换
            //black = (Fruit.Color)Enum.Parse(typeof(Fruit.Color), "black");

            //判断值是否属于枚举
            Console.WriteLine(Enum.IsDefined(typeof(Fruit.Color), "black"));
            Console.WriteLine(Enum.IsDefined(typeof(Fruit.Color), 3));
            Console.WriteLine(Enum.IsDefined(typeof(Fruit.Color), "red"));

            //-----------------下面进入下一结，位标志
            FileAttributes str= File.GetAttributes(@"E:\gitProject\CLRViaCsharpStudy\EnumTest\Fruit.cs");

            Console.WriteLine(Action.All.ToString("D"));
            Console.WriteLine(Action.All.ToString("F"));
            Enum.Parse(typeof(Action), "All");
            Action XX =(Action) Enum.Parse(typeof(Action), "3");
           Console.WriteLine(string.Format(string.Format("name={0},value={1}", XX.ToString("F"), XX.ToString("D"))));

            Action cc = Action.Add &Action.edit;
            Console.WriteLine(string.Format(string.Format("name={0},value={1}",cc.ToString("F"), cc.ToString("D"))));

            //------------------未Enum添加方法,方法应该是扩展方法(扩张方法只能对实例有用)
            new Action().Print();

            Console.ReadLine();
        }

        public enum Action
        {
            None = 0,
            Add = 1,
            edit = 2,
            Delete = 3,
            All = Add | edit | Delete
        }
    }
}
