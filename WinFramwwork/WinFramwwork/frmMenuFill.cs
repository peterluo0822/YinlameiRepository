using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CodeGenerator.Client.WinFormsUI.Controls;
using CodeGenerator.Client.WinFormsUI.Docking;
using FineEx.Control.Forms;

namespace MainFrame
{
    public partial class frmMenuFill : FineEx.Control.Forms.BaseForm
    {
        public frmMenuFill()
        {
            InitializeComponent();
            //WorkAreas

        }

        private bool _isHome = true;

        /// <summary>
        /// 是否为首页 True首页，显示背景图，Flase导航页，显示导航。
        /// </summary>
        public bool IsHome
        {
            get { return _isHome; }

            set
            {

                pictureBox1.Visible = value;
                listView1.Visible = !value;

                _isHome = value;
            }
        }




        /// <summary>
        /// 主窗体
        /// </summary>
        public MainFrom MainForm { get; set; }

        /// <summary>
        /// 初始化二三级目录
        /// </summary>
        /// <param name="moduleCode"></param>
        public void Init(string moduleCode)
        {
            if (string.IsNullOrEmpty(moduleCode))
                return;

            if (!isInit)
                LoadImage(Application.StartupPath + "\\Images\\");

            this.listView1.BeginUpdate();

            listView1.Groups.Clear();
            listView1.Items.Clear();


            string imageName = string.Empty;
            //int index = 0;
            //历遍父表所有的行并赋值给parentRow
            foreach (DataRow parentRow in FineEx.Control.Forms.CommonFrame.NavigationDataSource.Relations["左联"].ParentTable.Select(" ParentModuleCode='" + moduleCode + "'"))
            {
                //如果没有二级模块，直接显示。
                if (!string.IsNullOrEmpty(parentRow["ClassName"].ToString()))
                {
                    ListViewGroup defaultGroup = listView1.Groups["其它"];
                    if (defaultGroup == null)
                    {
                        defaultGroup = new ListViewGroup("其它", "其它");
                        listView1.Groups.Add(defaultGroup);
                    }
                    imageName = parentRow["ModuleName"].ToString();
                    if (!imageList1.Images.ContainsKey(imageName))
                    {
                        imageName = "通用图标";
                    }

                    ListViewItem item = new ListViewItem(parentRow["ModuleName"].ToString(), imageName, defaultGroup);
                    //item.ToolTipText = parentRow["ModuleName"].ToString();

                    string[] strs = { parentRow["AssemBlyName"].ToString(), parentRow["ClassName"].ToString() };
                    item.Tag = strs;
                    listView1.Items.Add(item);

                    continue;
                }

                ListViewGroup group = new ListViewGroup(parentRow["ModuleName"].ToString());
                listView1.Groups.Insert(listView1.Groups.Count - (listView1.Groups["其它"] != null ? 1 : 0), group);



                //index = 0;
                //历遍parentRow相关的所有子行并赋值给childRow
                foreach (DataRow childRow in parentRow.GetChildRows(FineEx.Control.Forms.CommonFrame.NavigationDataSource.Relations["左联"]))
                {

                    imageName = childRow["ModuleName"].ToString();
                    if (!imageList1.Images.ContainsKey(imageName))
                    {
                        imageName = "通用图标";
                    }

                    ListViewItem item = new ListViewItem(childRow["ModuleName"].ToString(), imageName, group);
                    //item.ToolTipText = childRow["ModuleName"].ToString();

                    string[] strs = { childRow["AssemBlyName"].ToString(), childRow["ClassName"].ToString() ,moduleCode};
                    item.Tag = strs;
                    listView1.Items.Add(item);

                    //index++;
                }
            }
            listView1.SetGroupState(ListViewGroupState.Collapsible | ListViewGroupState.Normal);
            this.listView1.EndUpdate();


        }

        /// <summary>
        /// 运行状态 True运行中
        /// </summary>
        private bool isRun;

        //单击导航，打开对面的页面
        private void listView1_Click(object sender, System.EventArgs e)
        {
            if (isRun)
                return;

            isRun = true;

            try
            {
                string[] strs = (string[])((ListView)(sender)).FocusedItem.Tag;
                string name = ((ListView)(sender)).FocusedItem.Text;

                if (!MainForm.ShowCenter(name))
                    OpenFormFunc(strs[0], strs[1], name,strs[2]);
            }
            finally
            {
                isRun = false;
            }

            //OpenFormFunc(strs[0], strs[1], "",  name,"", "");

        }

        /// <summary>
        /// 通过反射，加载对应的页面
        /// </summary>
        /// <param name="assemblyName">DLL文件</param>
        /// <param name="funcClassName">类名</param>
        /// <param name="funcName">打开的窗体名</param>
        /// <returns>True成功</returns>
        private bool OpenFormFunc(string assemblyName, string funcClassName, string funcName, string moduleCode)
        {
            StateForm stateForm = new StateForm();
            stateForm.ShowMessage();

            try
            {
                Assembly assembly = Assembly.LoadFrom(Application.StartupPath + "\\" + assemblyName);

                Type typeToLoad = assembly.GetType(funcClassName);
                FineEx.Control.Forms.BaseForm frm = (FineEx.Control.Forms.BaseForm)Activator.CreateInstance(typeToLoad);
                frm.ModuleCode = moduleCode;
                if (frm.DockType != DockContainerType.Document)
                    frm.DockType = DockContainerType.Document;

                bool flag = MainForm.ShowCenter(frm, funcName);
                if (!flag)
                    return MainForm.ShowCenter(frm, funcName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                stateForm.Close();
            }
        }


        private bool isInit;

        private void LoadImage(string path)
        {
            isInit = true;
            foreach (string directory in Directory.GetDirectories(path))
            {
                //递夹件文归递归
                LoadImage(directory);

            }
            //添加文件
            foreach (string file in Directory.GetFiles(path))
            {
                FileInfo info = new FileInfo(file);
                imageList1.Images.Add(info.Name.Replace(info.Extension, ""), System.Drawing.Image.FromFile(file));
            }
        }

        private void frmMenuFill_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }



    }
}
