using CodeGenerator.Client.WinFormsUI.Docking;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace FineEx.Control.Forms
{
    public partial class BaseForm : DockWindow
    {

        private string _moduleCode = string.Empty;

        public string ModuleCode
        {
            get { return _moduleCode; }
            set { _moduleCode = value; }
        }

        public BaseForm()
        {
            InitializeComponent();

        }

        #region 属性


        #region 控制显示的按钮
        public bool IsAddEvent { get; set; }


        /// <summary>
        /// 是否显示主框架上的添加按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的添加按钮 "), Category("操作按钮")]
        public bool AddVisible { get; set; }

        private string _addText = "添加";
        /// <summary>
        /// 是否显示主框架上的添加按钮 。
        /// </summary>
        [DefaultValue("添加"), Description("主框架上的添加按钮显示的文本 "), Category("操作按钮")]
        public string AddText
        {
            get { return _addText; }
            set { _addText = value; }
        }
         

        /// <summary>
        /// 是否显示主框架上的编辑按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的编辑按钮 "), Category("操作按钮")]
        public bool EditVisible { get; set; }

        private string _editText = "编辑";
        /// <summary>
        /// 是否显示主框架上的编辑按钮 。
        /// </summary>
        [DefaultValue("编辑"), Description("主框架上的编辑按钮显示的文本 "), Category("操作按钮")]
        public string EditText
        {
            get { return _editText; }
            set { _editText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的删除按钮 。
        /// </summary>
        [Description("是否显示主框架上的删除按钮 "), Category("操作按钮")]
        public bool DeleteVisible { get; set; }

        private string _deleteText = "删除";
        /// <summary>
        /// 是否显示主框架上的删除按钮 。
        /// </summary>
        [DefaultValue("删除"), Description("主框架上的删除按钮显示的文本 "), Category("操作按钮")]
        public string DeleteText
        {
            get { return _deleteText; }
            set { _deleteText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的保存按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的保存按钮 "), Category("操作按钮")]
        public bool SaveVisible { get; set; }

        private string _saveText = "保存";
        /// <summary>
        /// 是否显示主框架上的保存按钮 。
        /// </summary>
        [DefaultValue("保存"), Description("主框架上的保存按钮显示的文本 "), Category("操作按钮")]
        public string SaveText
        {
            get { return _saveText; }
            set { _saveText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的导出按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的导出按钮 "), Category("操作按钮")]
        public bool ExportVisible { get; set; }

        private string _exportText = "导出";
        /// <summary>
        /// 是否显示主框架上的导出按钮 。
        /// </summary>
        [DefaultValue("导出"), Description("主框架上的导出按钮显示的文本 "), Category("操作按钮")]
        public string ExportText
        {
            get { return _exportText; }
            set { _exportText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的导入按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的导入按钮 "), Category("操作按钮")]
        public bool ImportVisible { get; set; }

        private string _importText = "导入";
        /// <summary>
        /// 是否显示主框架上的导入按钮 。
        /// </summary>
        [DefaultValue("导入"), Description("主框架上的导入按钮显示的文本 "), Category("操作按钮")]
        public string ImportText
        {
            get { return _importText; }
            set { _importText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的筛选按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的筛选按钮 "), Category("操作按钮")]
        public bool CheckVisible { get; set; }
        private string _ceckText = "筛选";
        /// <summary>
        /// 是否显示主框架上的筛选按钮 。
        /// </summary>
        [DefaultValue("筛选"), Description("主框架上的筛选按钮显示的文本 "), Category("操作按钮")]
        public string CheckText
        {
            get { return _ceckText; }
            set { _ceckText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的重置按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的重置按钮 "), Category("操作按钮")]
        public bool ResetVisible { get; set; }

        private string _resetText = "重置";

        /// <summary>
        /// 是否显示主框架上的重置按钮 。
        /// </summary>
        [DefaultValue("重置"), Description("主框架上的重置按钮显示的文本 "), Category("操作按钮")]
        public new string ResetText
        {
            get { return _resetText; }
            set { _resetText = value; }
        }


        /// <summary>
        /// 是否显示主框架上的打印按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的打印按钮 "), Category("操作按钮")]
        public bool PritVisible { get; set; }

        private string _pritText = "打印";
        /// <summary>
        /// 是否显示主框架上的打印按钮 。
        /// </summary>
        [DefaultValue("打印"), Description("主框架上的打印按钮显示的文本 "), Category("操作按钮")]
        public string PritText
        {
            get { return _pritText; }
            set { _pritText = value; }
        }

        /// <summary>
        /// 是否显示主框架上的打印按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的自定义1按钮 "), Category("操作按钮")]
        public bool Other1Visible { get; set; }

        private string _other1Text = "自定义1";

        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue("自定义1"), Description("主框架上的自定义1按钮显示的文本 "), Category("操作按钮")]
        public string Other1Text
        {
            get { return _other1Text; }
            set { _other1Text = value; }
        }

        private int _other1Image = 0;
        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue(0), Description("主框架上的自定义1按钮显示的图片 "), Category("操作按钮")]
        public int Other1Image
        {
            get { return _other1Image; }
            set { _other1Image = value; }
        }


        /// <summary>
        /// 是否显示主框架上的打印按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的自定义1按钮 "), Category("操作按钮")]
        public bool Other2Visible { get; set; }

        private string _other2Text = "自定义2";

        /// <summary>
        /// 主框架上的自定义2按钮显示的文本 。
        /// </summary>
        [DefaultValue("自定义2"), Description("主框架上的自定义2按钮显示的文本 "), Category("操作按钮")]
        public string Other2Text
        {
            get { return _other2Text; }
            set { _other2Text = value; }
        }

        private int _other2Image = 0;
        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue(0), Description("主框架上的自定义2按钮显示的图片 "), Category("操作按钮")]
        public int Other2Image
        {
            get { return _other2Image; }
            set { _other2Image = value; }
        }


        /// <summary>
        /// 是否显示主框架上的打印按钮 。
        /// </summary>
        [DefaultValue(false), Description("是否显示主框架上的自定义3按钮 "), Category("操作按钮")]
        public bool Other3Visible { get; set; }

        private string _other3Text = "自定义3";

        /// <summary>
        /// 主框架上的自定义3按钮显示的文本 。
        /// </summary>
        [DefaultValue("自定义3"), Description("主框架上的自定义3按钮显示的文本 "), Category("操作按钮")]
        public string Other3Text
        {
            get { return _other3Text; }
            set { _other3Text = value; }
        }

        private int _other3Image = 0;
        /// <summary>
        /// 主框架上的自定义1按钮显示的文本
        /// </summary>
        [DefaultValue(0), Description("主框架上的自定义3按钮显示的图片 "), Category("操作按钮")]
        public int Other3Image
        {
            get { return _other3Image; }
            set { _other3Image = value; }
        }

        #endregion


        private bool _isRepeatClick = true;

        /// <summary>
        /// 是否能执行重复操作
        /// True不能执行重复操作，即第一次操作未执行完，不能执行第二次操作。
        /// </summary>
        [Description("是否能执行重复操作 True不能执行重复操作，即第一次操作未执行完，不能执行第二次操作。"), Category("操作按钮")]
        public bool IsRepeatClick
        {
            get { return _isRepeatClick; }
            set { _isRepeatClick = value; }
        }

        private bool _isRepeatClickStatus;

        /// <summary>
        /// 执行重复操作的状态
        /// True操作中
        /// </summary>       
        [Browsable(false), Description("执行重复操作的状态  True操作中"), Category("操作按钮")]
        public bool IsRepeatClickStatus
        {
            get { return _isRepeatClickStatus; }
        }

        #endregion


        #region 事件

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



        public void ucToolMenu1_RandomClick(object sender, EventArgs e, ButtonType type)
        {
            if (_isRepeatClick && _isRepeatClickStatus)
            {
                MessageBox.Show(@"系统正在执行，请不要重复操作！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _isRepeatClickStatus = true;

            try
            {
                if (AddClick != null && type == ButtonType.Add)
                    AddClick(sender, e); //添加

                if (EditClick != null && type == ButtonType.Edit)
                    EditClick(sender, e);  //编辑

                if (DeleteClick != null && type == ButtonType.Delete)
                    DeleteClick(sender, e); //删除

                if (SaveClick != null && type == ButtonType.Save)
                    SaveClick(sender, e); //保存

                if (ExportClick != null && type == ButtonType.Export)
                    ExportClick(sender, e); //导出

                if (ImportClick != null && type == ButtonType.Import)
                    ImportClick(sender, e); //导入

                if (CheckClick != null && type == ButtonType.Check)
                    CheckClick(sender, e); //筛选

                if (ResetClick != null && type == ButtonType.Reset)
                    ResetClick(sender, e); //重置

                if (PritClick != null && type == ButtonType.Prit)
                    PritClick(sender, e); //打印

                if (Other1Click != null && type == ButtonType.Other1)
                    Other1Click(sender, e); //自定义1

                if (Other2Click != null && type == ButtonType.Other2)
                    Other2Click(sender, e); //自定义2

                if (Other3Click != null && type == ButtonType.Other3)
                    Other3Click(sender, e); //自定义3

                if (type == ButtonType.Help)
                {
                    //frmBottom = new frmBottom();
                    //frmBottom.Init();
                    //ShowCenter(frmBottom, "帮助", DockStyle.Bottom);
                }

                if (RandomClick != null)
                    RandomClick(sender, e, type);//任意
            }
            finally
            {
                _isRepeatClickStatus = false;
            }



        }

        #endregion
    }
}
