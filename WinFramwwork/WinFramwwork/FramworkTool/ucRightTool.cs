using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinFramwwork.Properties;
namespace FineEx.Control.Forms
{
    public partial class ucRightTool : UserControl
    {
        public ucRightTool()
        {
            InitializeComponent();


            Init();
        }

        [ToolboxItem(true), Description("显示的为库仓他"), Category("操作按钮")]
        public string Warehouse
        {
            get { return lblWarehouse.Text; }
            set { lblWarehouse.Text = value; }
        }
        [ToolboxItem(true), Description("单击事件"), Category("操作按钮")]
        public string UserName
        {
            get { return lblUser.Text; }
            set { lblUser.Text = value; }
        }

        public delegate void EventClick(object sender, RightToolBtuuon type);
        /// <summary>
        /// 单击事件
        /// </summary>
        [ToolboxItem(true), Description("单击事件"), Category("操作按钮")]
        public event EventClick ClickEvent;

        private void Init()
        {

            foreach (System.Windows.Forms.Control item in this.Controls)
            {
                if (item.Equals(pbxClose) | item.Equals(pbxMax) | item.Equals(pbxMin) | item.Equals(pbxShowTool) | item.Equals(pbxShowTool))
                {
                    item.MouseEnter += new System.EventHandler(this.pbx_MouseEnter);
                    item.MouseLeave += new System.EventHandler(this.pbx_MouseLeave);

                    item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbx_MouseDown);
                    item.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbx_MouseUp);


                    item.Click += new System.EventHandler(pbx_Click);
                }
            }

            pbxClose.Tag = "Close";
            pbxMax.Tag = "Restore";
            pbxMin.Tag = "Min";
            pbxShowTool.Tag = false;
        }

        //单击事件
        private void pbx_Click(object sender, EventArgs e)
        {
            if (ClickEvent != null)
            {
                RightToolBtuuon type = RightToolBtuuon.ShowTool;
                string name = ((PictureBox)sender).Tag.ToString();
                switch (name)
                {
                    case "Close":
                        type = RightToolBtuuon.Close;
                        break;
                    case "Max":
                        type = RightToolBtuuon.Max;
                        break;
                    case "Restore":
                        type = RightToolBtuuon.Restore;
                        pbxMax.Tag = pbxMax.Tag.ToString() == "Max" ? "Restore" : "Max";
                        break;
                    case "Min":
                        type = RightToolBtuuon.Min;
                        pbxMax.Tag = pbxMax.Tag.ToString() == "Max" ? "Restore" : "Max";
                        break;
                    default:
                        type = RightToolBtuuon.ShowTool;
                        pbxShowTool.Tag = !(bool)pbxShowTool.Tag;
                        break;
                }

                ClickEvent(sender, type);

            }
           
          

        }

        //进入可见部分
        private void pbx_MouseEnter(object sender, EventArgs e)
        {
            //((ToolStripButton)sender).BackColor = toolStrip1.BackColor;//System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(79)))), ((int)(((byte)(143)))));

            if (sender.Equals(pbxClose))
            {
                pbxClose.Image = global::WinFramwwork.Properties.Resources.close_highlight;
            }
            else if (sender.Equals(pbxMin))
            {
                pbxMin.Image = global::WinFramwwork.Properties.Resources.min_highlight;
            }
            else if (sender.Equals(pbxMax))
            {
                if (pbxMax.Tag.ToString() != "Max")
                {
                    pbxMax.Image = global::WinFramwwork.Properties.Resources.restore_highlight;
                }
                else
                {
                    pbxMax.Image = global::WinFramwwork.Properties.Resources.max_highlight;
                }
            }
            else if (sender.Equals(pbxShowTool))
            {
                if ((bool)pbxShowTool.Tag)
                {
                    pbxShowTool.Image = Resources.ToolDown_highlight;
                }
                else
                {
                    pbxShowTool.Image = Resources.ToolUp_highlight;
                }
            }
        }
        //离开可见部分
        private void pbx_MouseLeave(object sender, EventArgs e)
        {
            //((ToolStripButton)sender).BackColor = toolStrip1.BackColor;//System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(79)))), ((int)(((byte)(143)))));

            if (sender.Equals(pbxClose))
            {
                pbxClose.Image = global::WinFramwwork.Properties.Resources.close_normal;
            }
            else if (sender.Equals(pbxMin))
            {
                pbxMin.Image = global::WinFramwwork.Properties.Resources.min_normal;
            }
            else if (sender.Equals(pbxMax))
            {
                if (pbxMax.Tag.ToString() != "Max")
                {
                    pbxMax.Image = global::WinFramwwork.Properties.Resources.restore_normal;
                }
                else
                {
                    pbxMax.Image = global::WinFramwwork.Properties.Resources.max_normal;
                }
            }
            else if (sender.Equals(pbxShowTool))
            {
                if ((bool)pbxShowTool.Tag)
                {
                    pbxShowTool.Image = Resources.ToolDown_norma;
                }
                else
                {
                    pbxShowTool.Image = Resources.ToolUp_norma;
                }
            }
        }
        //按下键
        private void pbx_MouseDown(object sender, MouseEventArgs e)
        {
            //((ToolStripButton)sender).BackColor = toolStrip1.BackColor;//System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(79)))), ((int)(((byte)(143)))));

            if (sender.Equals(pbxClose))
            {
                pbxClose.Image = global::WinFramwwork.Properties.Resources.close_down;
            }
            else if (sender.Equals(pbxMin))
            {
                pbxMin.Image = global::WinFramwwork.Properties.Resources.min_down;
            }
            else if (sender.Equals(pbxMax))
            {
                if (pbxMax.Tag.ToString() != "Max")
                {
                    pbxMax.Image = global::WinFramwwork.Properties.Resources.restore_down;
                }
                else
                {
                    pbxMax.Image = global::WinFramwwork.Properties.Resources.max_down;
                }
            }
            else if (sender.Equals(pbxShowTool))
            {
                if ((bool)pbxShowTool.Tag)
                {
                    pbxShowTool.Image = Resources.ToolDown_down;
                }
                else
                {
                    pbxShowTool.Image = Resources.ToolUp_down;
                }
            }
        }
        //松开键
        private void pbx_MouseUp(object sender, MouseEventArgs e)
        {
            pbx_MouseEnter(sender, null);
        }



        /// <summary>
        /// 加载用户定义颜色
        /// </summary>
        public void LoadUserColor()
        {
            this.BackColor = System.Drawing.Color.FromArgb(SkinMemage.TopColor.R, SkinMemage.TopColor.G, SkinMemage.TopColor.B);
             
            this.ResumeLayout(false);
            this.PerformLayout();
        }


    }

    public enum RightToolBtuuon
    {
        /// <summary>
        /// 关闭
        /// </summary>
        Close,

        /// <summary>
        /// 最大 
        /// </summary>
        Max,

        /// <summary>
        /// 正常
        /// </summary>
        Restore,

        /// <summary>
        /// 最小
        /// </summary>
        Min,

        /// <summary>
        /// 显示工具
        /// </summary>
        ShowTool
    }
}
