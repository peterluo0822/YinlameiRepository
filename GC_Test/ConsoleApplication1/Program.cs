using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ss();
            GC.Collect();
            Console.ReadLine();
        }

        public static void ss ()
        {
            //System.Timers.Timer t = new System.Timers.Timer()
            //t.Interval = 1000;
            //t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);//到达时间的时候执行事件；    
            //t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；    
            //t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            System.Threading.Timer t = new System.Threading.Timer(ChkSrv,null,1000,100);
        }

        public static void ChkSrv(object source)
        {
            Console.WriteLine("hello");
        }

        public static void ChkSrv(object source, System.Timers.ElapsedEventArgs e)
        {
            int Hour = e.SignalTime.Hour;
            int Minute = e.SignalTime.Minute;
            int Second = e.SignalTime.Second;
            Console.WriteLine(string.Format("{0}:{1}:{2}", Hour, Minute, Second));
        }
    }
}
