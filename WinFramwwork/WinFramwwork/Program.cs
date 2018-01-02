using MainFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFramwwork.test;

namespace WinFramwwork
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainFrom());  
            //Application.Run(new FrmTest());
            Application.Run(new Messagebox.AutoMessage(5,"hello"));
            
        }
    }
}
