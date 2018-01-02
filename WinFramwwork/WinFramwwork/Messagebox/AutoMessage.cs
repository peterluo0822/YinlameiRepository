using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFramwwork.Messagebox
{
    public partial class AutoMessage : Form,IDisposable
    {
        public int _showTime = 3;
        public string _Message = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="showTime">信息显示的时间</param>
        /// <param name="Message">需要显示的信息</param>
        public AutoMessage( int showTime ,string Message)
        {
            InitializeComponent();
            Type(this, 20, 0.1);
            _showTime = showTime;
            _Message = Message;
        }

        public void InScreen()
        {

        }

        public  void OutScreen()
        {

        }

        /// <summary>
        /// 圆角：radius=圆角弧度   rect是要做圆角的矩形
        /// </summary>
        public void SetWindowRegion(int width, int height)
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, width, height);
            FormPath = GetRoundedRectPath(rect, 8);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角      
            path.AddArc(arcRect, 180, 90);
            //   右上角      
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角      
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角      
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }


        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(
                new Point[] {
            new Point(0, sender.Height / p_1),
            new Point(sender.Width / p_1, 0),
            new Point(sender.Width - sender.Width / p_1, 0),
            new Point(sender.Width, sender.Height / p_1),
            new Point(sender.Width, sender.Height - sender.Height / p_1),
            new Point(sender.Width - sender.Width / p_1, sender.Height),
            new Point(sender.Width / p_1, sender.Height),
            new Point(0, sender.Height - sender.Height / p_1) },

                (float)p_2);

            sender.Region = new Region(oPath);
        }


        private void pbxClose_Click(object sender, EventArgs e)
        {
            timer_In.Stop();
            timer_Show.Stop();
            timer_out.Start();            
        }

        #region 移动窗体
        Point pt;
        private void Controls_MouseDown(object sender, MouseEventArgs e)
        {
            pt = Cursor.Position;
        }

        private void Controls_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int px = Cursor.Position.X - pt.X;
                int py = Cursor.Position.Y - pt.Y;
                this.Location = new Point(this.Location.X + px, this.Location.Y + py);
                pt = Cursor.Position;
            }
        }
        #endregion

        #region 进入界面
        #endregion

        #region 离开界面
        #endregion

        #region 显示
        #endregion

        Point InLocation;
        //任务栏的高度
        private int diffHeight;
        private void AutoMessage_Load(object sender, EventArgs e)
        {
            //提示信息
            lab_Message.Text = _Message;

            //显示时长
            lab_ShowTome.Text = _showTime.ToString();

            //任务栏的高度
            diffHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height- SystemInformation.WorkingArea.Height;

            //最终的location
            InLocation = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height - diffHeight);

            this.Location= new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height- diffHeight);
            timer_In.Start();
        }

        private void timer_In_Tick(object sender, EventArgs e)
        {
            ShowInPosition(InLocation.Y);
        }
        private void timer_out_Tick(object sender, EventArgs e)
        {
            ShowOutPosition(InLocation.Y);
        }
        private void ShowOutPosition(object obj)
        {
            if (this.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                WaitCallback del = new WaitCallback(ShowOutPosition);
                this.Invoke(del, obj);
            }
            else
            {
                if (this.Location.Y< System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y +10> System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height ? System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height : this.Location.Y + 10);
                }
                else
                {
                    timer_out.Stop();
                    this.Dispose();
                }
            }
        }
        private void ShowInPosition(object obj)
        {
            if (this.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                WaitCallback del = new WaitCallback(ShowInPosition);
                this.Invoke(del, obj);
            }
            else
            {
                if (InLocation.Y < this.Location.Y)
                {
                    this.Location = new Point(this.Location.X, this.Location.Y - 10 < InLocation.Y ? InLocation.Y : this.Location.Y - 10);
                }
                else
                {
                    timer_In.Stop();
                    timer_Show.Start();
                }
            }
        }

        /// <summary>
        /// 信息显示的时间，默认显示3秒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Show_Tick(object sender, EventArgs e)
        {
            ShowTimeText(InLocation.Y);
        }
        private void ShowTimeText(object obj)
        {
            if (lab_ShowTome.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                WaitCallback del = new WaitCallback(ShowTimeText);
                lab_ShowTome.Invoke(del, obj);
            }
            else
            {
                if (_showTime>0)
                {
                    _showTime = _showTime - 1;
                    lab_ShowTome.Text = _showTime.ToString();
                    if (_showTime == 0)
                    {
                        timer_Show.Stop();
                        timer_out.Start();
                    }
                }
            }
        }
    }
}
