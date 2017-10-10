using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] version = new string[] { "1.0.1","1.2.0"};
            string[] jobs = new string[10];
            var names = new[] { "Tom","Pamila",null};//null 可以转换为任何类型的引用
            //var Ages = new[] { 20, 20, null };//error null 不能转换为int
            //var deiscriblw = new[] { "Tom", 10 };//string 和 int ,不能确定是何种类型

            var students = new[] { new { name = "Tom", age = 25 } , new { name = "Pamila", age = 29} };
            foreach(var student in students)
            {
                Console.WriteLine(string.Format("name={0},age={1}",student.name,student.age));
            }

            //----------数据组的copy
            string[] ob = new string[10];
            object[] test = ob;
            test[0] = "name";
            //test[1] = 20;//能通过编译,但不符合类似安全（string），所以会抛出异常

            //string[] Source  = new string[] { "demo1","demo2"};
            //IComparable[] dest = new IComparable[Source.Length];
            //System.Array.Copy(Source, dest, Source.Length);//二者都是引用类型，引用类型保存的是指向堆得指针，任何两个应用类型可以互相copy
            //foreach (var item in dest)
            //{
            //    Console.WriteLine(item);
            //}


            //IFormattable[]  Source = new IFormattable[2] ;
            //IComparable[] dest = new IComparable[Source.Length];
            //System.Array.Copy(Source, dest, Source.Length);//二者都是引用类型，引用类型保存的是指向堆得指针，任何两个应用类型可以互相copy
            //foreach (var item in dest)
            //{
            //    Console.WriteLine(item);
            //}
            int[,,,] a;

            IFormattable[] Source = new IFormattable[2];
            IComparable[] dest = new IComparable[Source.Length];
            System.Array.ConstrainedCopy(Source,0, dest,0, Source.Length);//二者都是引用类型，引用类型保存的是指向堆得指针，任何两个应用类型可以互相copy
            foreach (var item in dest)
            {
                Console.WriteLine(item);
            }

            //IComparable[] Source  = new IComparable[2];
            //int[] dest = new int[Source.Length] ;
            //System.Array.Copy(Source, dest, Source.Length);//不能转换成功，因为int值类型，IComparable引用类型
            //foreach (var item in dest)
            //{
            //    Console.WriteLine(item);
            //}



            //IComparable[] Source = new IComparable[2];
            //int[] dest = new int[Source.Length];
            //System.Array.Copy(Source, dest, Source.Length);//不能转换成功，因为int值类型，IComparable引用类型
            //foreach (var item in dest)
            //{
            //    Console.WriteLine(item);
            //}


            Console.ReadLine();
        }
    }
}
