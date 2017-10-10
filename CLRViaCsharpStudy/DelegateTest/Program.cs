using System;
using System.Threading;

namespace DelegateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(obj => Console.WriteLine(obj), i);
                ThreadPool.QueueUserWorkItem(new WaitCallback(WriteData), i);
                new Action<int>(WriteData2)(i);
               Console.WriteLine(  new Func<int,string>(WriteData3)(i));
            }

                Console.ReadLine();
        }


        public static void  WriteData(object i)
        {
            Console.WriteLine(i);
        }
        public static void WriteData2(int i)
        {
            Console.WriteLine(i);
        }
        public static string   WriteData3(int i)
        {
            Console.WriteLine(i);
            return "Func";
        }
    }
}
