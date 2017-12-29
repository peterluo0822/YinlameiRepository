using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using FineEx.Func;

namespace FineEx.Control.Forms
{
    [ToolboxItem(true), Description("主窗体工具框"), Category("框架")]
    public partial class ucToolMenu : UserControl
    {
        public ucToolMenu()
        {
            InitializeComponent();

            //初始化事件
            InitEvent();


            // 当标鼠移动上去显示为手型
            InitHandEvent();

            ////移动窗体
            //new MoveForms().Init(toolStrip1);
        }

        #region 控制大小显示

        private bool _upDown = true;

        /// <summary>
        /// True展开 False折叠
        /// </summary>
        public bool UpDown
        {
            get { return _upDown; }
            set
            {
                if (value)
                {
                    this.Height = 67;
                }
                else
                {
                    this.Height = 0;
                }

                _upDown = value;
            }
        }

        #endregion

        #region 控制显示的按钮
        /// <summary>
        /// 是否显示主框架上的退出按钮。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的退出按钮 "), Category("操作按钮")]
        public ToolStripButton SignOut
        {
            get { return tsbHelp; }
            set { tsbHelp = value; }
        }

        /// <summary>
        /// 是否显示主框架上的重新登录按钮。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的重新登录按钮 "), Category("操作按钮")]
        public ToolStripButton SignIn
        {
            get { return tsbSignIn; }
            set { tsbSignIn = value; }
        }


        /// <summary>
        /// 是否显示主框架上的添加按钮。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的添加按钮 "), Category("操作按钮")]
        public ToolStripButton Add
        {
            get { return tsbAdd; }
            set { tsbAdd = value; }
        }

        /// <summary>
        /// 是否显示主框架上的编辑按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的编辑按钮"), Category("操作按钮")]
        public ToolStripButton Edit
        {
            get { return tsbEdit; }
            set { tsbEdit = value; }
        }

        /// <summary>
        /// 是否显示主框架上的删除按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的删除按钮 "), Category("操作按钮")]
        public ToolStripButton Delete
        {
            get { return tsbDelete; }
            set { tsbDelete = value; }
        }

        /// <summary>
        /// 是否显示主框架上的保存按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的保存按钮 "), Category("操作按钮")]
        public ToolStripButton Save
        {
            get { return tsbSave; }
            set { tsbSave = value; }
        }

        /// <summary>
        /// 是否显示主框架上的导出按钮 
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的导出按钮 "), Category("操作按钮")]
        public ToolStripButton Export
        {
            get { return tsbExport; }
            set { tsbExport = value; }
        }

        /// <summary>
        /// 是否显示主框架上的导入按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的导入按钮 "), Category("操作按钮")]
        public ToolStripButton Import
        {
            get { return tsbImport; }
            set { tsbImport = value; }
        }


        /// <summary>
        /// 是否显示主框架上的筛选按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的筛选按钮 "), Category("操作按钮")]
        public ToolStripButton Check
        {
            get { return tsbCheck; }
            set { tsbCheck = value; }
        }

        /// <summary>
        /// 是否显示主框架上的重置按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的重置按钮 "), Category("操作按钮")]
        public ToolStripButton Reset
        {
            get { return tsbReset; }
            set { tsbReset = value; }
        }

        /// <summary>
        /// 是否显示主框架上的打印按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的打印按钮 "), Category("操作按钮")]
        public ToolStripButton Prit
        {
            get { return tsbPrit; }
            set { tsbPrit = value; }
        }

        /// <summary>
        /// 是否显示主框架上的自定义按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的自定义按钮 "), Category("操作按钮")]
        public ToolStripButton Other1
        {
            get { return tsbOther1; }
            set { tsbOther1 = value; }
        }

        private int _other1Image = 0;
        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue(0), Description("主框架上的自定义1按钮显示的图片 "), Category("操作按钮")]
        public int Other1Image
        {
            get { return _other1Image; }
            set
            {
                _other1Image = value;
                return;
                //if (value<0||value > imgOther.Images.Count - 1)
                //{
                //    return;
                //}

                //tsbOther1.Image = imgOther.Images[value];
                //_other1Image = value;
            }
        }

        /// <summary>
        /// 是否显示主框架上的自定义按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的自定义按钮 "), Category("操作按钮")]
        public ToolStripButton Other2
        {
            get { return tsbOther2; }
            set { tsbOther2 = value; }
        }

        private int _other2Image = 0;
        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue(0), Description("主框架上的自定义1按钮显示的图片 "), Category("操作按钮")]
        public int Other2Image
        {
            get { return _other2Image; }
            set
            {
                _other2Image = value;
                return;

                //if (value < 0 || value > imgOther.Images.Count - 1)
                //{
                //    return;
                //}

                //tsbOther2.Image = imgOther.Images[value];
                //_other2Image = value;
            }
        }
        /// <summary>
        /// 是否显示主框架上的自定义按钮 。
        /// </summary>
        [ToolboxItem(true), Description("是否显示主框架上的自定义按钮 "), Category("操作按钮")]
        public ToolStripButton Other3
        {
            get { return tsbOther3; }
            set { tsbOther3 = value; }
        }
        private int _other3Image = 0;
        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue(0), Description("主框架上的自定义1按钮显示的图片 "), Category("操作按钮")]
        public int Other3Image
        {
            get { return _other3Image; }
            set
            {
                _other3Image = value; return;
                //if (value < 0 || value > imgOther.Images.Count - 1)
                //{
                //    return;
                //}

                //tsbOther3.Image = imgOther.Images[value];
                //_other3Image = value;
            }
        }
        #endregion

        #region 事件

        /// <summary>
        /// 初始化事件
        /// </summary>
        public void InitEvent()
        {
            //单个操作事件
            tsbHelp.Click += HelpClick; //

            tsbAdd.Click += AddClick; //添加
            tsbEdit.Click += EditClick; //编辑
            tsbDelete.Click += DeleteClick; //删除
            tsbSave.Click += SaveClick; //保存
            tsbExport.Click += ExportClick; //导出
            tsbImport.Click += ImportClick; //导入
            tsbCheck.Click += CheckClick; //筛选
            tsbReset.Click += ResetClick; //重置
            tsbPrit.Click += PritClick; //打印
            tsbOther1.Click += Other1Click;//自定义
            tsbOther2.Click += Other2Click;//自定义
            tsbOther3.Click += Other3Click;//自定义

            //总操作事件
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item.Name.IndexOf("tsb") >= 0)
                {
                    item.Click += tsb_Click;
                }
            }
        }

        /// <summary>
        /// 在单击 框架上的添加按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的帮助按钮 时发生"), Category("操作按钮")]
        public event EventHandler HelpClick;



        /// <summary>
        /// 在单击 框架上的添加按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的添加按钮 时发生"), Category("操作按钮")]
        public event EventHandler AddClick;


        /// <summary>
        /// 在单击 框架上的编辑按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的编辑按钮 时发生"), Category("操作按钮")]
        public event EventHandler EditClick;

        /// <summary>
        /// 在单击 框架上的删除按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的删除按钮 时发生"), Category("操作按钮")]
        public event EventHandler DeleteClick;

        /// <summary>
        /// 在单击 框架上的保存按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的保存按钮 时发生"), Category("操作按钮")]
        public event EventHandler SaveClick;

        /// <summary>
        /// 在单击 框架上的导出按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的导出按钮 时发生"), Category("操作按钮")]
        public event EventHandler ExportClick;

        /// <summary>
        /// 在单击 框架上的导入按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的导入按钮 时发生"), Category("操作按钮")]
        public event EventHandler ImportClick;

        /// <summary>
        /// 在单击 框架上的筛选按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的筛选按钮 时发生"), Category("操作按钮")]
        public event EventHandler CheckClick;

        /// <summary>
        /// 在单击 框架上的重置按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的重置按钮 时发生"), Category("操作按钮")]
        public event EventHandler ResetClick;

        /// <summary>
        /// 在单击 框架上的打印按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上的打印按钮 时发生"), Category("操作按钮")]
        public event EventHandler PritClick;

        /// <summary>
        /// 在单击 框架上的自定义1 时发生。 
        /// </summary>
        [Description("在单击 框架上的自定义1按钮 时发生"), Category("操作按钮")]
        public event EventHandler Other1Click;

        /// <summary>
        /// 在单击 框架上的自定义2 时发生。 
        /// </summary>
        [Description("在单击 框架上的自定义2按钮 时发生"), Category("操作按钮")]
        public event EventHandler Other2Click;

        /// <summary>
        /// 在单击 框架上的自定义3 时发生。 
        /// </summary>
        [Description("在单击 框架上的自定义3按钮 时发生"), Category("操作按钮")]
        public event EventHandler Other3Click;

        [ComVisible(true)]
        public delegate void HandlerClick(object sender, EventArgs e, ButtonType type);

        /// <summary>
        /// 在单击 框架上任意按钮 时发生。 
        /// </summary>
        [Description("在单击 框架上任意按钮 时发生"), Category("操作按钮")]
        public event HandlerClick RandomClick;


        private void tsb_Click(object sender, EventArgs e)
        {
            if (RandomClick != null)
            {
                string name = ((ToolStripItem)(sender)).Name;
                name = name.Replace("tsb", "");

                foreach (
                    ButtonType item in
                        Enum.GetValues(typeof(ButtonType)).Cast<ButtonType>().Where(item => name == item.ToString()))
                {
                    RandomClick(sender, e, item);
                }
            }

        }

        public void ClearEvent()
        {
            if (RandomClick != null)
            {
                foreach (Delegate d in RandomClick.GetInvocationList())
                {
                    //得到方法名
                    object delObj = d.GetType().GetProperty("Method").GetValue(d, null);
                    string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);
                    System.Diagnostics.Debug.Print(funcName);
                    RandomClick -= d as HandlerClick;
                }
            }
        }

        #endregion

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

        private void tsb_MouseEnter(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Hand;

            if (sender is ToolStripButton)
            {
                ((ToolStripButton)sender).ForeColor = Color.Black;
            }

        }

        private void tsb_MouseLeave(object sender, EventArgs e)
        {
            toolStrip1.Cursor = Cursors.Default;

            if (sender is ToolStripButton)
            {
                ((ToolStripButton)sender).ForeColor = Color.White;
            }
        }

        #endregion

        #region 单击LOGO 打开官网

        //单击LOGO 打开官网
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //OfficialWebsite
            string url = ConfigurationManager.AppSettings["OfficialWebsite"];
            if (!string.IsNullOrEmpty(url))
                CommonFunc.OpenURL(url);
        }

        #endregion


        #region 操作事件

        //退出
        private void tsbSignIn_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(@"确认重新登录吗？", @"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            //    DialogResult.Yes)
            //{
            //    Application.Restart();
            //}
            if (MessageBox.Show(@"确认退出吗？", @"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //帮助
        private void tsbHelp_Click(object sender, EventArgs e)
        {
            //if(HelpClick;!= null)
              HelpClick(sender,e);
        }

        #endregion


        /// <summary>
        /// 加载用户定义颜色
        /// </summary>
        public void LoadUserColor()
        {
            this.BackColor = toolStrip1.BackColor = Color.FromArgb(SkinMemage.TopColor.R, SkinMemage.TopColor.G, SkinMemage.TopColor.B);

            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

    }




}
