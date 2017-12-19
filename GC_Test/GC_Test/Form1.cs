using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GC_Test
{
    public partial class Form1 : Form
    {
        private void button1_Click(object sender, EventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);//到达时间的时候执行事件；    
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；    
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }
        public void ChkSrv(object source, System.Timers.ElapsedEventArgs e)
        {
            int Hour = e.SignalTime.Hour;
            int Minute = e.SignalTime.Minute;
            int Second = e.SignalTime.Second;
            GC.Collect();
            button1.Text = string.Format("{0}:{1}:{2}", Hour, Minute, Second);
        }
        public Form1()
        {
            InitializeComponent();
        }

    }
}
