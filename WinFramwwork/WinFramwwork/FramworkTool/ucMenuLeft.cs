using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FineEx.Control.Forms
{
    [ToolboxItem(false), Description("主窗体左侧一级导航"), Category("框架")]
    public partial class ucMenuLeft : UserControl
    {
        public ucMenuLeft()
        {
            InitializeComponent();

            ButtonInfo info = new ButtonInfo();
            info.ModuleName = homeText;
            info.Image1 = "0";
            info.Image2 = "1";

            tsbMaxMin.Tag = info;
            tsbMaxMin.Text = homeText;


        }
        /// <summary>
        /// 简洁　
        /// </summary>
        private readonly string homeText = @"  ";


        [Description("单击控件按钮发生，被导航按钮外。"), Category("自定义事件")]
        public event ButtonEventHandler ButtonClick;

        public delegate void ButtonEventHandler(string moduleCode, string moduleName);


        /// <summary>
        /// 初始化左侧一级导航
        /// </summary>
        public void Init()
        {
            ButtonInfo info;

            LoadImage(Application.StartupPath + "\\Images\\导航图标\\");
            for (int i = 0; i < CommonFrame.NavigationDataSource.Tables[0].Rows.Count; i++)
            {
                info = new ButtonInfo();

                info.ModuleCode = CommonFrame.NavigationDataSource.Tables[0].Rows[i]["ModuleCode"].ToString();
                info.ModuleName = CommonFrame.NavigationDataSource.Tables[0].Rows[i]["ModuleName"].ToString();
                //info.Image1 = "Home.jpg";
                //info.Image2 = "Home.jpg";

                GetButton(info, i + 1);
            }

            InitHandEvent();
        }

        /// <summary>
        /// 加载用户定义颜色
        /// </summary>
        public void LoadUserColor()
        {
              this.BackColor =   toolStrip1.BackColor = Color.FromArgb(SkinMemage.LeftColor.R, SkinMemage.LeftColor.G, SkinMemage.LeftColor.B);
    
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// 添加按键按键
        /// </summary>
        /// <param name="info"></param>
        /// <param name="index"></param>
        private void GetButton(ButtonInfo info, int index)
        {
            var tsb = new ToolStripButton();

            tsb.ForeColor = System.Drawing.Color.White;
            tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsb.Size = new System.Drawing.Size(90, 36);
            tsb.Text = " " + info.ModuleName;
            tsb.Name = "tsb" + index;
            tsb.Margin = new System.Windows.Forms.Padding(5);

            //if (index + 1 < imageList1.Images.Count)
            tsb.Image = imageList1.Images[info.ModuleName];

            tsb.Click += this.Button_Click;
            tsb.Tag = info;
            this.toolStrip1.Items.Add(tsb);
        }


        //最大最小按钮事件
        private void tsbMaxMin_Click(object sender, EventArgs e)
        {
            bool falg = tsbMaxMin.DisplayStyle != ToolStripItemDisplayStyle.Image;

            foreach (var c in toolStrip1.Items)
            {
                if (c is ToolStripButton)
                {
                    ((ToolStripButton)c).DisplayStyle = falg ? ToolStripItemDisplayStyle.Image : ToolStripItemDisplayStyle.ImageAndText;
                }
            }


            if (falg)
            {
                this.Width = 54;
                this.tsbMaxMin.Image = global::WinFramwwork.Properties.Resources.右箭头;
                //tsbMaxMin.Image = imageList1.Images[1];

            }
            else
            {
                this.Width = 113;
                //tsbMaxMin.Image = imageList1.Images[0];
                this.tsbMaxMin.Image = global::WinFramwwork.Properties.Resources.左箭头;
            }

        }

        //上次选择的项
        private object selecteItem;

        //所有按钮的单击事件
        private void Button_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null  )
            {
                ButtonInfo info = (ButtonInfo)((ToolStripButton)sender).Tag;
                ButtonClick(info.ModuleCode, info.ModuleName);
                if (selecteItem != null)
                {
                    ((ToolStripButton)selecteItem).BackColor = Color.Teal;
                    ((ToolStripButton)selecteItem).ForeColor = Color.White;
                }

                ((ToolStripButton)sender).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(205)))), ((int)(((byte)(254)))));
                ((ToolStripButton)sender).ForeColor = Color.White;


                selecteItem = sender;
            }

        }

        
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="path"></param>
        private void LoadImage(string path)
        { 
            //添加文件
            foreach (string file in Directory.GetFiles(path))
            {
                FileInfo info = new FileInfo(file);
                imageList1.Images.Add(info.Name.Replace(info.Extension, ""), System.Drawing.Image.FromFile(file));
            }
        }


        #region  当标鼠移动上去显示为手型

        /// <summary>
        /// 当标鼠移动上去显示为手型
        /// </summary>
        private void InitHandEvent()
        {
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item.Name.IndexOf("tsb") >= 0)
                {
                    item.MouseEnter += tsb_MouseEnter;
                    item.MouseLeave += tsb_MouseLeave;
                }
            }

        }

        //变黑
        private void tsb_MouseEnter(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Hand;

            if (sender is ToolStripButton)
            {
                ((ToolStripButton)sender).ForeColor = Color.Black;
            }

        }

        //变白
        private void tsb_MouseLeave(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Default;

            if (!sender.Equals(selecteItem) && sender is ToolStripButton)
            {
                ((ToolStripButton)sender).ForeColor = Color.White;
            }
        }

        #endregion

    }
    public class ButtonInfo
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }


    }

}
