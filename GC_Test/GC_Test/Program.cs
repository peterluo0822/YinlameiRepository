using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GC_Test
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();
            Shell.WriteLine("注意：启动程序...");
            Shell.WriteLine("/tWritten by Oyi319");
            Shell.WriteLine("/tBlog: http://blog.csdn.com/oyi319");
            Shell.WriteLine("{0}：{1}", "警告", "这是一条警告信息。");
            Shell.WriteLine("{0}：{1}", "错误", "这是一条错误信息！");
            Shell.WriteLine("{0}：{1}", "注意", "这是一条需要的注意信息。");
            Shell.WriteLine("");
            Console.ReadLine();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static class Shell
        {
            /// <summary>  
            /// 输出信息  
            /// </summary>  
            /// <param name="format"></param>  
            /// <param name="args"></param>  
            public static void WriteLine(string format, params object[] args)
            {
                WriteLine(string.Format(format, args));
            }

            /// <summary>  
            /// 输出信息  
            /// </summary>  
            /// <param name="output"></param>  
            public static void WriteLine(string output)
            {
                Console.ForegroundColor = GetConsoleColor(output);
                Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, output);
            }

            /// <summary>  
            /// 根据输出文本选择控制台文字颜色  
            /// </summary>  
            /// <param name="output"></param>  
            /// <returns></returns>  
            private static ConsoleColor GetConsoleColor(string output)
            {
                if (output.StartsWith("警告")) return ConsoleColor.Yellow;
                if (output.StartsWith("错误")) return ConsoleColor.Red;
                if (output.StartsWith("注意")) return ConsoleColor.Green;
                return ConsoleColor.Gray;
            }
        }
    }
}
