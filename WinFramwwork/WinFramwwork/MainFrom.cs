using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CodeGenerator.Client.WinFormsUI.Docking;
using FineEx;
using FineEx.Control.Forms;
using FineEx.Func;
using WinFormsUI;

namespace MainFrame
{
    public partial class MainFrom : FormEx
    {

        public MainFrom()
        {
            InitializeComponent();
        }

        #region 初始化导航

        private frmMenuFill _menuFill;// 二三级导航窗体

        private void MainFrame_Load(object sender, EventArgs e)
        {

            //设置状态栏显示内容
            SetInitStatus();

            //初始化左侧一级导航
            ucMenuLeft1.Init();
            //初始化二三级导航
            InitMenuFill(null, "首页");

            //初始化 单菜单效果
            InitMouseEnter();


            Timer _timer = new Timer();
            _timer.Tick += (sender1, e1) =>
            {
                _timer.Enabled = false;
                this.WindowState = FormWindowState.Maximized;
                this.Visible = true;
            };
            _timer.Interval = 100;
            _timer.Enabled = true;
            FineEx.Gloable.WarehouseIDDevelop = FineEx.Gloable.WarehouseID;

        }

        /// <summary>
        /// 初始化二三级导航窗体
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="moduleName"></param>
        private void InitMenuFill(string moduleCode, string moduleName)
        {
            if (_menuFill == null || _menuFill.IsDisposed)
            {
                _menuFill = new frmMenuFill();
                //  _menuFill.AllowUnDock = true;
                _menuFill.MainForm = this;
            }

            if (_menuFill.Text != moduleName)
            {
                _menuFill.IsHome = moduleName == "首页";
                try
                {
                    _menuFill.Init(moduleCode);
                }
                catch (Exception)
                {
                }


            }



            //首页，动态 改名称

            foreach (DockPanel p in DockManager.ListDocument)
            {
                if (_menuFill.Text == p.Form.Text)
                {
                    if (!p.Form.IsDisposed)
                    {
                        try
                        {
                            p.Form.Text = moduleName;
                            p.Form.Focus();
                            return;
                        }
                        catch (Exception ex)
                        {
                            p.Form.Visible = false;
                            DockManager.ListDocument.Remove(p);

                            _menuFill = null;
                            InitMenuFill(moduleCode, moduleName);
                            return;
                        }
                    }
                }
            }


            ShowCenter(_menuFill, moduleName);
        }

        //单击一级导航事件，动态加载二三级导航
        private void ucMenuLeft1_ButtonClick(string moduleCode, string moduleName)
        {
            InitMenuFill(moduleCode, moduleName);
        }

        /// <summary>
        /// 设置状态栏显示内容
        /// </summary>
        private void SetInitStatus()
        {
            //tslDate.Text = @"  " + DateTime.Today.ToString("yyyy'年'MM'月'd'日'  dddd");
            //tslOperator.Text = @"登录名：" + Gloable.LoginName;

            //tslCopyRight.Text = @"上海发网供应链管理有限公司 版权所有(C) 2006-" + DateTime.Today.ToString("yyyy");

            ucRightTool1.Warehouse = "Warehouse";
            ucRightTool1.UserName = "UserName";
        }

        #endregion

        #region 打开或关闭窗体

        /// <summary>
        /// 显示中心操作区的窗体
        /// </summary>
        /// <param name="wnd"></param>
        /// <returns></returns>
        public bool ShowCenter(BaseForm wnd)
        {
            return dockManager1.DockWindow(wnd, DockStyle.Fill);
        }

        /// <summary>
        /// 显示中心操作区的窗体
        /// </summary>
        /// <param name="wnd"></param>
        /// <returns></returns>
        public bool ShowCenter(DockWindow wnd)
        {
            return dockManager1.DockWindow(wnd, DockStyle.Fill);
        }

        /// <summary>
        /// 显示中心操作区的窗体
        /// </summary>
       /// <param name="title">标题</param>
        /// <returns></returns>
        public bool ShowCenter(  string title)
        {
            foreach (DockPanel p in DockManager.ListDocument)
            {
                if (title == p.Form.Text)
                {
                    if (!p.Form.IsDisposed)
                    {
                        try
                        {
                            p.Form.Focus();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            p.Form.Visible = false;
                            DockManager.ListDocument.Remove(p);
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 显示中心操作区的窗体
        /// </summary>
        /// <param name="wnd"></param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public bool ShowCenter(BaseForm wnd, string title)
        {
            return ShowCenter(wnd, title, DockStyle.Fill);
        }

        /// <summary>
        /// 显示中心操作区的窗体
        /// </summary>
        /// <param name="wnd"></param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public bool ShowCenter(BaseForm wnd, string title, DockStyle type)
        {
            foreach (DockPanel p in DockManager.ListDocument)
            {
                if (title == p.Form.Text)
                {
                    if (!p.Form.IsDisposed)
                    {
                        try
                        {
                            p.Form.Focus();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            p.Form.Visible = false;
                            DockManager.ListDocument.Remove(p);
                            return false;
                        }
                    }


                }
            }

            wnd.Activated += new EventHandler(this.dockPanel1_Activated);

            wnd.Deactivate += new EventHandler(this.dockPanel1_Deactivate);
            wnd.FormClosed += new FormClosedEventHandler(this.dockPanel1_FormClosed);
            wnd.VisibleChanged += new EventHandler(this.dockPanel1_VisibleChanged);

            wnd.Text = title;
            return dockManager1.DockWindow(wnd, type);
        }



        /// <summary>
        /// 关闭所有的内容窗体
        /// </summary>
        /// <param name="isThis">True 关闭所有，False关闭除此之外</param>
        public void CloseCenter(bool isThis)
        {
            try
            {
                ArrayList closeList = new ArrayList();

                foreach (DockPanel p in DockManager.ListDocument)
                {
                    if (_menuFill.Text != p.Form.Text)
                        if (isThis || !p.Form.Focused)
                            closeList.Add(p);
                }

                foreach (DockPanel wnd in closeList)
                {
                    wnd.Form.Visible = false;
                    DockManager.ListDocument.Remove(wnd);
                }

                closeList.Clear();

            }
            catch (Exception ex)
            {
                //MessageBox.ShowError(ex.Message);
            }
        }


        //当面板被激活时发生。
        public void dockPanel1_Activated(object sender, EventArgs e)
        {
            if (!((BaseForm)sender).IsAddEvent)
            {

                //ucToolMenu1.ClearEvent();

                ucToolMenu1.RandomClick += ((BaseForm)sender).ucToolMenu1_RandomClick;
                SetToolShowButton((BaseForm)sender);
            }
            ((BaseForm)sender).IsAddEvent = true;
        }


        //当窗体失去焦点并不再是活动窗体时发生。
        public void dockPanel1_Deactivate(object sender, EventArgs e)
        {
            ucToolMenu1.RandomClick -= ((BaseForm)sender).ucToolMenu1_RandomClick;
            ((BaseForm)sender).IsAddEvent = false;
        }
        //关闭窗体后发生
        public void dockPanel1_FormClosed(object sender, EventArgs e)
        {
            ucToolMenu1.RandomClick -= ((BaseForm)sender).ucToolMenu1_RandomClick;
            ((BaseForm)sender).IsAddEvent = false;
        }

        private void dockPanel1_VisibleChanged(object sender, EventArgs e)
        {
            if (!((BaseForm)sender).Visible)
            {
                ucToolMenu1.RandomClick -= ((BaseForm)sender).ucToolMenu1_RandomClick;
                ((BaseForm)sender).IsAddEvent = false;
            }
        }

        /// <summary>
        /// 设置工具框显示的按钮
        /// </summary>
        /// <param name="wnd"></param>
        private void SetToolShowButton(BaseForm wnd)
        {
            ucToolMenu1.Add.Visible = wnd.AddVisible; // 添加
            ucToolMenu1.Add.Text = wnd.AddText;
            ucToolMenu1.Edit.Visible = wnd.EditVisible; // 编辑,
            ucToolMenu1.Edit.Text = wnd.EditText;
            ucToolMenu1.Delete.Visible = wnd.DeleteVisible; // 删除
            ucToolMenu1.Delete.Text = wnd.DeleteText;
            ucToolMenu1.Save.Visible = wnd.SaveVisible; // 保存
            ucToolMenu1.Save.Text = wnd.SaveText;
            ucToolMenu1.Export.Visible = wnd.ExportVisible; // 导出
            ucToolMenu1.Export.Text = wnd.ExportText;
            ucToolMenu1.Import.Visible = wnd.ImportVisible; // 导入
            ucToolMenu1.Import.Text = wnd.ImportText;
            ucToolMenu1.Check.Visible = wnd.CheckVisible; // 筛选
            ucToolMenu1.Check.Text = wnd.CheckText;
            ucToolMenu1.Reset.Visible = wnd.ResetVisible; // 重置
            ucToolMenu1.Reset.Text = wnd.ResetText;
            ucToolMenu1.Prit.Visible = wnd.PritVisible; // 打印
            ucToolMenu1.Prit.Text = wnd.PritText;

            ucToolMenu1.Other1.Visible = wnd.Other1Visible;//自定义1
            ucToolMenu1.Other1.Text = wnd.Other1Text;
            ucToolMenu1.Other1Image = wnd.Other1Image;

            ucToolMenu1.Other2.Visible = wnd.Other2Visible;//自定义1
            ucToolMenu1.Other2.Text = wnd.Other2Text;
            ucToolMenu1.Other2Image = wnd.Other2Image;

            ucToolMenu1.Other3.Visible = wnd.Other3Visible;//自定义1
            ucToolMenu1.Other3.Text = wnd.Other3Text;
            ucToolMenu1.Other3Image = wnd.Other3Image;
        }


        #endregion

        #region 显示或隐藏工具框

        //private void pbUpDown_Click(object sender, EventArgs e)
        //{
        //    ucToolMenu1.UpDown = !ucToolMenu1.UpDown;

        //    //if (ucToolMenu1.UpDown)
        //    //{
        //    //    pbUpDown.Cursor = Cursors.PanNorth;
        //    //}
        //    //else
        //    //{
        //    //    pbUpDown.Cursor = Cursors.PanSouth;
        //    //}

        //    pbUpDown.Image = imgToolUpDown.Images[ucToolMenu1.UpDown ? 0 : 1];
        //}

        //private void pbUpDown_MouseEnter(object sender, EventArgs e)
        //{
        //    pbUpDown.BorderStyle = BorderStyle.FixedSingle;
        //}

        //private void pbUpDown_MouseLeave(object sender, EventArgs e)
        //{
        //    pbUpDown.BorderStyle = BorderStyle.None;
        //}

        #endregion

        #region 菜单

        //改密码
        private void menuEditPassword_Click(object sender, EventArgs e)
        {
          
        }




        //关于
        private void menuAbout_Click(object sender, EventArgs e)
        {
           // frmAbout curFrm = new frmAbout();
            
        }
        //退出
        private void menuLogOut_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        #region 窗口

        //帮助
        private void ucToolMenu1_HelpClick(object sender, EventArgs e)
        {
            //if (frmBottom == null || frmBottom.IsDisposed)
            //{
            //    frmBottom = new frmBottom();
            //    //frmBottom.Init();
            //}

            //ShowCenter(frmBottom, "帮助", DockStyle.Bottom);

            //FineEx.Func.CommonFunc.OpenURL("http://60.190.135.106:8091/OMS_WebService/FineExDBService_Route/help.HTML");
        }

        //帮助
        private void tsmHelp_Click(object sender, EventArgs e)
        {
            //ucToolMenu1_HelpClick(null, null);
        }

        //快速导航
        private void tsmRight_Click(object sender, EventArgs e)
        {
           
            //if (frmRight == null || frmRight.IsDisposed)
            //{
            //    frmRight = new frmRight();
            //    //frmBottom.Init();
            //}

            //ShowCenter(frmRight, "快速导航", DockStyle.Right);
        }

        private void tsmHome_Click(object sender, EventArgs e)
        {
            InitMenuFill(null, "首页");
        }



        /// <summary>
        /// 关闭所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCloseAll_Click(object sender, EventArgs e)
        {
            CloseCenter(true);
        }

        //关闭除此之外
        private void menuIsThisCloseAll_Click(object sender, EventArgs e)
        {
            CloseCenter(false);
        }
        #endregion

        /// <summary>
        /// 调用计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCalc_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        /// <summary>
        /// 打开计事本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuNoteBook_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe ");
        }


        /// <summary>
        /// 显示系统更新日志明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Diary(object sender, EventArgs e)
        {
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult ans = CommonFunc.ShowMessage("确定要退出系统吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void tolUpdate_Click(object sender, EventArgs e)
        {
            DialogResult ans = CommonFunc.ShowMessage("确定要更新系统至最新版本吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans != DialogResult.Yes) return;
            DataSet ds = new DataSet();
            string FileName = Application.StartupPath + "\\FineExConfig.xml";
            DataTable dt;
            DataRow dr;
            if (File.Exists(FileName))
            {
                ds.ReadXml(FileName);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Version"] = "1";
                }
                dt.AcceptChanges();
                dt.WriteXml(FileName);
            }
            Application.Restart();
        }

        private void tsmFineex_Click(object sender, EventArgs e)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["OfficialWebsite"];
            if (!string.IsNullOrEmpty(url))
                CommonFunc.OpenURL(url);
        }

        private void tmsOMS_Click(object sender, EventArgs e)
        {
            FineEx.Func.CommonFunc.OpenURL("http://oms.fineex.com/");
        }

        #endregion

        #region 单菜单效果
        /// <summary>
        /// 初始化 单菜单效果
        /// </summary>
        private void InitMouseEnter()
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.MouseEnter += new EventHandler(this.menu_MouseEnter);
                item.MouseLeave += new EventHandler(this.menu_MouseLeave);

                item.DropDownOpening += new EventHandler(this.menu_DropDownOpening);
                item.DropDownClosed += new EventHandler(this.menu_DropDownClosed);
            }
        }

        private ToolStripMenuItem selectItem;//记录选择的菜单项
        private bool _menuOpenState;//菜单展开的状态 True展开中，False。中闭关闭

        //菜单获得时点焦时，改变为黑色字体
        private void menu_MouseEnter(object sender, EventArgs e)
        {
            selectItem = ((ToolStripMenuItem)sender);
            selectItem.ForeColor = Color.Black;
        }

        //菜单获得时失去点焦时，改变为白色字体
        private void menu_MouseLeave(object sender, EventArgs e)
        {
            selectItem = ((ToolStripMenuItem)sender);

            //菜单为关闭中，且为黑色字体
            if (!_menuOpenState)
            {
                selectItem.ForeColor = Color.White;
            }
        }

        //菜单展开时，改变为黑色字体
        private void menu_DropDownOpening(object sender, EventArgs e)
        {
            _menuOpenState = true;
        }

        //菜单关闭时，改变为白色字体
        private void menu_DropDownClosed(object sender, EventArgs e)
        {
            _menuOpenState = false;
            if (selectItem!=null)
            if (!selectItem.Selected)
            {
                selectItem.ForeColor = Color.White;
            }


        }

        #endregion

        #region 系统工具栏

        //系统工具栏
        private void ucRightTool1_ClickEvent(object sender, RightToolBtuuon type)
        {
            switch (type)
            {
                case RightToolBtuuon.Close: // 关闭
                    Close();
                    break;
                case RightToolBtuuon.Max: // 最大 
                case RightToolBtuuon.Restore: // 正常
                    this.WindowState = WindowState == FormWindowState.Maximized
                        ? FormWindowState.Normal
                        : FormWindowState.Maximized;

                    break;
                case RightToolBtuuon.Min: // 最小
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case RightToolBtuuon.ShowTool: // 显示工具
                    ucToolMenu1.UpDown = !ucToolMenu1.UpDown;
                    break;
            }
        }

        #endregion



        /// <summary>
        /// 加载用户定义颜色
        /// </summary>
        public void LoadUserColor()
        {
            this.BackColor = Color.FromArgb(SkinMemage.TopColor.R, SkinMemage.TopColor.G, SkinMemage.TopColor.B);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// 加载用户定义颜色
        /// </summary>
        private void LoadUserColorAll()
        {
            LoadUserColor();
            ucRightTool1.LoadUserColor();
            ucToolMenu1.LoadUserColor();
            ucMenuLeft1.LoadUserColor();
        }

        private void ECS_Click(object sender, EventArgs e)
        {
            FineEx.Func.CommonFunc.OpenURL("http://60.190.135.108:808/Default.aspx");
        }


        private void menuOperat_Click(object sender, EventArgs e)
        {
            //FineEx.Func.CommonFunc.OpenURL("http://60.190.135.106:8091/OMS_WebService/FineExDBService_Route/help.HTML");
        }

    }
}