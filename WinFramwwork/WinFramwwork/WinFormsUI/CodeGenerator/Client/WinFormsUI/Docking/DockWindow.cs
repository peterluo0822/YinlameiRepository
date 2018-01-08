namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Xml;

    [Designer(typeof(ControlDesigner))]
    public class DockWindow : Form
    {
        private bool allowClose = true;
        private bool allowDock = true;
        private bool allowSave = true;
        private bool allowSplit = true;
        private bool allowUnDock = true;
        private Container components = null;
        private DockPanel controlContainer = new DockPanel();
        private DockContainerType dockType = DockContainerType.None;
        internal DockContainer dragTarget = null;
        private bool hideOnClose = false;
        private bool isLoaded = false;
        private DockContainer lastHost = null;
        private bool layoutFinished = false;
        private bool showCommandInProcess = false;
        private bool showFormAtOnLoad = true;
        private bool wasDocked = false;

        public DockWindow()
        {
            this.InitializeComponent();
            if (!base.Modal)
            {
                base.Opacity = 0.0;
                base.ShowInTaskbar = false;
                this.controlContainer.Form = this;
                this.controlContainer.Resize += new EventHandler(this.controlContainer_Resize);
                this.controlContainer.Activated += new EventHandler(this.controlContainer_Activated);
                this.controlContainer.Deactivate += new EventHandler(this.controlContainer_Deactivate);
            }
        }

        public void Close()
        {
            if (this.hideOnClose)
            {
                this.Hide();
            }
            else
            {
                FormClosingEventArgs e = new FormClosingEventArgs(CloseReason.UserClosing, false);
                base.OnFormClosing(e);
                if (!e.Cancel)
                {
                    this.Visible = false;
                    base.OnFormClosed(new FormClosedEventArgs(CloseReason.UserClosing));
                    base.Close();
                }
            }
        }

        private void controlContainer_Activated(object sender, EventArgs e)
        {
            this.OnActivated(e);
        }

        private void controlContainer_Deactivate(object sender, EventArgs e)
        {
            this.OnDeactivate(e);
        }

        private void controlContainer_Resize(object sender, EventArgs e)
        {
            this.OnResize(e);
        }

        internal void CopyPropToDockForm(DockForm form)
        {
            form.AllowDock = this.allowDock;
            form.FormBorderStyle = base.FormBorderStyle;
            form.Icon = base.Icon;
            form.Text = this.Text;
        }

        internal void CopyToDockForm(DockForm form)
        {
            form.Location = base.Location;
            form.Size = base.Size;
            form.RootContainer.Controls.Add(this.controlContainer);
            form.RootContainer.DockType = this.DockType;
            this.CopyPropToDockForm(form);
        }

        public void CreateContainer()
        {
            if (!base.DesignMode)
            {
                DockManager.RegisterWindow(this);
                this.controlContainer.Dock = DockStyle.Fill;
                int count = base.Controls.Count;
                int num2 = 0;
                if (!base.Controls.Contains(this.controlContainer))
                {
                    this.controlContainer.Dock = DockStyle.None;
                    base.Controls.Add(this.controlContainer);
                    this.controlContainer.Location = Point.Empty;
                    this.controlContainer.Size = base.ClientSize;
                    Size size = Size.Subtract(base.Size, base.ClientSize);
                    if (!this.MinimumSize.IsEmpty)
                    {
                        this.controlContainer.MinFormSize = Size.Subtract(this.MinimumSize, size);
                    }
                    if (!this.MaximumSize.IsEmpty)
                    {
                        this.controlContainer.MaxFormSize = Size.Subtract(this.MaximumSize, size);
                    }
                }
                while (base.Controls.Count > num2)
                {
                    if (base.Controls[0] != this.controlContainer)
                    {
                        Control control = base.Controls[num2];
                        base.Controls.Remove(control);
                        if (control != null)
                        {
                            this.controlContainer.Controls.Add(control);
                        }
                    }
                    else
                    {
                        num2 = 1;
                    }
                }
                this.controlContainer.Dock = DockStyle.Fill;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool Focus()
        {
            if (base.Modal)
            {
                return base.Focus();
            }
            if (this.IsDocked)
            {
                this.controlContainer.SelectTab();
            }
            else
            {
                this.controlContainer.TopLevelControl.Focus();
            }
            return this.Focused;
        }

        public void Hide()
        {
            this.Visible = false;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            this.AutoScaleBaseSize = new Size(6, 14);
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x100, 0xe4);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Name = "DockWindow";
            base.ShowInTaskbar = false;
            this.Text = "DockWindow";
            base.ResumeLayout(false);
        }

        public void InvokeKeyDown(KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        public void InvokeKeyUp(KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        internal void InvokeVisibleChanged(EventArgs eventArgs)
        {
            this.OnVisibleChanged(eventArgs);
        }

        private void LoadDockForm()
        {
            DockForm form = new DockForm();
            this.CopyToDockForm(form);
            if (this.showFormAtOnLoad)
            {
                form.Show();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.layoutFinished = true;
            base.OnLayout(levent);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (base.Modal)
            {
                base.OnLoad(e);
            }
            else
            {
                if (!this.IsDocked & !base.DesignMode)
                {
                    if (this.dockType != DockContainerType.None)
                    {
                        base.Opacity = 0.0;
                        this.CreateContainer();
                        this.LoadDockForm();
                        this.isLoaded = true;
                    }
                    else
                    {
                        base.Opacity = 1.0;
                    }
                }
                base.OnLoad(e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.HostContainer != null)
            {
                this.HostContainer.SetWindowText();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!base.Modal && ((base.Visible && !base.DesignMode) && (this.dockType != DockContainerType.None)))
            {
                base.Visible = false;
            }
        }

        public virtual void ReadXml(XmlReader reader)
        {
        }

        internal void Release()
        {
            DockContainer parent = this.controlContainer.Parent as DockContainer;
            if (parent != null)
            {
                parent.ReleaseWindow(this);
            }
        }

        public void Show()
        {
            if (!this.showCommandInProcess)
            {
                this.showCommandInProcess = true;
                if (!this.isLoaded)
                {
                    this.OnLoad(EventArgs.Empty);
                }
                if (this.showFormAtOnLoad)
                {
                    this.Visible = true;
                }
                this.showCommandInProcess = false;
            }
        }

        public virtual void WriteXml(XmlTextWriter writer)
        {
        }

        [Category("DockDotNET"), Description("允许此窗口关闭。")]
        public bool AllowClose
        {
            get
            {
                return this.allowClose;
            }
            set
            {
                this.allowClose = value;
            }
        }

        [Category("DockDotNET"), Description("允许其他窗口或容器停靠到这一点。")]
        public bool AllowDock
        {
            get
            {
                return this.allowDock;
            }
            set
            {
                this.allowDock = value;
            }
        }

        [Description("允许的框架内保存和恢复窗口的位置。"), Category("DockDotNET")]
        public bool AllowSave
        {
            get
            {
                return this.allowSave;
            }
            set
            {
                this.allowSave = value;
            }
        }

        [Category("DockDotNET"), Description("允许其他窗口拆分控件容器。")]
        public bool AllowSplit
        {
            get
            {
                return this.allowSplit;
            }
            set
            {
                this.allowSplit = value;
            }
        }

        [Category("DockDotNET"), Description("允许取消停靠该窗口。")]
        public bool AllowUnDock
        {
            get
            {
                return this.allowUnDock;
            }
            set
            {
                this.allowUnDock = value;
            }
        }

        [Browsable(false)]
        public DockPanel ControlContainer
        {
            get
            {
                return this.controlContainer;
            }
            set
            {
                this.controlContainer = value;
            }
        }

        [Description("获取或设置停靠元素类型。"), Category("DockDotNET")]
        public DockContainerType DockType
        {
            get
            {
                return this.dockType;
            }
            set
            {
                this.dockType = value;
            }
        }

        [Browsable(false)]
        internal DockContainer DragTarget
        {
            get
            {
                return this.dragTarget;
            }
        }

        public override bool Focused
        {
            get
            {
                if (!this.Visible)
                {
                    return false;
                }
                return ((this.controlContainer.Parent is DockContainer) && ((this.controlContainer.Parent as DockContainer).ActivePanel == this.controlContainer));
            }
        }

        [Category("DockDotNET"), Description("获取或设置标志，防止关闭该窗口。")]
        public bool HideOnClose
        {
            get
            {
                return this.hideOnClose;
            }
            set
            {
                this.hideOnClose = value;
            }
        }

        [Browsable(false)]
        public DockContainer HostContainer
        {
            get
            {
                return (this.controlContainer.Parent as DockContainer);
            }
        }

        [Browsable(false)]
        public bool IsDocked
        {
            get
            {
                DockContainer parent = this.controlContainer.Parent as DockContainer;
                if (parent == null)
                {
                    return false;
                }
                if ((parent.IsRootContainer & (parent.panList.Count == 1)) & (parent.conList.Count == 0))
                {
                    return false;
                }
                return true;
            }
        }

        [Browsable(false)]
        public bool IsLoaded
        {
            get
            {
                return this.isLoaded;
            }
        }

        [Obsolete("This property is obsolete. Use the Visible property instead."), Browsable(false)]
        public bool IsVisible
        {
            get
            {
                return this.Visible;
            }
            set
            {
                this.Visible = value;
            }
        }

        [Browsable(false)]
        internal bool ShowFormAtOnLoad
        {
            get
            {
                return this.showFormAtOnLoad;
            }
            set
            {
                this.showFormAtOnLoad = value;
            }
        }

        [DefaultValue(true), Category("Behavior"), Description("Determines whether the window is visible or hidden.")]
        public bool Visible
        {
            get
            {
                if (base.Modal)
                {
                    return base.Visible;
                }
                bool visible = false;
                if (this.controlContainer.TopLevelControl != null)
                {
                    visible = this.controlContainer.TopLevelControl.Visible;
                }
                return (this.IsDocked | visible);
            }
            set
            {
                bool visible = this.Visible;
                if (base.Modal)
                {
                    base.Visible = value;
                }
                else
                {
                    DockContainer parent = this.controlContainer.Parent as DockContainer;
                    if (value)
                    {
                        if (!this.isLoaded)
                        {
                            this.Show();
                            return;
                        }
                        if (this.lastHost != null)
                        {
                            if (!this.lastHost.IsDisposed)
                            {
                                this.lastHost.DockWindow(this, DockStyle.Fill);
                                return;
                            }
                            this.lastHost = null;
                        }
                        if (!((base.DesignMode || !this.layoutFinished) || this.Visible))
                        {
                            this.CreateContainer();
                            this.LoadDockForm();
                        }
                    }
                    else
                    {
                        if ((parent != null) && parent.AutoHide)
                        {
                            parent.StopAutoHide();
                        }
                        this.wasDocked = this.IsDocked;
                        this.lastHost = this.wasDocked ? parent : null;
                        this.Release();
                        if (this.controlContainer.TopLevelControl != null)
                        {
                            this.controlContainer.TopLevelControl.Hide();
                        }
                    }
                    if (this.Visible != visible)
                    {
                        this.OnVisibleChanged(EventArgs.Empty);
                    }
                }
            }
        }

        [Browsable(false)]
        public bool WasDocked
        {
            get
            {
                return this.wasDocked;
            }
        }
    }
}

