using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFramwwork.Messagebox
{
  public  class FlyMessagebox
    {
        /// <summary>
        /// 显示提示对话框
        /// </summary>
        /// <param name="showTime">显示时长</param>
        /// <param name="Message">显示信息</param>
        public static void ShowAuto(int showTime, string Message)
        {
            Thread showThread = new Thread(showMessage);
            showThread.Start(new string[] { showTime.ToString(),Message});
        }

        private static void showMessage(object obj)
        {
            try
            {
                if (obj == null) throw new Exception("此方法不能没有参数");
                string[] para =( string[])obj;
                if (para.Length<2) throw new Exception("此方法需要传递长度为2的数组参数,1显示时长，2显示消息");
                int showTime = Convert.ToInt32(para[0]);
                string Message = para[1];
                AutoMessage frm = new AutoMessage(showTime,Message);
                frm.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

     
    }
}
