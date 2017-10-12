using System;
using System.Threading;

namespace DelegateTest
{
    class Program
    {
        public static event WaitCallback xx;
        static void Main(string[] args)
        {
            for (int i = 0; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(obj => Console.WriteLine(obj), i);
                ThreadPool.QueueUserWorkItem(new WaitCallback(WriteData), i);

                WaitCallback de = new WaitCallback(WriteData);
                de += obj => { Console.WriteLine(obj); }; 
                de(i);

                new Action<int>(WriteData2)(i);
               Console.WriteLine(  new Func<int,string>(WriteData3)(i));

                Func<int, string> dele = new Func<int, string>(WriteData3);
                dele += WriteData4;
                dele(i);


                xx+= obj => { Console.WriteLine(1234566); };
                xx(1);
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
        public static string WriteData4(int i)
        {
            Console.WriteLine(i);
            return "Func1";
        }
    }
}
