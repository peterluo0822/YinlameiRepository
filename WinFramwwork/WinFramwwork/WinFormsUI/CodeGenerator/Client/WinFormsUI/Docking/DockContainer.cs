namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    public class DockContainer : Panel
    {
        private DockPanel activePanel;
        private bool autoHide;
        private Timer autoHideTimer;
        private bool blockFocusEvents;
        private const int bottomDock = 0x1a;
        private FlatButton btnAutoHide;
        private FlatButton btnClose;
        private FlatButton btnMenu;
        private FlatButton btnTabL;
        private FlatButton btnTabR;
        private IContainer components;
        internal ArrayList conList;
        private ContextMenuStrip contextMenuStrip;
        internal bool disableOnControlAdded;
        internal bool disableOnControlRemove;
        private int dockBorder;
        protected int dockOffsetB;
        protected int dockOffsetL;
        protected int dockOffsetR;
        protected int dockOffsetT;
        private DockContainerType dockType;
        private DockContainer dragDummy;
        private Panel dragObject;
        private DockPanel dragPanel;
        internal DockEventHandler dragWindowHandler;
        internal Bitmap fadeBkImage;
        internal Bitmap fadeImage;
        private bool fadeIn;
        private Point fadeLocation;
        private Size fadeSize;
        private const int fadeSpeed = 50;
        internal bool fading;
        private AutoHideStorage hideStorage;
        internal bool isActive;
        internal bool isDragContainer;
        private ToolStripItem miClose;
        private ToolStripItem miSep;
        private ToolStripItem miUndock;
        private Size oldSize;
        private int panelOffset;
        private bool panelOverflow;
        internal ArrayList panList;
        private Point ptStart;
        internal bool removeable;
        private bool resizing;
        private bool showIcons;
        private int splitterWidth;
        private ToolTip toolTip;
        private int topDock;

        public DockContainer()
        {
            this.ptStart = Point.Empty;
            this.oldSize = Size.Empty;
            this.dockType = DockContainerType.None;
            this.activePanel = null;
            this.dragPanel = new DockPanel();
            this.dragDummy = null;
            this.dragObject = null;
            this.isActive = false;
            this.isDragContainer = false;
            this.disableOnControlAdded = false;
            this.disableOnControlRemove = false;
            this.removeable = true;
            this.autoHide = false;
            this.fadeIn = false;
            this.fading = false;
            this.fadeImage = null;
            this.fadeBkImage = null;
            this.panelOverflow = false;
            this.showIcons = true;
            this.resizing = false;
            this.blockFocusEvents = false;
            this.panelOffset = 0;
            this.splitterWidth = 4;
            this.dockBorder = 20;
            this.conList = new ArrayList();
            this.panList = new ArrayList();
            this.btnClose = new FlatButton();
            this.btnTabL = new FlatButton();
            this.btnTabR = new FlatButton();
            this.btnAutoHide = new FlatButton();
            this.btnMenu = new FlatButton();
            this.dockOffsetL = 0;
            this.dockOffsetT = 0;
            this.dockOffsetR = 0;
            this.dockOffsetB = 0;
            this.topDock = 0x10;
            this.InitializeComponent();
            this.Init();
        }

        public DockContainer(IContainer container)
        {
            this.ptStart = Point.Empty;
            this.oldSize = Size.Empty;
            this.dockType = DockContainerType.None;
            this.activePanel = null;
            this.dragPanel = new DockPanel();
            this.dragDummy = null;
            this.dragObject = null;
            this.isActive = false;
            this.isDragContainer = false;
            this.disableOnControlAdded = false;
            this.disableOnControlRemove = false;
            this.removeable = true;
            this.autoHide = false;
            this.fadeIn = false;
            this.fading = false;
            this.fadeImage = null;
            this.fadeBkImage = null;
            this.panelOverflow = false;
            this.showIcons = true;
            this.resizing = false;
            this.blockFocusEvents = false;
            this.panelOffset = 0;
            this.splitterWidth = 4;
            this.dockBorder = 20;
            this.conList = new ArrayList();
            this.panList = new ArrayList();
            this.btnClose = new FlatButton();
            this.btnTabL = new FlatButton();
            this.btnTabR = new FlatButton();
            this.btnAutoHide = new FlatButton();
            this.btnMenu = new FlatButton();
            this.dockOffsetL = 0;
            this.dockOffsetT = 0;
            this.dockOffsetR = 0;
            this.dockOffsetB = 0;
            this.topDock = 0x10;
            container.Add(this);
            this.InitializeComponent();
            this.Init();
        }

        internal void AddPanel(DockPanel src, DockContainer dst)
        {
            if (!((src == null) | (dst == null)))
            {
                try
                {
                    this.blockFocusEvents = true;
                    if (dst == this)
                    {
                        base.Controls.Add(src);
                    }
                    else
                    {
                        dst.Controls.Add(src);
                        base.Controls.Add(dst);
                    }
                    this.blockFocusEvents = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("DockContainer.AddPanel: " + exception.Message);
                }
            }
        }

        internal void AddWindowContent(DockContainer src, DockContainer dst)
        {
            if (!((src == null) | (dst == null)))
            {
                try
                {
                    this.blockFocusEvents = true;
                    if (dst == this)
                    {
                        ArrayList list = new ArrayList();
                        src.GetPanels(list);
                        base.Controls.AddRange((DockPanel[]) list.ToArray(typeof(DockPanel)));
                        this.AdjustBorders();
                    }
                    else
                    {
                        src.Location = dst.Location;
                        src.Size = dst.Size;
                        src.Dock = dst.Dock;
                        base.Controls.Add(src);
                    }
                    this.blockFocusEvents = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("DockContainer.AddWindowContent: " + exception.Message);
                }
            }
        }

        internal void AdjustBorders()
        {
            int topDock = 0;
            int num2 = 0;
            int num3 = 0;
            if (this.HideContainer)
            {
                base.DockPadding.All = 0;
                this.dragPanel.Hide();
                base.Invalidate();
            }
            else
            {
                if (this.IsPanelContainer | !this.removeable)
                {
                    if (this.dockType == DockContainerType.ToolWindow)
                    {
                        if (this.IsRootContainer)
                        {
                            topDock = 1;
                        }
                        else
                        {
                            topDock = this.topDock;
                        }
                        if ((this.panList.Count > 1) & !this.autoHide)
                        {
                            num2 = 0x1a;
                        }
                        else
                        {
                            num2 = 1;
                        }
                        num3 = 1;
                    }
                    else if (this.dockType == DockContainerType.Document)
                    {
                        topDock = 0x17;
                        num2 = 2;
                        num3 = 2;
                    }
                }
                base.DockPadding.Top = topDock + this.dockOffsetT;
                base.DockPadding.Bottom = num2 + this.dockOffsetB;
                base.DockPadding.Right = num3 + this.dockOffsetR;
                base.DockPadding.Left = num3 + this.dockOffsetL;
                switch (this.Dock)
                {
                    case DockStyle.Top:
                    {
                        ScrollableControl.DockPaddingEdges dockPadding = base.DockPadding;
                        dockPadding.Bottom += this.splitterWidth;
                        this.dragPanel.Height = this.splitterWidth;
                        this.dragPanel.Width = base.Width;
                        this.dragPanel.Location = new Point(0, base.Height - this.splitterWidth);
                        this.dragPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                        this.dragPanel.Cursor = Cursors.HSplit;
                        this.dragPanel.Show();
                        break;
                    }
                    case DockStyle.Bottom:
                    {
                        ScrollableControl.DockPaddingEdges edges4 = base.DockPadding;
                        edges4.Top += this.splitterWidth;
                        this.dragPanel.Height = this.splitterWidth;
                        this.dragPanel.Width = base.Width;
                        this.dragPanel.Location = new Point(0, 0);
                        this.dragPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        this.dragPanel.Cursor = Cursors.HSplit;
                        this.dragPanel.Show();
                        break;
                    }
                    case DockStyle.Left:
                    {
                        ScrollableControl.DockPaddingEdges edges1 = base.DockPadding;
                        edges1.Right += this.splitterWidth;
                        this.dragPanel.Height = base.Height;
                        this.dragPanel.Width = this.splitterWidth;
                        this.dragPanel.Location = new Point(base.Width - this.splitterWidth, 0);
                        this.dragPanel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.dragPanel.Cursor = Cursors.VSplit;
                        this.dragPanel.Show();
                        break;
                    }
                    case DockStyle.Right:
                    {
                        ScrollableControl.DockPaddingEdges edges2 = base.DockPadding;
                        edges2.Left += this.splitterWidth;
                        this.dragPanel.Height = base.Height;
                        this.dragPanel.Width = this.splitterWidth;
                        this.dragPanel.Location = new Point(0, 0);
                        this.dragPanel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.dragPanel.Cursor = Cursors.VSplit;
                        this.dragPanel.Show();
                        break;
                    }
                    case DockStyle.Fill:
                        this.dragPanel.Hide();
                        break;

                    default:
                        this.dragPanel.Hide();
                        break;
                }
                if (this.dockType == DockContainerType.Document)
                {
                    if (DockManager.Style == DockVisualStyle.VS2003)
                    {
                        this.btnTabR.Size = this.btnTabL.Size = this.btnClose.Size = new Size(14, 15);
                        this.btnTabR.ForeColor = this.btnTabL.ForeColor = this.btnClose.ForeColor = Color.FromArgb(0x55, 0x55, 0x55);
                        this.btnTabR.BackColor = this.btnTabL.BackColor = this.btnClose.BackColor = Color.FromArgb(0xf7, 0xf3, 0xe9);
                        this.btnTabR.LightColor = this.btnTabL.LightColor = this.btnClose.LightColor = SystemColors.Control;
                        this.btnClose.Location = new Point((base.Width - 0x12) - this.dockOffsetR, (base.DockPadding.Top - 0x1a) + 5);
                        this.btnTabR.Location = new Point(((base.Width - 0x12) - 14) - this.dockOffsetR, (base.DockPadding.Top - 0x1a) + 5);
                        this.btnTabL.Location = new Point(((base.Width - 0x12) - 0x1c) - this.dockOffsetR, (base.DockPadding.Top - 0x1a) + 5);
                        this.btnMenu.Hide();
                    }
                    else if (DockManager.Style == DockVisualStyle.VS2005)
                    {
                        this.btnMenu.Size = this.btnClose.Size = new Size(14, 13);
                        this.btnMenu.ForeColor = this.btnClose.ForeColor = SystemColors.ControlText;
                        this.btnMenu.BackColor = this.btnClose.BackColor = SystemColors.Control;
                        this.btnMenu.LightColor = this.btnClose.LightColor = Color.FromArgb(10, 0x24, 0x6a);
                        this.btnMenu.ShadowColor = this.btnClose.ShadowColor = Color.FromArgb(10, 0x24, 0x6a);
                        this.btnMenu.SelectColor = this.btnClose.SelectColor = Color.FromArgb(50, 10, 0x24, 0x6a);
                        this.btnClose.Location = new Point((base.Width - 0x12) - this.dockOffsetR, (base.DockPadding.Top - 0x1a) + 7);
                        this.btnTabR.Hide();
                        this.btnTabL.Hide();
                        this.btnMenu.Location = new Point(((base.Width - 0x12) - 14) - this.dockOffsetR, (base.DockPadding.Top - 0x1a) + 7);
                    }
                    this.btnAutoHide.Hide();
                }
                else if (this.dockType == DockContainerType.ToolWindow)
                {
                    this.btnAutoHide.Size = this.btnMenu.Size = new Size(14, 13);
                    this.btnAutoHide.ForeColor = this.btnMenu.ForeColor = this.btnClose.ForeColor = SystemColors.ControlText;
                    this.btnAutoHide.BackColor = this.btnMenu.BackColor = this.btnClose.BackColor = SystemColors.Control;
                    this.btnAutoHide.LightColor = this.btnMenu.LightColor = this.btnClose.LightColor = Color.White;
                    this.btnAutoHide.ShadowColor = this.btnMenu.ShadowColor = this.btnClose.ShadowColor = Color.White;
                    if (DockManager.Style == DockVisualStyle.VS2003)
                    {
                        this.btnMenu.Hide();
                        this.btnAutoHide.Hide();
                        this.btnClose.Size = new Size(0x10, 13);
                        this.btnClose.Location = new Point((base.Width - base.DockPadding.Right) - 0x12, (base.DockPadding.Top - this.topDock) + 5);
                    }
                    else if (DockManager.Style == DockVisualStyle.VS2005)
                    {
                        this.btnMenu.Location = new Point((((base.Width - base.DockPadding.Right) - 15) - 15) - 15, (base.DockPadding.Top - this.topDock) + 2);
                        this.btnAutoHide.Location = new Point(((base.Width - base.DockPadding.Right) - 15) - 15, (base.DockPadding.Top - this.topDock) + 2);
                        this.btnClose.Size = new Size(14, 13);
                        this.btnClose.Location = new Point((base.Width - base.DockPadding.Right) - 15, (base.DockPadding.Top - this.topDock) + 2);
                    }
                }
                base.Invalidate();
                foreach (DockContainer container in this.conList)
                {
                    container.AdjustBorders();
                }
            }
        }

        private void autoHideTimer_Tick(object sender, EventArgs e)
        {
            if (!this.fadeIn)
            {
                switch (this.hideStorage.toplevelDock)
                {
                    case DockStyle.Top:
                        if ((this.fadeLocation.Y - 50) > -base.Height)
                        {
                            this.fadeLocation.Y -= 50;
                            break;
                        }
                        this.StopFading(true);
                        return;

                    case DockStyle.Bottom:
                        if ((this.fadeLocation.Y + 50) < base.Height)
                        {
                            this.fadeLocation.Y += 50;
                            break;
                        }
                        this.StopFading(true);
                        return;

                    case DockStyle.Left:
                        if ((this.fadeLocation.X - 50) > -base.Width)
                        {
                            this.fadeLocation.X -= 50;
                            break;
                        }
                        this.StopFading(true);
                        return;

                    case DockStyle.Right:
                        if ((this.fadeLocation.X + 50) < base.Width)
                        {
                            this.fadeLocation.X += 50;
                            break;
                        }
                        this.StopFading(true);
                        return;
                }
            }
            else
            {
                switch (this.hideStorage.toplevelDock)
                {
                    case DockStyle.Top:
                        if ((this.fadeLocation.Y + 50) < 0)
                        {
                            this.fadeLocation.Y += 50;
                            break;
                        }
                        this.StopFading(false);
                        return;

                    case DockStyle.Bottom:
                        if ((this.fadeLocation.Y - 50) > 0)
                        {
                            this.fadeLocation.Y -= 50;
                            break;
                        }
                        this.StopFading(false);
                        return;

                    case DockStyle.Left:
                        if ((this.fadeLocation.X + 50) < 0)
                        {
                            this.fadeLocation.X += 50;
                            break;
                        }
                        this.StopFading(false);
                        return;

                    case DockStyle.Right:
                        if ((this.fadeLocation.X - 50) > 0)
                        {
                            this.fadeLocation.X -= 50;
                            break;
                        }
                        this.StopFading(false);
                        return;
                }
            }
            base.Invalidate();
        }

        private void btnAutoHide_Click(object sender, EventArgs e)
        {
            this.AutoHide = !this.autoHide;
        }

        private void btnAutoHide_PostPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(this.btnMenu.ForeColor);
            int num = (this.btnMenu.Width >> 1) - 1;
            int num2 = this.btnMenu.Height >> 1;
            graphics.TranslateTransform((float) num, (float) num2);
            if (this.autoHide)
            {
                graphics.RotateTransform(90f);
            }
            graphics.DrawLine(pen, -3, 1, 3, 1);
            graphics.DrawLine(pen, -2, -4, 2, -4);
            graphics.DrawLine(pen, 0, 2, 0, 4);
            graphics.DrawLine(pen, -2, -3, -2, 0);
            graphics.DrawLine(pen, 1, -3, 1, 0);
            graphics.DrawLine(pen, 2, -3, 2, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.conList.Count > 0)
            {
                this.disableOnControlRemove = true;
                while (this.conList.Count > 0)
                {
                    DockContainer item = this.conList[0] as DockContainer;
                    item.disableOnControlRemove = true;
                    item.activePanel = null;
                    item.btnClose_Click(sender, e);
                    if (this.conList.Contains(item))
                    {
                        this.conList.Remove(item);
                    }
                }
                this.disableOnControlRemove = false;
            }
            else if ((this.panList.Count > 1) & (this.activePanel != null))
            {
                if (!this.activePanel.Form.AllowClose)
                {
                    return;
                }
                this.activePanel.Form.Close();
            }
            else
            {
                ArrayList list = new ArrayList();
                list.AddRange(this.panList);
                foreach (DockPanel panel2 in list)
                {
                    panel2.Form.Close();
                }
                this.panList.Clear();
            }
            if (this.IsEmpty & this.removeable)
            {
                if (this.autoHide)
                {
                    this.hideStorage.manager.AutoHideContainer(this, DockStyle.Fill, false);
                }
                else if (base.Parent is DockContainer)
                {
                    (base.Parent as DockContainer).RemoveContainer(this);
                }
            }
        }

        private void btnClose_PostPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(this.btnClose.ForeColor);
            int num = 0;
            int num2 = 0;
            if (this.btnClose.Pressed)
            {
                num = 1;
                num2 = 1;
            }
            if (DockManager.Style == DockVisualStyle.VS2003)
            {
                if (this.dockType == DockContainerType.Document)
                {
                    graphics.DrawLine(pen, (int) (num + 3), (int) (num2 + 3), (int) (num + 9), (int) (num2 + 9));
                    graphics.DrawLine(pen, (int) (num + 4), (int) (num2 + 3), (int) (num + 10), (int) (num2 + 9));
                    graphics.DrawLine(pen, (int) (num + 3), (int) (num2 + 9), (int) (num + 9), (int) (num2 + 3));
                    graphics.DrawLine(pen, (int) (num + 4), (int) (num2 + 9), (int) (num + 10), (int) (num2 + 3));
                }
                else
                {
                    graphics.DrawLine(pen, (int) (num + 6), (int) (num2 + 3), (int) (num + 12), (int) (num2 + 9));
                    graphics.DrawLine(pen, (int) (num + 5), (int) (num2 + 3), (int) (num + 11), (int) (num2 + 9));
                    graphics.DrawLine(pen, (int) (num + 12), (int) (num2 + 3), (int) (num + 6), (int) (num2 + 9));
                    graphics.DrawLine(pen, (int) (num + 11), (int) (num2 + 3), (int) (num + 5), (int) (num2 + 9));
                }
            }
            else
            {
                num = this.btnMenu.Width >> 1;
                num2 = this.btnMenu.Height >> 1;
                graphics.DrawLine(pen, (int) (num - 3), (int) (num2 + 3), (int) (num + 3), (int) (num2 - 3));
                graphics.DrawLine(pen, (int) (num - 4), (int) (num2 + 3), (int) (num + 2), (int) (num2 - 3));
                graphics.DrawLine(pen, (int) (num - 3), (int) (num2 - 3), (int) (num + 3), (int) (num2 + 3));
                graphics.DrawLine(pen, (int) (num - 4), (int) (num2 - 3), (int) (num + 2), (int) (num2 + 3));
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.dragObject = this;
            this.contextMenuStrip.Show(Control.MousePosition);
        }

        private void btnMenu_PostPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(this.btnMenu.ForeColor);
            int num = this.btnMenu.Width >> 1;
            int num2 = this.btnMenu.Height >> 1;
            GraphicsPath path = new GraphicsPath();
            path.AddLine((int) (num - 3), (int) (num2 + 1), (int) (num + 2), (int) (num2 + 1));
            path.AddLine((float) (num + 2), (float) (num2 + 1), num - 0.5f, (float) (num2 + 3));
            path.AddLine(num - 0.5f, (float) (num2 + 3), (float) (num - 3), (float) (num2 + 1));
            graphics.DrawPath(pen, path);
            if (this.btnMenu.Enabled)
            {
                graphics.FillPath(new SolidBrush(pen.Color), path);
            }
        }

        private void btnTabL_Click(object sender, EventArgs e)
        {
            this.panelOffset--;
            if (this.panelOffset == 0)
            {
                this.btnTabL.Enabled = false;
            }
            base.Invalidate();
        }

        private void btnTabL_PostPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(this.btnTabL.ForeColor);
            int num = 0;
            int num2 = 0;
            if (this.btnTabL.Pressed)
            {
                num = 1;
                num2 = 1;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddLine((int) (num + 4), (int) (num2 + 6), (int) (num + 8), (int) (num2 + 2));
            path.AddLine((int) (num + 8), (int) (num2 + 2), (int) (num + 8), (int) (num2 + 10));
            path.AddLine((int) (num + 8), (int) (num2 + 10), (int) (num + 4), (int) (num2 + 6));
            graphics.DrawPath(pen, path);
            if (this.btnTabL.Enabled)
            {
                graphics.FillPath(new SolidBrush(pen.Color), path);
            }
        }

        private void btnTabR_Click(object sender, EventArgs e)
        {
            this.panelOffset++;
            if (this.panelOffset > 0)
            {
                this.btnTabL.Enabled = true;
            }
            if (this.panelOffset == (this.panList.Count - 1))
            {
                this.btnTabR.Enabled = false;
            }
            base.Invalidate();
        }

        private void btnTabR_PostPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(this.btnTabR.ForeColor);
            int num = 0;
            int num2 = 0;
            if (this.btnTabR.Pressed)
            {
                num = 1;
                num2 = 1;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddLine((int) (num + 4), (int) (num2 + 2), (int) (num + 4), (int) (num2 + 10));
            path.AddLine((int) (num + 4), (int) (num2 + 10), (int) (num + 8), (int) (num2 + 6));
            path.AddLine((int) (num + 8), (int) (num2 + 6), (int) (num + 4), (int) (num2 + 2));
            graphics.DrawPath(pen, path);
            if (this.btnTabR.Enabled)
            {
                graphics.FillPath(new SolidBrush(pen.Color), path);
            }
        }

        public void CloseClick(object sender, EventArgs e)
        {
            this.btnClose_Click(sender, e);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.activePanel == null)
                {
                    e.Cancel = true;
                }
                else
                {
                    bool allowClose = this.activePanel.Form.AllowClose;
                    bool allowUnDock = this.activePanel.Form.AllowUnDock;
                    if (!allowClose & !allowUnDock)
                    {
                        e.Cancel = true;
                    }
                    this.miUndock.Visible = allowUnDock;
                    this.miClose.Visible = allowClose;
                    this.miSep.Visible = allowClose & allowUnDock;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.contextMenuStrip_Opening : " + exception.Message);
                e.Cancel = true;
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

        private void DockContainer_DoubleClick(object sender, EventArgs e)
        {
            if ((((DockContainer) sender).ActivePanel.Form.Tag == null) || (((DockContainer) sender).ActivePanel.Form.Tag.ToString() != "首页"))
            {
                this.btnClose_Click(sender, e);
            }
        }

        public bool DockWindow(CodeGenerator.Client.WinFormsUI.Docking.DockWindow wnd, DockStyle style)
        {
            try
            {
                Point empty = Point.Empty;
                if (this.conList.Count > 0)
                {
                    return (this.conList[0] as DockContainer).DockWindow(wnd, style);
                }
                if (!wnd.Visible)
                {
                    wnd.ShowFormAtOnLoad = false;
                    wnd.Show();
                    wnd.ShowFormAtOnLoad = true;
                }
                if (wnd.HostContainer != null)
                {
                    wnd.Release();
                }
                if (style == DockStyle.None)
                {
                    return false;
                }
                empty = this.GetVirtualDragDest(style);
                DockManager.NoGuidePlease = true;
                this.DragWindow(wnd, new DockEventArgs(empty, wnd.DockType, true));
                DockManager.NoGuidePlease = false;
                wnd.InvokeVisibleChanged(EventArgs.Empty);
                return wnd.IsDocked;
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.DockWindow: " + exception.Message);
            }
            return false;
        }

        private void dragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.ptStart = new Point(e.X + this.dragPanel.Left, e.Y + this.dragPanel.Top);
            this.dragDummy = new DockContainer();
            this.dragDummy.isDragContainer = true;
            if (this.Dock == DockStyle.Left)
            {
                this.dragDummy.Location = new Point(((base.Location.X + this.ptStart.X) - 2) + this.TopLevelContainer.dockOffsetL, base.Location.Y);
                this.dragDummy.Size = new Size(4, base.Height);
            }
            else if (this.Dock == DockStyle.Right)
            {
                this.dragDummy.Location = new Point(((base.Location.X + this.ptStart.X) - 2) - this.TopLevelContainer.dockOffsetR, base.Location.Y);
                this.dragDummy.Size = new Size(4, base.Height);
            }
            else if (this.Dock == DockStyle.Bottom)
            {
                this.dragDummy.Location = new Point(base.Location.X, ((base.Location.Y + this.ptStart.Y) - 2) - this.TopLevelContainer.dockOffsetB);
                this.dragDummy.Size = new Size(base.Width, 4);
            }
            else
            {
                this.dragDummy.Location = new Point(base.Location.X, ((base.Location.Y + this.ptStart.Y) - 2) + this.TopLevelContainer.dockOffsetT);
                this.dragDummy.Size = new Size(base.Width, 4);
            }
            base.Parent.Controls.Add(this.dragDummy);
            this.dragDummy.BringToFront();
            this.resizing = true;
        }

        private void dragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.resizing)
            {
                this.ptStart = new Point(e.X + this.dragPanel.Left, e.Y + this.dragPanel.Top);
                DockContainer parent = base.Parent as DockContainer;
                int width = parent.Width;
                int height = parent.Height;
                if (this.Dock == DockStyle.Left)
                {
                    if (this.ptStart.X < (this.dockBorder * 3))
                    {
                        this.ptStart.X = this.dockBorder * 3;
                    }
                    else if (this.ptStart.X > (width - (this.dockBorder * 3)))
                    {
                        this.ptStart.X = width - (this.dockBorder * 3);
                    }
                    this.dragDummy.Location = new Point((this.ptStart.X - 2) + this.TopLevelContainer.dockOffsetL, 0);
                }
                else if (this.Dock == DockStyle.Right)
                {
                    if (this.ptStart.X > (base.Width - (this.dockBorder * 3)))
                    {
                        this.ptStart.X = base.Width - (this.dockBorder * 3);
                    }
                    else if (this.ptStart.X < ((base.Width - width) + (this.dockBorder * 3)))
                    {
                        this.ptStart.X = (base.Width - width) + (this.dockBorder * 3);
                    }
                    this.dragDummy.Location = new Point((((width - base.Width) + this.ptStart.X) - 2) - this.TopLevelContainer.dockOffsetR, 0);
                }
                else if (this.Dock == DockStyle.Top)
                {
                    if (this.ptStart.Y < (this.dockBorder * 3))
                    {
                        this.ptStart.Y = this.dockBorder * 3;
                    }
                    else if (this.ptStart.Y > (height - (this.dockBorder * 3)))
                    {
                        this.ptStart.Y = height - (this.dockBorder * 3);
                    }
                    this.dragDummy.Location = new Point(0, (this.ptStart.Y - 2) + this.TopLevelContainer.dockOffsetT);
                }
                else if (this.Dock == DockStyle.Bottom)
                {
                    if (this.ptStart.Y > (base.Height - (this.dockBorder * 3)))
                    {
                        this.ptStart.Y = base.Height - (this.dockBorder * 3);
                    }
                    else if (this.ptStart.Y < ((base.Height - height) + (this.dockBorder * 3)))
                    {
                        this.ptStart.Y = (base.Height - height) + (this.dockBorder * 3);
                    }
                    this.dragDummy.Location = new Point(0, (((height - base.Height) + this.ptStart.Y) - 2) - this.TopLevelContainer.dockOffsetB);
                }
                this.dragDummy.BringToFront();
                base.Invalidate();
            }
        }

        private void dragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.resizing)
            {
                this.resizing = false;
                base.Parent.Controls.Remove(this.dragDummy);
                base.Parent.Invalidate();
                this.dragDummy.Dispose();
                this.dragDummy = null;
                this.ptStart = new Point(e.X + this.dragPanel.Left, e.Y + this.dragPanel.Top);
                if (this.Dock == DockStyle.Left)
                {
                    if (this.ptStart.X < (this.dockBorder * 3))
                    {
                        this.ptStart.X = this.dockBorder * 3;
                    }
                    else if (this.ptStart.X > (base.Parent.Width - (this.dockBorder * 3)))
                    {
                        this.ptStart.X = base.Parent.Width - (this.dockBorder * 3);
                    }
                }
                else if (this.Dock == DockStyle.Right)
                {
                    if (this.ptStart.X > (base.Width - (this.dockBorder * 3)))
                    {
                        this.ptStart.X = base.Width - (this.dockBorder * 3);
                    }
                    else if (this.ptStart.X < ((base.Width - base.Parent.Width) + (this.dockBorder * 3)))
                    {
                        this.ptStart.X = (base.Width - base.Parent.Width) + (this.dockBorder * 3);
                    }
                }
                else if (this.Dock == DockStyle.Top)
                {
                    if (this.ptStart.Y < (this.dockBorder * 3))
                    {
                        this.ptStart.Y = this.dockBorder * 3;
                    }
                    else if (this.ptStart.Y > (base.Parent.Height - (this.dockBorder * 3)))
                    {
                        this.ptStart.Y = base.Parent.Height - (this.dockBorder * 3);
                    }
                }
                else if (this.Dock == DockStyle.Bottom)
                {
                    if (this.ptStart.Y > (base.Height - (this.dockBorder * 3)))
                    {
                        this.ptStart.Y = base.Height - (this.dockBorder * 3);
                    }
                    else if (this.ptStart.Y < ((base.Height - base.Parent.Height) + (this.dockBorder * 3)))
                    {
                        this.ptStart.Y = (base.Height - base.Parent.Height) + (this.dockBorder * 3);
                    }
                }
                switch (this.Dock)
                {
                    case DockStyle.Top:
                        if (this.ptStart.Y >= 60)
                        {
                            base.Height = this.ptStart.Y;
                            break;
                        }
                        base.Height = 60;
                        break;

                    case DockStyle.Bottom:
                    {
                        int y = 0;
                        if ((base.Height - this.ptStart.Y) >= 60)
                        {
                            y = this.ptStart.Y;
                        }
                        else
                        {
                            y = base.Height - 60;
                        }
                        base.Location = new Point(base.Location.X, base.Location.Y + y);
                        base.Height -= y;
                        return;
                    }
                    case DockStyle.Left:
                        if (this.ptStart.X >= 40)
                        {
                            base.Width = this.ptStart.X;
                            break;
                        }
                        base.Width = 40;
                        break;

                    case DockStyle.Right:
                    {
                        int x = 0;
                        if ((base.Width - this.ptStart.X) >= 40)
                        {
                            x = this.ptStart.X;
                        }
                        else
                        {
                            x = base.Width - 40;
                        }
                        base.Location = new Point(base.Location.X + x, base.Location.Y);
                        base.Width -= x;
                        return;
                    }
                }
            }
        }

        private void dragPanel_Paint(object sender, PaintEventArgs e)
        {
            if (DockManager.Style == DockVisualStyle.VS2005)
            {
                Graphics graphics = e.Graphics;
                DockContainer topLevelContainer = this.TopLevelContainer;
                Rectangle rect = topLevelContainer.RectangleToScreen(topLevelContainer.ClientRectangle);
                Rectangle rectangle2 = this.dragPanel.RectangleToScreen(this.dragPanel.ClientRectangle);
                rect.X -= rectangle2.X;
                rect.Y -= rectangle2.Y;
                LinearGradientBrush brush = new LinearGradientBrush(rect, SystemColors.Control, Color.White, LinearGradientMode.Horizontal);
                graphics.FillRectangle(brush, this.dragPanel.ClientRectangle);
            }
        }

        public bool DragWindow(object sender, DockEventArgs e)
        {
            if (!e.Handled)
            {
                try
                {
                    if ((((e.DockType == DockContainerType.None) || ((e.DockType == DockContainerType.Document) && (this.dockType == DockContainerType.ToolWindow))) || (((this.conList.Count > 0) && !e.IgnoreHierarchy) || ((sender == base.TopLevelControl) || this.autoHide))) || (!base.TopLevelControl.Visible && (sender is DockForm)))
                    {
                        return false;
                    }
                    if ((base.TopLevelControl is DockForm) && (((base.TopLevelControl as DockForm).Moving | !(base.TopLevelControl as DockForm).AllowDock) | !(base.TopLevelControl as DockForm).Visible))
                    {
                        return false;
                    }
                    if (!this.HitTest(e.Point))
                    {
                        return false;
                    }
                    DockForm formAtPoint = DockManager.GetFormAtPoint(e.Point, 1);
                    if ((formAtPoint != base.TopLevelControl) & (formAtPoint != null))
                    {
                        return false;
                    }
                    DockManager.UpdateDockGuide(this, e);
                    if (e.Handled)
                    {
                        return false;
                    }
                    Size empty = Size.Empty;
                    if (sender is DockForm)
                    {
                        empty = (sender as DockForm).Size;
                    }
                    else if (sender is DockContainer)
                    {
                        empty = (sender as DockContainer).Size;
                    }
                    else if (sender is CodeGenerator.Client.WinFormsUI.Docking.DockWindow)
                    {
                        empty = (sender as CodeGenerator.Client.WinFormsUI.Docking.DockWindow).Size;
                    }
                    e.Target = this.GetTarget(empty, e.DockType, e.Point);
                    if (e.Target == null)
                    {
                        return false;
                    }
                    e.Handled = true;
                    if (e.Release)
                    {
                        if (!this.conList.Contains(e.Target) & (e.Target != this))
                        {
                            DockContainer container = new DockContainer {
                                removeable = this.removeable,
                                DockBorder = this.dockBorder
                            };
                            this.removeable = true;
                            DockPanel activePanel = this.ActivePanel;
                            container.Controls.AddRange((DockPanel[]) this.panList.ToArray(typeof(DockPanel)));
                            container.ActivePanel = activePanel;
                            container.DockType = this.dockType;
                            if (this.conList.Count > 1)
                            {
                                this.disableOnControlRemove = true;
                                container.Controls.AddRange((DockContainer[]) this.conList.ToArray(typeof(DockContainer)));
                                this.disableOnControlRemove = false;
                            }
                            base.Controls.Add(container);
                            container.Dock = DockStyle.Fill;
                        }
                        if (sender is CodeGenerator.Client.WinFormsUI.Docking.DockWindow)
                        {
                            this.AddPanel((sender as CodeGenerator.Client.WinFormsUI.Docking.DockWindow).ControlContainer, e.Target);
                        }
                        else
                        {
                            DockContainer src = null;
                            if (sender is DockForm)
                            {
                                src = (sender as DockForm).RootContainer;
                            }
                            else if (sender is DockContainer)
                            {
                                src = sender as DockContainer;
                            }
                            this.AddWindowContent(src, e.Target);
                        }
                        base.TopLevelControl.BringToFront();
                        base.TopLevelControl.Invalidate(true);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("DockContainer.DragWindow: " + exception.Message);
                }
            }
            return true;
        }

        private void DrawDocWndVS2003(Graphics g, Pen penBlack, Pen penWhite, ref float n, ref int y, ref int h, ref int y0, StringFormat sf)
        {
            y0 = base.DockPadding.Top - 0x1a;
            this.BackColor = SystemColors.Control;
            g.DrawRectangle(SystemPens.ControlDark, this.dockOffsetL, y0 + this.dockOffsetT, ((base.Width - 1) - this.dockOffsetL) - this.dockOffsetR, (((base.Height - 1) - y0) - this.dockOffsetT) - this.dockOffsetB);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0xf7, 0xf3, 0xe9)), 1, y0 + 1, ((base.Width - 2) - this.dockOffsetL) - this.dockOffsetR, 0x17);
            g.DrawLine(penWhite, (int) (1 + this.dockOffsetL), (int) ((((int) y0) + 0x1a) - 3), (int) ((base.Width - 2) - this.dockOffsetR), (int) ((((int) y0) + 0x1a) - 3));
            n = (base.DockPadding.Left - 1) + 4;
            y = 3;
            h = -20;
            int num = (base.Width - 0x12) - 0x1c;
            this.panelOverflow = false;
            foreach (DockPanel panel in this.panList)
            {
                if (this.panList.IndexOf(panel) < this.panelOffset)
                {
                    panel.TabRect = RectangleF.Empty;
                }
                else
                {
                    Brush brush;
                    Font font;
                    if ((panel == this.activePanel) && base.ContainsFocus)
                    {
                        font = new Font(this.Font, FontStyle.Bold);
                    }
                    else
                    {
                        font = this.Font;
                    }
                    SizeF ef = this.MeasurePanel(panel, g, false, font);
                    panel.TabRect = new RectangleF(n, (float) (y0 + 3), ef.Width, 22f);
                    if ((n + ((int) ef.Width)) >= (num - 2))
                    {
                        this.panelOverflow = true;
                        ef.Width = (num - n) - 2f;
                    }
                    if (panel == this.activePanel)
                    {
                        g.FillRectangle(SystemBrushes.Control, panel.TabRect);
                        g.DrawLine(penBlack, n + ef.Width, (float) (y0 + y), n + ef.Width, (float) ((y0 + y) - h));
                        g.DrawLine(penWhite, n + 1f, (float) (y0 + y), n + ef.Width, (float) (y0 + y));
                        g.DrawLine(penWhite, n, (float) (y0 + y), n, (float) ((y0 + y) - h));
                        brush = new SolidBrush(Color.Black);
                    }
                    else
                    {
                        int num2 = this.panList.IndexOf(panel) + 1;
                        if (num2 != this.panList.IndexOf(this.activePanel))
                        {
                            g.DrawLine(new Pen(Color.FromArgb(0x80, 0x80, 0x80)), (n + ef.Width) - 1f, (float) ((y0 + y) + 2), (n + ef.Width) - 1f, (float) (((y0 + y) - h) - 2));
                        }
                        brush = new SolidBrush(Color.FromArgb(0x55, 0x55, 0x55));
                    }
                    RectangleF layoutRectangle = new RectangleF(n + 3f, ((float) ((y0 + y) - 1)) + ((24f - ef.Height) / 2f), ef.Width - 6f, ef.Height);
                    if (this.showIcons && (panel.Form.Icon != null))
                    {
                        g.DrawIcon(panel.Form.Icon, new Rectangle((int) layoutRectangle.X, (y0 - 1) + (y + 4), 0x10, 0x10));
                        layoutRectangle.Offset(ef.Height + 3f, 0f);
                        layoutRectangle.Width -= ef.Height + 3f;
                    }
                    g.DrawString(panel.Form.Text, font, brush, layoutRectangle, sf);
                    n += (int) ef.Width;
                    if (this.panelOverflow)
                    {
                        break;
                    }
                }
            }
            if (this.panelOverflow)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0xf7, 0xf3, 0xe9)), num - 2, y0 + 1, (base.Width - 2) - num, 0x17);
                if (this.panelOffset < (this.panList.Count - 1))
                {
                    this.btnTabR.Enabled = true;
                }
                else
                {
                    this.btnTabR.Enabled = false;
                }
            }
            else
            {
                this.btnTabR.Enabled = false;
            }
        }

        private void DrawDocWndVS2005(Graphics g, Pen penBlack, Pen penWhite, ref float n, ref int y, ref int h, ref int y0, StringFormat sf)
        {
            y0 = base.DockPadding.Top - 0x1a;
            this.BackColor = SystemColors.Control;
            g.Clear(SystemColors.Control);
            sf.Alignment = StringAlignment.Center;
            this.showIcons = false;
            g.DrawRectangle(SystemPens.ControlDark, this.dockOffsetL, this.dockOffsetT, ((base.Width - 1) - this.dockOffsetL) - this.dockOffsetR, ((base.Height - 1) - this.dockOffsetT) - this.dockOffsetB);
            g.DrawLine(SystemPens.ControlDark, (int)(1 + this.dockOffsetL), (int)((((int)y0) + 0x1a) - 4), (int)((base.Width - 2) - this.dockOffsetR), (int)((((int)y0) + 0x1a) - 4));
            n = (base.DockPadding.Left - 1) + 3;
            y = 6;
            h = -17;
            int x = (base.Width - 8) - 0x2a;
            this.panelOverflow = false;
            for (int i = 0; i < this.panList.Count; i++)
            {
                DockPanel panel = this.panList[i] as DockPanel;
                if (i < this.panelOffset)
                {
                    panel.TabRect = RectangleF.Empty;
                }
                else
                {
                    Font font = new Font(this.Font, FontStyle.Bold);
                    SizeF ef = this.MeasurePanel(panel, g, false, font);
                    if ((n + ((int)ef.Width)) >= (x - 2))
                    {
                        this.panelOverflow = true;
                        ef.Width = (x - n) - 2f;
                    }
                    GraphicsPath path = new GraphicsPath();
                    path.AddLine((n + ef.Width) + 6f, (float)((y0 + y) + 2), (n + ef.Width) + 6f, (float)(((y0 + y) - h) - 1));
                    path.AddLine((n + ef.Width) + 6f, (float)((y0 + y) + 2), (n + ef.Width) + 4f, (float)(y0 + y));
                    path.AddLine((n + ef.Width) + 4f, (float)(y0 + y), n + 18f, (float)(y0 + y));
                    path.AddLine(n + 18f, (float)(y0 + y), n + 13f, (float)((y0 + y) + 3));
                    path.AddLine(n, (float)((y0 + 0x1a) - 4), n + 13f, (float)((y0 + y) + 3));
                    GraphicsPath path2 = path.Clone() as GraphicsPath;
                    path2.AddLine(n, (float)((y0 + 0x1a) - 4), (n + ef.Width) + 6f, (float)((y0 + 0x1a) - 4));
                    panel.TabRect = path2.GetBounds();
                    if (panel == this.activePanel)
                    {
                        g.FillPath(new SolidBrush(Color.White), path2);
                    }
                    else
                    {
                        g.FillPath(SystemBrushes.Control, path2);
                    }
                    g.DrawPath(SystemPens.ControlDark, path);
                    if ((panel == this.activePanel) && base.ContainsFocus)
                    {
                        font = new Font(this.Font, FontStyle.Bold);
                    }
                    else
                    {
                        font = new Font(this.Font, FontStyle.Regular);
                    }
                    Brush brush = new SolidBrush(Color.Black);
                    RectangleF layoutRectangle = new RectangleF(n + 12f, ((float)((y0 + y) - 3)) + ((24f - ef.Height) / 2f), ef.Width - 10f, ef.Height);
                    g.DrawString(panel.Form.Text, font, brush, layoutRectangle, sf);
                    n += (int)ef.Width;
                    if (this.panelOverflow)
                    {
                        break;
                    }
                }
            }
            if (this.panelOverflow)
            {
                g.FillRectangle(SystemBrushes.Control, x, y0 + 4, (base.Width - 2) - x, 0x12);
                if (this.panelOffset < (this.panList.Count - 1))
                {
                    this.btnTabR.Enabled = true;
                }
                else
                {
                    this.btnTabR.Enabled = false;
                }
            }
            else
            {
                this.btnTabR.Enabled = false;
            }
            sf.Alignment = StringAlignment.Near;
        }

        private void DrawToolWndVS2003(Graphics g, Pen penBlack, Pen penWhite, ref float n, ref int y, ref int h, ref int y0, StringFormat sf)
        {
            Brush activeCaptionText;
            int num = ((base.Width - base.DockPadding.Left) - base.DockPadding.Right) + 2;
            float height = 0f;
            y0 = base.DockPadding.Top - this.topDock;
            this.BackColor = SystemColors.Control;
            if (!this.IsRootContainer)
            {
                if (base.ContainsFocus)
                {
                    g.FillRectangle(SystemBrushes.ActiveCaption, base.DockPadding.Left - 1, ((int) y0) + 3, (base.DockPadding.Left - 1) + num, (int) (y0 + h));
                    activeCaptionText = SystemBrushes.ActiveCaptionText;
                    this.btnClose.BackColor = SystemColors.ActiveCaption;
                    this.btnClose.ForeColor = SystemColors.ActiveCaptionText;
                    this.btnClose.Invalidate();
                }
                else
                {
                    g.FillRectangle(SystemBrushes.Control, base.DockPadding.Left - 1, ((int) y0) + 3, (base.DockPadding.Left - 1) + num, (int) (y0 + h));
                    g.DrawLine(SystemPens.ControlDark, base.DockPadding.Left, y0 + 3, ((base.DockPadding.Left - 1) + num) - 2, y0 + 3);
                    g.DrawLine(SystemPens.ControlDark, (int) (base.DockPadding.Left - 1), (int) (((int) y0) + 4), (int) (base.DockPadding.Left - 1), (int) (((int) (y0 + h)) + 1));
                    g.DrawLine(SystemPens.ControlDark, base.DockPadding.Left, (y0 + h) + 2, ((base.DockPadding.Left - 1) + num) - 2, (y0 + h) + 2);
                    g.DrawLine(SystemPens.ControlDark, (int) (((base.DockPadding.Left - 1) + num) - 1), (int) (((int) y0) + 4), (int) (((base.DockPadding.Left - 1) + num) - 1), (int) (((int) (y0 + h)) + 1));
                    activeCaptionText = SystemBrushes.ControlText;
                    this.btnClose.BackColor = SystemColors.Control;
                    this.btnClose.ForeColor = SystemColors.ControlText;
                    this.btnClose.Invalidate();
                }
                if (this.activePanel != null)
                {
                    height = g.MeasureString(this.activePanel.Form.Text, this.Font).Height;
                    g.DrawString(this.activePanel.Form.Text, this.Font, activeCaptionText, (float) ((base.DockPadding.Left - 1) + 2), (((float) y0) + ((this.topDock - height) / 2f)) + 1f, sf);
                }
            }
            g.DrawRectangle(SystemPens.ControlDark, (int) (base.DockPadding.Left - 1), (int) (((int) y0) + 0x15), (int) (num - 1), (int) (((base.Height - base.DockPadding.Top) - base.DockPadding.Bottom) + 1));
            if (this.panList.Count > 1)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0xf7, 0xf3, 0xe9)), (base.DockPadding.Left - 1) + 1, (base.Height - base.DockPadding.Bottom) + 3, num - 2, 0x17);
                g.DrawLine(penBlack, (int) ((base.DockPadding.Left - 1) + 1), (int) ((base.Height - base.DockPadding.Bottom) + 3), (int) (num - 2), (int) ((base.Height - base.DockPadding.Bottom) + 3));
                n = (base.DockPadding.Left - 1) + 4;
                y = ((base.Height - base.DockPadding.Bottom) - 2) + 0x1a;
                h = 0x15;
                foreach (DockPanel panel in this.panList)
                {
                    SizeF ef = this.MeasurePanel(panel, g, true, this.Font);
                    panel.TabRect = new RectangleF(n, (float) ((base.Height - base.DockPadding.Bottom) + 2), ef.Width, 22f);
                    if (panel == this.activePanel)
                    {
                        g.FillRectangle(SystemBrushes.Control, panel.TabRect);
                        g.DrawLine(penBlack, n + ef.Width, (float) y, n + ef.Width, (float) (y - h));
                        g.DrawLine(penBlack, n + 1f, (float) y, n + ef.Width, (float) y);
                        g.DrawLine(penWhite, n, (float) y, n, (float) (y - h));
                        activeCaptionText = new SolidBrush(Color.Black);
                    }
                    else
                    {
                        int num3 = this.panList.IndexOf(panel) + 1;
                        if (num3 != this.panList.IndexOf(this.activePanel))
                        {
                            g.DrawLine(new Pen(Color.FromArgb(0x80, 0x80, 0x80)), (n + ef.Width) - 1f, (float) (y - 2), (n + ef.Width) - 1f, (float) ((y - h) + 3));
                        }
                        activeCaptionText = new SolidBrush(Color.FromArgb(0x55, 0x55, 0x55));
                    }
                    RectangleF layoutRectangle = new RectangleF(n + 3f, ((float) (y + 3)) + ((-24f - ef.Height) / 2f), ef.Width - 6f, ef.Height);
                    if (this.showIcons && (panel.Form.Icon != null))
                    {
                        g.DrawIcon(panel.Form.Icon, new Rectangle(((int) layoutRectangle.X) + 1, (y + 2) + -20, 0x10, 0x10));
                        layoutRectangle.Offset(ef.Height + 3f, 0f);
                        layoutRectangle.Width -= ef.Height + 3f;
                    }
                    g.DrawString(panel.Form.Text, this.Font, activeCaptionText, layoutRectangle, sf);
                    n += (int) ef.Width;
                }
            }
        }

        private void DrawToolWndVS2005(Graphics g, Pen penBlack, Pen penWhite, ref float n, ref int y, ref int h, ref int y0, StringFormat sf)
        {
            Brush activeCaptionText;
            int num = ((base.Width - base.DockPadding.Left) - base.DockPadding.Right) + 2;
            float height = 0f;
            y0 = base.DockPadding.Top - this.topDock;
            this.BackColor = SystemColors.Control;
            g.Clear(Color.White);
            if (!this.IsRootContainer)
            {
                if (base.ContainsFocus)
                {
                    g.FillRectangle(SystemBrushes.ActiveCaption, base.DockPadding.Left - 1, y0, (base.DockPadding.Left - 1) + num, y0 + h);
                    activeCaptionText = SystemBrushes.ActiveCaptionText;
                    this.btnAutoHide.BackColor = this.btnMenu.BackColor = this.btnClose.BackColor = SystemColors.ActiveCaption;
                    this.btnAutoHide.ForeColor = this.btnMenu.ForeColor = this.btnClose.ForeColor = SystemColors.ActiveCaptionText;
                }
                else
                {
                    g.FillRectangle(SystemBrushes.ControlDark, base.DockPadding.Left - 1, y0, (base.DockPadding.Left - 1) + num, y0 + h);
                    activeCaptionText = SystemBrushes.Control;
                    this.btnAutoHide.BackColor = this.btnMenu.BackColor = this.btnClose.BackColor = SystemColors.ControlDark;
                    this.btnAutoHide.ForeColor = this.btnMenu.ForeColor = this.btnClose.ForeColor = SystemColors.Control;
                }
                this.btnClose.Invalidate();
                this.btnMenu.Invalidate();
                this.btnAutoHide.Invalidate();
                if (this.activePanel != null)
                {
                    height = g.MeasureString(this.activePanel.Form.Text, this.Font).Height;
                    g.DrawString(this.activePanel.Form.Text, this.Font, activeCaptionText, (float) ((base.DockPadding.Left - 1) + 2), ((float) y0) + ((this.topDock - height) / 2f), sf);
                }
            }
            g.DrawRectangle(SystemPens.ControlDark, (int) (base.DockPadding.Left - 1), (int) ((((int) y0) + this.topDock) - 1), (int) (num - 1), (int) (((base.Height - base.DockPadding.Top) - base.DockPadding.Bottom) + 1));
            if ((this.panList.Count > 1) & !this.autoHide)
            {
                DockContainer topLevelContainer = this.TopLevelContainer;
                Rectangle rect = topLevelContainer.RectangleToScreen(topLevelContainer.ClientRectangle);
                Rectangle rectangle2 = base.RectangleToScreen(base.ClientRectangle);
                rect.X -= rectangle2.X;
                rect.Y -= rectangle2.Y;
                Rectangle rectangle3 = new Rectangle(base.DockPadding.Left - 1, (base.Height - base.DockPadding.Bottom) + 3, num + 1, 0x17);
                LinearGradientBrush brush = new LinearGradientBrush(rect, SystemColors.Control, Color.White, LinearGradientMode.Horizontal);
                g.FillRectangle(brush, rectangle3);
                g.DrawLine(SystemPens.ControlDark, (int) (base.DockPadding.Left - 1), (int) ((base.Height - base.DockPadding.Bottom) + 3), (int) (num - 1), (int) ((base.Height - base.DockPadding.Bottom) + 3));
                n = (base.DockPadding.Left - 1) + 4;
                y = ((base.Height - base.DockPadding.Bottom) - 2) + 0x1a;
                h = 0x15;
                foreach (DockPanel panel in this.panList)
                {
                    SizeF ef = this.MeasurePanel(panel, g, true, this.Font);
                    panel.TabRect = new Rectangle(((int) n) + 1, (base.Height - base.DockPadding.Bottom) + 2, ((int) ef.Width) - 2, 0x16);
                    int num3 = ((int) (n + ef.Width)) - 1;
                    if (panel == this.activePanel)
                    {
                        g.FillRectangle(new SolidBrush(Color.White), panel.TabRect);
                        g.DrawLine(SystemPens.ControlDark, num3, y - 2, num3, y - h);
                        g.DrawLine(SystemPens.ControlDark, n + 2f, (float) y, (float) (num3 - 2), (float) y);
                        g.DrawLine(SystemPens.ControlDark, n, (float) (y - 2), n, (float) (y - h));
                        g.DrawLine(SystemPens.ControlDark, n, (float) (y - 2), n + 2f, (float) y);
                        g.DrawLine(SystemPens.ControlDark, num3, y - 2, num3 - 2, y);
                        activeCaptionText = new SolidBrush(Color.Black);
                    }
                    else
                    {
                        int num4 = this.panList.IndexOf(panel) + 1;
                        if ((num4 != this.panList.IndexOf(this.activePanel)) & (num4 != this.panList.Count))
                        {
                            g.DrawLine(SystemPens.ControlDark, (int) (num3 - 2), (int) (((int) y) - 4), (int) (num3 - 2), (int) (((int) (y - h)) + 3));
                        }
                        activeCaptionText = new SolidBrush(Color.FromArgb(0x40, 0x40, 0x40));
                    }
                    RectangleF layoutRectangle = new RectangleF(n + 3f, ((float) (y + 2)) + ((-24f - ef.Height) / 2f), ef.Width - 6f, ef.Height);
                    if (this.showIcons && (panel.Form.Icon != null))
                    {
                        g.DrawIcon(panel.Form.Icon, new Rectangle(((int) layoutRectangle.X) + 1, (y + 2) + -20, 0x10, 0x10));
                        layoutRectangle.Offset(ef.Height + 3f, 0f);
                        layoutRectangle.Width -= ef.Height + 3f;
                    }
                    g.DrawString(panel.Form.Text, this.Font, activeCaptionText, layoutRectangle, sf);
                    n += (int) ef.Width;
                }
            }
        }

        internal void FadeIn()
        {
            this.fadeImage = new Bitmap(base.Width, base.Height);
            base.DrawToBitmap(this.fadeImage, base.ClientRectangle);
            Bitmap bitmap = new Bitmap(this.hideStorage.manager.Width, this.hideStorage.manager.Height);
            this.fadeBkImage = new Bitmap(base.Width, base.Height);
            Rectangle clientRectangle = this.hideStorage.manager.ClientRectangle;
            this.hideStorage.manager.DrawToBitmap(bitmap, clientRectangle);
            Graphics graphics = Graphics.FromImage(this.fadeBkImage);
            graphics.DrawImage(bitmap, 0, 0, new Rectangle(base.Left, base.Top, base.Width, base.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            this.fading = true;
            this.fadeIn = true;
            this.fadeSize = base.Size;
            this.fadeLocation = Point.Empty;
            foreach (Control control in base.Controls)
            {
                control.Hide();
            }
            base.Show();
            base.BringToFront();
            switch (this.hideStorage.toplevelDock)
            {
                case DockStyle.Top:
                    this.fadeLocation.Y = -base.Height;
                    break;

                case DockStyle.Bottom:
                    this.fadeLocation.Y = base.Height;
                    break;

                case DockStyle.Left:
                    this.fadeLocation.X = -base.Width;
                    break;

                case DockStyle.Right:
                    this.fadeLocation.X = base.Width;
                    break;
            }
            base.Invalidate();
            this.autoHideTimer.Start();
        }

        internal void FadeOut()
        {
            this.fadeImage = new Bitmap(base.Width, base.Height);
            base.DrawToBitmap(this.fadeImage, base.ClientRectangle);
            this.fading = true;
            this.fadeIn = false;
            this.fadeSize = base.Size;
            this.fadeLocation = Point.Empty;
            foreach (Control control in base.Controls)
            {
                control.Hide();
            }
            base.Show();
            base.BringToFront();
            base.Invalidate();
            this.autoHideTimer.Start();
        }

        private void FloatWindow()
        {
            Rectangle rectangle = base.RectangleToScreen(base.ClientRectangle);
            DockForm form = new DockForm(this.dragObject);
            form.Show();
            form.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            if (!DockManager.FastMoveDraw)
            {
                form.Opacity = 1.0;
            }
            form.StartMoving(new Point(Control.MousePosition.X + 10, Control.MousePosition.Y + 10));
            this.dragObject = null;
            if ((((this.panList.Count == 0) & (this.dockType == DockContainerType.Document)) & this.removeable) & (base.Parent is DockContainer))
            {
                (base.Parent as DockContainer).Controls.Remove(this);
            }
        }

        public DockContainer GetNextChild(DockContainerType type, DockContainer last)
        {
            DockContainer nextChild = null;
            foreach (DockContainer container2 in this.conList)
            {
                nextChild = container2.GetNextChild(type, last);
                if (nextChild != null)
                {
                    return nextChild;
                }
            }
            if (((this.conList.Count == 0) && (this.dockType == type)) && (this != last))
            {
                nextChild = this;
            }
            return nextChild;
        }

        internal void GetPanels(ArrayList list)
        {
            if (this.IsPanelContainer)
            {
                foreach (DockPanel panel in this.panList)
                {
                    list.Add(panel);
                }
            }
            else
            {
                foreach (DockContainer container in this.conList)
                {
                    container.GetPanels(list);
                }
            }
        }

        internal DockContainer GetTarget(Size srcSize, DockContainerType type, Point pt)
        {
            Rectangle rectangle = base.RectangleToScreen(base.ClientRectangle);
            Rectangle rectangle2 = new Rectangle(rectangle.Left + base.DockPadding.Left, rectangle.Top + base.DockPadding.Top, (rectangle.Width - base.DockPadding.Left) - base.DockPadding.Right, (rectangle.Height - base.DockPadding.Top) - base.DockPadding.Bottom);
            if (rectangle2.Contains(pt))
            {
                DockContainer container = null;
                if ((pt.X - rectangle2.Left) <= this.dockBorder)
                {
                    container = new DockContainer {
                        DockType = type,
                        Dock = DockStyle.Left
                    };
                    if (srcSize.Width > (base.Width / 2))
                    {
                        container.Width = base.Width / 2;
                    }
                    else
                    {
                        container.Width = srcSize.Width;
                    }
                    container.Height = base.Height;
                    container.Location = new Point(rectangle.X, rectangle.Y);
                    return container;
                }
                if ((rectangle2.Right - pt.X) <= this.dockBorder)
                {
                    container = new DockContainer {
                        DockType = type,
                        Dock = DockStyle.Right
                    };
                    if (srcSize.Width > (base.Width / 2))
                    {
                        container.Width = base.Width / 2;
                    }
                    else
                    {
                        container.Width = srcSize.Width;
                    }
                    container.Height = base.Height;
                    container.Location = new Point((rectangle.X + base.Width) - container.Width, rectangle.Y);
                    return container;
                }
                if ((pt.Y - rectangle2.Top) <= this.dockBorder)
                {
                    container = new DockContainer {
                        DockType = type,
                        Dock = DockStyle.Top
                    };
                    if (srcSize.Height > (base.Height / 2))
                    {
                        container.Height = base.Height / 2;
                    }
                    else
                    {
                        container.Height = srcSize.Height;
                    }
                    container.Width = base.Width;
                    container.Location = new Point(rectangle.X, rectangle.Y);
                    return container;
                }
                if ((rectangle2.Bottom - pt.Y) <= this.dockBorder)
                {
                    container = new DockContainer {
                        DockType = type,
                        Dock = DockStyle.Bottom
                    };
                    if (srcSize.Height > (base.Height / 2))
                    {
                        container.Height = base.Height / 2;
                    }
                    else
                    {
                        container.Height = srcSize.Height;
                    }
                    container.Width = base.Width;
                    container.Location = new Point(rectangle.X, (rectangle.Y + base.Height) - container.Height);
                }
                return container;
            }
            if (this.IsRootContainer)
            {
                DockForm parent = base.Parent as DockForm;
                if (parent.Bounds.Contains(pt) & (this.dockType == type))
                {
                    return this;
                }
            }
            if (rectangle.Contains(pt))
            {
                if ((pt.Y <= (rectangle.Top + base.DockPadding.Top)) && (this.dockType == type))
                {
                    return this;
                }
                if ((((this.dockType == type) && (this.dockType == DockContainerType.ToolWindow)) && (this.panList.Count > 1)) && (pt.Y > ((rectangle.Top + base.Height) - base.DockPadding.Bottom)))
                {
                    return this;
                }
            }
            return null;
        }

        internal Point GetVirtualDragDest(DockStyle style)
        {
            Point empty = Point.Empty;
            Rectangle bounds = base.RectangleToScreen(base.ClientRectangle);
            switch (style)
            {
                case DockStyle.Top:
                    return new Point(bounds.Left + (bounds.Width / 2), (bounds.Top + base.DockPadding.Top) + (this.dockBorder / 2));

                case DockStyle.Bottom:
                    return new Point(bounds.Left + (bounds.Width / 2), ((bounds.Top + bounds.Height) - base.DockPadding.Bottom) - (this.dockBorder / 2));

                case DockStyle.Left:
                    return new Point((bounds.Left + base.DockPadding.Left) + (this.dockBorder / 2), bounds.Top + (bounds.Height / 2));

                case DockStyle.Right:
                    return new Point(((bounds.Left + bounds.Width) - base.DockPadding.Right) - (this.dockBorder / 2), bounds.Top + (bounds.Height / 2));

                case DockStyle.Fill:
                    if (this.IsRootContainer)
                    {
                        bounds = base.Parent.Bounds;
                    }
                    return new Point(bounds.Left + (bounds.Width / 2), bounds.Top + (base.DockPadding.Top / 2));
            }
            return empty;
        }

        private void HideButtons()
        {
            this.btnClose.Hide();
            this.btnAutoHide.Hide();
            this.btnMenu.Hide();
            this.btnTabL.Hide();
            this.btnTabR.Hide();
        }

        protected bool HitTest(Point pt)
        {
            try
            {
                Rectangle bounds = base.RectangleToScreen(base.ClientRectangle);
                if (this.IsRootContainer)
                {
                    bounds = base.Parent.Bounds;
                }
                return bounds.Contains(pt);
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.HitTest: " + exception.Message);
                return false;
            }
        }

        private void Init()
        {
            this.dragWindowHandler = new DockEventHandler(this.DragWindow);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            if (DockManager.Style == DockVisualStyle.VS2003)
            {
                this.topDock = 0x16;
                this.btnTabL.Hide();
                this.btnTabL.ShadowColor = Color.Black;
                this.btnTabL.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                this.btnTabL.Enabled = false;
                this.btnTabL.PostPaint += new PaintEventHandler(this.btnTabL_PostPaint);
                this.btnTabL.Click += new EventHandler(this.btnTabL_Click);
                base.Controls.Add(this.btnTabL);
                this.btnTabR.Hide();
                this.btnTabR.ShadowColor = Color.Black;
                this.btnTabR.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                this.btnTabR.Enabled = false;
                this.btnTabR.PostPaint += new PaintEventHandler(this.btnTabR_PostPaint);
                this.btnTabR.Click += new EventHandler(this.btnTabR_Click);
                base.Controls.Add(this.btnTabR);
            }
            else if (DockManager.Style == DockVisualStyle.VS2005)
            {
                this.topDock = 0x10;
                this.btnAutoHide.Hide();
                this.btnAutoHide.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                this.btnAutoHide.PostPaint += new PaintEventHandler(this.btnAutoHide_PostPaint);
                this.btnAutoHide.Click += new EventHandler(this.btnAutoHide_Click);
                base.Controls.Add(this.btnAutoHide);
                this.btnMenu.Hide();
                this.btnMenu.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                this.btnMenu.PostPaint += new PaintEventHandler(this.btnMenu_PostPaint);
                this.btnMenu.Click += new EventHandler(this.btnMenu_Click);
                base.Controls.Add(this.btnMenu);
            }
            this.btnClose.Hide();
            this.btnClose.ShadowColor = Color.Black;
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnClose.PostPaint += new PaintEventHandler(this.btnClose_PostPaint);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.Controls.Add(this.btnClose);
            this.dragPanel.Hide();
            this.dragPanel.MouseDown += new MouseEventHandler(this.dragPanel_MouseDown);
            this.dragPanel.MouseMove += new MouseEventHandler(this.dragPanel_MouseMove);
            this.dragPanel.MouseUp += new MouseEventHandler(this.dragPanel_MouseUp);
            this.dragPanel.Paint += new PaintEventHandler(this.dragPanel_Paint);
            base.Controls.Add(this.dragPanel);
            this.miClose = this.contextMenuStrip.Items.Add("关闭", null, new EventHandler(this.CloseClick));
            this.miSep = this.contextMenuStrip.Items.Add("-");
            this.miUndock = this.contextMenuStrip.Items.Add("取消停靠", null, new EventHandler(this.UndockClick));
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.toolTip = new ToolTip(this.components);
            this.contextMenuStrip = new ContextMenuStrip(this.components);
            this.autoHideTimer = new Timer(this.components);
            base.SuspendLayout();
            this.toolTip.Active = false;
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new Size(0x3d, 4);
            this.contextMenuStrip.Opening += new CancelEventHandler(this.contextMenuStrip_Opening);
            this.autoHideTimer.Interval = 0x19;
            this.autoHideTimer.Tick += new EventHandler(this.autoHideTimer_Tick);
            this.BackColor = Color.Transparent;
            base.DoubleClick += new EventHandler(this.DockContainer_DoubleClick);
            base.ResumeLayout(false);
        }

        public void InvokeKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                foreach (DockContainer container in this.conList)
                {
                    if (container.ContainsFocus)
                    {
                        container.InvokeKeyDown(sender, e);
                        return;
                    }
                }
                if (this.activePanel != null)
                {
                    this.activePanel.Form.InvokeKeyDown(e);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.InvokeKeyDown: " + exception.Message);
            }
        }

        public void InvokeKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                foreach (DockContainer container in this.conList)
                {
                    if (container.ContainsFocus)
                    {
                        container.InvokeKeyUp(sender, e);
                        break;
                    }
                }
                if (this.activePanel != null)
                {
                    this.activePanel.Form.InvokeKeyUp(e);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.InvokeKeyUp: " + exception.Message);
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        private SizeF MeasurePanel(DockPanel panel, Graphics graphics, bool cut, Font font)
        {
            SizeF ef = graphics.MeasureString(panel.Form.Text, font);
            if (font.Bold)
            {
                ef.Width += 12f;
            }
            else
            {
                ef.Width += 6f;
            }
            if (this.showIcons && (panel.Form.Icon != null))
            {
                ef.Width += ef.Height + 3f;
            }
            if ((ef.Width > (((((base.Width - base.DockPadding.Left) - base.DockPadding.Right) + 2) - 8) / this.panList.Count)) && cut)
            {
                ef.Width = ((((base.Width - base.DockPadding.Left) - base.DockPadding.Right) + 2) - 8) / this.panList.Count;
            }
            return ef;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (this.disableOnControlAdded)
            {
                if (e.Control is DockContainer)
                {
                    this.conList.Add(e.Control);
                }
                else if (e.Control is DockPanel)
                {
                    this.panList.Add(e.Control);
                }
            }
            else
            {
                if ((e.Control is DockPanel) && (e.Control != this.dragPanel))
                {
                    this.panList.Add(e.Control);
                    this.ShowButtons();
                    this.ActivePanel = e.Control as DockPanel;
                }
                else if (e.Control is FlatButton)
                {
                    if (this.panList.Count > 0)
                    {
                        this.ShowButtons();
                    }
                    else
                    {
                        this.HideButtons();
                    }
                }
                else if (e.Control is DockContainer)
                {
                    DockContainer control = e.Control as DockContainer;
                    if (!control.isDragContainer)
                    {
                        this.conList.Add(e.Control);
                    }
                    if (control.DockType == DockContainerType.Document)
                    {
                        if (control.panList.Count > 0)
                        {
                            control.ShowButtons();
                        }
                        else
                        {
                            control.HideButtons();
                        }
                    }
                }
                this.AdjustBorders();
                base.OnControlAdded(e);
                base.Invalidate();
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (this.disableOnControlRemove)
            {
                if (e.Control is DockContainer)
                {
                    this.conList.Remove(e.Control);
                }
                else if (e.Control is DockPanel)
                {
                    this.panList.Remove(e.Control);
                }
            }
            else
            {
                if ((e.Control is DockPanel) && (e.Control != this.dragPanel))
                {
                    this.panList.Remove(e.Control);
                    if (this.panList.Count == 0)
                    {
                        this.HideButtons();
                    }
                    else
                    {
                        this.ShowButtons();
                    }
                }
                else if (e.Control is DockContainer)
                {
                    if ((e.Control as DockContainer).isDragContainer)
                    {
                        return;
                    }
                    this.conList.Remove(e.Control);
                    if (this.conList.Count == 1)
                    {
                        DockContainer container = this.conList[0] as DockContainer;
                        container.disableOnControlRemove = true;
                        base.Controls.Remove(container);
                        this.DockType = container.DockType;
                        this.removeable = container.removeable;
                        base.Controls.AddRange((DockContainer[]) container.conList.ToArray(typeof(DockContainer)));
                        base.Controls.AddRange((DockPanel[]) container.panList.ToArray(typeof(DockPanel)));
                        container.Dispose();
                        container = null;
                    }
                }
                this.AdjustBorders();
                if (this.panList.Count > 0)
                {
                    this.ActivePanel = (DockPanel) this.panList[this.panList.Count - 1];
                }
                base.OnControlRemoved(e);
                base.Invalidate();
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (!((this.activePanel == null) || this.blockFocusEvents))
            {
                this.activePanel.SetFocus(true);
            }
            base.Invalidate();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (!((this.activePanel == null) || this.blockFocusEvents))
            {
                this.activePanel.SetFocus(false);
            }
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.Focus();
            ArrayList list = new ArrayList();
            this.GetPanels(list);
            if (!this.IsRootContainer & (list.Count > 0))
            {
                this.dragObject = this;
            }
            else
            {
                this.dragObject = null;
            }
            this.ptStart = new Point(e.X, e.Y);
            int count = this.panList.Count;
            for (int i = 0; i < count; i++)
            {
                DockPanel panel = this.panList[i] as DockPanel;
                if (panel.TabRect.Contains((float) e.X, (float) e.Y))
                {
                    this.ActivePanel = panel;
                    if (panel.Form.AllowUnDock)
                    {
                        this.dragObject = panel;
                    }
                    else
                    {
                        this.dragObject = null;
                    }
                    if ((e.Button == MouseButtons.Middle) & (this.DockType == DockContainerType.Document))
                    {
                        this.CloseClick(this, null);
                    }
                    return;
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.ContextMenuStrip = null;
            base.OnMouseLeave(e);
            if (this.autoHide)
            {
                this.hideStorage.manager.OnMouseLeave(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.autoHide && this.AllowUnDock)
            {
                if (((((this.panList.Count > 1) & (e.Y > (base.Height - base.DockPadding.Bottom))) & (this.DockType != DockContainerType.Document)) | ((e.Y < 0x1a) & (this.DockType == DockContainerType.Document))) && base.ClientRectangle.Contains(e.X, e.Y))
                {
                    this.UpdateToolTip(new Point(e.X, e.Y));
                }
                else if (((e.Button == MouseButtons.Left) & (this.dragObject != null)) & !this.ptStart.Equals(new Point(e.X, e.Y)))
                {
                    if (this.autoHide)
                    {
                        this.hideStorage.manager.AutoHideContainer(this, DockStyle.Fill, false);
                    }
                    this.FloatWindow();
                }
                base.OnMouseMove(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            HatchBrush brush = new HatchBrush(HatchStyle.Percent50, Color.Black, Color.Transparent);
            Pen penBlack = new Pen(Color.Black);
            Pen penWhite = new Pen(Color.White);
            float n = 0f;
            int y = 0;
            int h = 0x10;
            int num4 = 0;
            StringFormat genericDefault = StringFormat.GenericDefault;
            genericDefault.Trimming = StringTrimming.EllipsisCharacter;
            if (this.fading)
            {
                if (this.fadeBkImage != null)
                {
                    g.DrawImage(this.fadeBkImage, Point.Empty);
                }
                g.DrawImage(this.fadeImage, this.fadeLocation.X, this.fadeLocation.Y);
            }
            else if (this.isDragContainer)
            {
                g.FillRectangle(brush, base.ClientRectangle);
            }
            else if (!this.HideContainer)
            {
                if (this.panList.Count == 0)
                {
                    Rectangle rect = new Rectangle(this.dockOffsetL, this.dockOffsetT, (base.Width - this.dockOffsetL) - this.dockOffsetR, (base.Height - this.dockOffsetT) - this.dockOffsetB);
                    g.FillRectangle(SystemBrushes.ControlDark, rect);
                }
                else if (this.dockType == DockContainerType.ToolWindow)
                {
                    if (DockManager.Style == DockVisualStyle.VS2003)
                    {
                        this.DrawToolWndVS2003(g, penBlack, penWhite, ref n, ref y, ref h, ref num4, genericDefault);
                    }
                    else if (DockManager.Style == DockVisualStyle.VS2005)
                    {
                        this.DrawToolWndVS2005(g, penBlack, penWhite, ref n, ref y, ref h, ref num4, genericDefault);
                    }
                }
                else if (this.dockType == DockContainerType.Document)
                {
                    if (DockManager.Style == DockVisualStyle.VS2003)
                    {
                        this.DrawDocWndVS2003(g, penBlack, penWhite, ref n, ref y, ref h, ref num4, genericDefault);
                    }
                    else if (DockManager.Style == DockVisualStyle.VS2005)
                    {
                        this.DrawDocWndVS2005(g, penBlack, penWhite, ref n, ref y, ref h, ref num4, genericDefault);
                    }
                }
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            this.AdjustBorders();
            if (base.Parent != null)
            {
                DockManager.RegisterContainer(this);
            }
            else
            {
                DockManager.UnRegisterContainer(this);
            }
            base.OnParentChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if ((base.Width != 0) && (base.Height != 0))
            {
                foreach (DockContainer container in this.conList)
                {
                    if (container.Dock != DockStyle.Fill)
                    {
                        if ((this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right))
                        {
                            container.Height += (base.Height - this.oldSize.Height) / 2;
                        }
                        else if ((this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom))
                        {
                            container.Width += (base.Width - this.oldSize.Width) / 2;
                        }
                    }
                }
                this.oldSize = base.Size;
                base.OnResize(e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Invalidate();
        }

        internal virtual void ReadXml(XmlReader reader, bool dockManagerHead)
        {
            int num;
            reader.Read();
            if (this is DockManager)
            {
                goto Label_015A;
            }
            string attribute = reader.GetAttribute("dock");
            if (attribute != null)
            {
                if (!(attribute == "Fill"))
                {
                    if (attribute == "Top")
                    {
                        this.Dock = DockStyle.Top;
                        goto Label_00B2;
                    }
                    if (attribute == "Bottom")
                    {
                        this.Dock = DockStyle.Bottom;
                        goto Label_00B2;
                    }
                    if (attribute == "Left")
                    {
                        this.Dock = DockStyle.Left;
                        goto Label_00B2;
                    }
                    if (attribute == "Right")
                    {
                        this.Dock = DockStyle.Right;
                        goto Label_00B2;
                    }
                }
                else
                {
                    this.Dock = DockStyle.Fill;
                    goto Label_00B2;
                }
            }
            this.disableOnControlAdded = false;
            return;
        Label_00B2:
            if (this.Dock != DockStyle.Fill)
            {
                dockManagerHead = false;
            }
            string s = reader.GetAttribute("width");
            if (s != null)
            {
                base.Width = int.Parse(s);
            }
            s = reader.GetAttribute("height");
            if (s != null)
            {
                base.Height = int.Parse(s);
            }
            attribute = reader.GetAttribute("type");
            if (attribute != null)
            {
                if (!(attribute == "Document"))
                {
                    if (attribute == "ToolWindow")
                    {
                        this.DockType = DockContainerType.ToolWindow;
                        goto Label_015A;
                    }
                }
                else
                {
                    this.DockType = DockContainerType.Document;
                    goto Label_015A;
                }
            }
            this.disableOnControlAdded = false;
            return;
        Label_015A:
            num = 0;
            s = reader.GetAttribute("active");
            if (s != null)
            {
                int.TryParse(s, out num);
            }
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name == "container")
                    {
                        DockContainer container = new DockContainer();
                        container.ReadXml(reader.ReadSubtree(), dockManagerHead);
                        if (container.conList.Count == 1)
                        {
                            DockStyle dock = container.Dock;
                            container = container.conList[0] as DockContainer;
                            if (this.conList.Count == 0)
                            {
                                container.Dock = DockStyle.Fill;
                            }
                            else
                            {
                                container.Dock = dock;
                            }
                            container.AdjustBorders();
                        }
                        else if (!container.IsEmpty || (container.DockType == DockContainerType.Document))
                        {
                            if (container.conList.Count == 2)
                            {
                                DockContainer container2 = container.conList[0] as DockContainer;
                                DockContainer container3 = container.conList[1] as DockContainer;
                                if ((((container2.DockType == DockContainerType.Document) && container2.IsEmpty) && (container3.DockType == DockContainerType.Document)) && container3.IsEmpty)
                                {
                                    container2.removeable = true;
                                    container3.removeable = true;
                                    container.Controls.Clear();
                                    container.removeable = !dockManagerHead || (container.Dock != DockStyle.Fill);
                                    container.AdjustBorders();
                                }
                                else if (((container2.DockType == DockContainerType.Document) && container2.IsEmpty) && container2.removeable)
                                {
                                    container2.removeable = true;
                                    container3.removeable = true;
                                    container = container3;
                                    container.Dock = DockStyle.Fill;
                                    container.AdjustBorders();
                                }
                                else if (((container3.DockType == DockContainerType.Document) && container3.IsEmpty) && container3.removeable)
                                {
                                    container2.removeable = true;
                                    container3.removeable = true;
                                    container = container2;
                                    container.Dock = DockStyle.Fill;
                                    container.AdjustBorders();
                                }
                                else if (((container2.DockType == DockContainerType.Document) && container2.IsPanelContainer) && (container2.Dock == DockStyle.Fill))
                                {
                                    container2.removeable = ((container3.DockType == DockContainerType.Document) || !dockManagerHead) || (container.Dock != DockStyle.Fill);
                                }
                                else if (((container3.DockType == DockContainerType.Document) && container3.IsPanelContainer) && (container3.Dock == DockStyle.Fill))
                                {
                                    container3.removeable = ((container2.DockType == DockContainerType.Document) || !dockManagerHead) || (container.Dock != DockStyle.Fill);
                                }
                            }
                            else if (container.IsEmpty)
                            {
                                container.removeable = !dockManagerHead || (container.Dock != DockStyle.Fill);
                                container.AdjustBorders();
                            }
                        }
                        if (!(container.removeable && container.IsEmpty))
                        {
                            base.Controls.Add(container);
                            base.DockPadding.All = 0;
                        }
                    }
                    else if (reader.Name == "panel")
                    {
                        DockPanel panel = new DockPanel();
                        if (panel.ReadXml(reader))
                        {
                            base.Controls.Add(panel);
                            panel.Form.InvokeVisibleChanged(EventArgs.Empty);
                        }
                    }
                }
            }
            this.SelectTab(num);
            this.disableOnControlAdded = false;
        }

        internal bool ReleaseWindow(CodeGenerator.Client.WinFormsUI.Docking.DockWindow form)
        {
            bool flag = false;
            if (base.Controls.Contains(form.ControlContainer))
            {
                base.Controls.Remove(form.ControlContainer);
                flag = true;
            }
            if (this.IsEmpty & this.removeable)
            {
                if (base.Parent is DockContainer)
                {
                    (base.Parent as DockContainer).RemoveContainer(this);
                    return flag;
                }
                if (base.Parent is DockForm)
                {
                    (base.Parent as DockForm).Close();
                }
            }
            return flag;
        }

        protected void RemoveContainer(DockContainer cont)
        {
            if (base.Controls.Contains(cont))
            {
                base.Controls.Remove(cont);
                cont.Dispose();
                cont = null;
            }
        }

        public void SelectTab(DockPanel p)
        {
            try
            {
                if (((this.panList != null) && (p != null)) && this.panList.Contains(p))
                {
                    this.ActivePanel = p;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.SelectTab: " + exception.Message);
            }
        }

        public void SelectTab(int i)
        {
            try
            {
                if ((this.panList.Count > i) && (this.panList[i] != null))
                {
                    this.ActivePanel = this.panList[i] as DockPanel;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.SelectTab: " + exception.Message);
            }
        }

        internal void SetFormSizeBounds(DockForm form)
        {
            Size empty = Size.Empty;
            Size size2 = Size.Empty;
            foreach (DockPanel panel in this.panList)
            {
                if (!panel.MinFormSize.IsEmpty)
                {
                    if (panel.MinFormSize.Width > empty.Width)
                    {
                        empty.Width = panel.MinFormSize.Width;
                    }
                    if (panel.MinFormSize.Height > empty.Height)
                    {
                        empty.Height = panel.MinFormSize.Height;
                    }
                }
                if (!panel.MaxFormSize.IsEmpty)
                {
                    if (panel.MaxFormSize.Width < size2.Width)
                    {
                        size2.Width = panel.MaxFormSize.Width;
                    }
                    if (panel.MaxFormSize.Height < size2.Height)
                    {
                        size2.Height = panel.MaxFormSize.Height;
                    }
                }
            }
            try
            {
                Size size3 = new Size(base.Padding.Left + base.Padding.Right, base.Padding.Top + base.Padding.Bottom);
                if (empty.IsEmpty)
                {
                    form.MinimumSize = Size.Empty;
                }
                else
                {
                    form.MinimumSize = Size.Add(empty, size3);
                }
                if (size2.IsEmpty)
                {
                    form.MaximumSize = Size.Empty;
                }
                else
                {
                    form.MaximumSize = Size.Add(size2, size3);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.SetFormSizeBounds : " + exception.Message);
            }
        }

        internal void SetWindowText()
        {
            if ((base.TopLevelControl != null) && (this.activePanel != null))
            {
                CodeGenerator.Client.WinFormsUI.Docking.DockWindow window = this.activePanel.Form;
                if (base.TopLevelControl is DockForm)
                {
                    DockForm topLevelControl = base.TopLevelControl as DockForm;
                    window.CopyPropToDockForm(topLevelControl);
                    this.SetFormSizeBounds(topLevelControl);
                }
                else
                {
                    base.Invalidate();
                }
                if (this.autoHide)
                {
                    this.hideStorage.manager.Invalidate();
                }
            }
        }

        private void ShowButtons()
        {
            if (((this.activePanel != null) && (this.activePanel.Form != null)) && this.activePanel.Form.AllowClose)
            {
                this.btnClose.Show();
            }
            else
            {
                this.btnClose.Hide();
            }
            if (DockManager.Style == DockVisualStyle.VS2005)
            {
                this.btnMenu.Show();
                if (this.dockType == DockContainerType.ToolWindow)
                {
                    this.btnAutoHide.Show();
                }
                else
                {
                    this.btnAutoHide.Hide();
                }
            }
            if ((this.dockType == DockContainerType.Document) & (DockManager.Style == DockVisualStyle.VS2003))
            {
                this.btnTabL.Show();
                this.btnTabR.Show();
            }
        }

        internal void StopAutoHide()
        {
            DockEventArgs args;
            this.hideStorage.manager.AutoHideContainer(this, this.hideStorage.toplevelDock, false);
            if ((this.hideStorage.parent.TopLevelContainer != this.hideStorage.parent) & (this.hideStorage.parent.Dock != DockStyle.None))
            {
                args = new DockEventArgs(this.hideStorage.parent.GetVirtualDragDest(this.hideStorage.parentDock), this.DockType, true) {
                    IgnoreHierarchy = true
                };
                DockManager.NoGuidePlease = true;
                this.hideStorage.parent.DragWindow(this, args);
                DockManager.NoGuidePlease = false;
            }
            else
            {
                args = new DockEventArgs(this.hideStorage.manager.GetVirtualDragDest(this.hideStorage.toplevelDock), this.DockType, true) {
                    IgnoreHierarchy = true
                };
                DockManager.NoGuidePlease = true;
                this.hideStorage.manager.DragWindow(this, args);
                DockManager.NoGuidePlease = false;
            }
        }

        internal void StopFading(bool release)
        {
            this.fading = false;
            if (release)
            {
                base.Hide();
            }
            foreach (Control control in base.Controls)
            {
                if (!(control is DockPanel) | (control == this.activePanel))
                {
                    control.Show();
                }
            }
            this.autoHideTimer.Stop();
            if (release)
            {
                this.hideStorage.manager.ReleaseAutoHideContainer();
                if (this.fadeImage != null)
                {
                    this.fadeImage.Dispose();
                    this.fadeImage = null;
                }
                if (this.fadeBkImage != null)
                {
                    this.fadeBkImage.Dispose();
                    this.fadeBkImage = null;
                }
            }
            else
            {
                base.BringToFront();
                base.Focus();
                this.ShowButtons();
                base.Invalidate();
            }
        }

        internal void TransferControls(DockContainer con)
        {
            foreach (Control control in con.Controls)
            {
                base.Controls.Add(control);
            }
            base.DockPadding.All = 0;
        }

        public void UndockClick(object sender, EventArgs e)
        {
            if (this.autoHide)
            {
                this.hideStorage.manager.AutoHideContainer(this, DockStyle.Fill, false);
            }
            this.FloatWindow();
        }

        private void UpdateToolTip(Point pt)
        {
            if (this.activePanel != null)
            {
                for (int i = 0; i < this.panList.Count; i++)
                {
                    DockPanel panel = this.panList[i] as DockPanel;
                    RectangleF tabRect = panel.TabRect;
                    if (tabRect.Contains((PointF) pt))
                    {
                        if ((Control.MouseButtons == MouseButtons.Left) && (panel != this.activePanel))
                        {
                            if (i > this.panList.IndexOf(this.activePanel))
                            {
                                if ((pt.X - this.activePanel.TabRect.Right) < (tabRect.Width - this.activePanel.TabRect.Width))
                                {
                                    return;
                                }
                                this.panList.Remove(this.activePanel);
                                this.panList.Insert(Math.Min(i + 1, this.panList.Count), this.activePanel);
                            }
                            else
                            {
                                if ((this.activePanel.TabRect.Left - pt.X) < (tabRect.Width - this.activePanel.TabRect.Width))
                                {
                                    return;
                                }
                                this.panList.Remove(this.activePanel);
                                this.panList.Insert(i, this.activePanel);
                            }
                            base.Invalidate();
                        }
                        else
                        {
                            if ((this.toolTip.GetToolTip(this) != panel.Form.Text) | !this.toolTip.Active)
                            {
                                this.toolTip.SetToolTip(this, panel.Form.Text);
                                this.toolTip.Active = true;
                            }
                            this.ContextMenuStrip = this.contextMenuStrip;
                        }
                        return;
                    }
                }
                this.ContextMenuStrip = null;
                this.toolTip.Active = false;
            }
        }

        internal virtual void WriteXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("container");
            writer.WriteAttributeString("dock", this.Dock.ToString());
            writer.WriteAttributeString("width", base.Width.ToString());
            writer.WriteAttributeString("height", base.Height.ToString());
            writer.WriteAttributeString("type", this.dockType.ToString());
            if ((this.panList.Count > 0) && (this.activePanel != null))
            {
                writer.WriteAttributeString("active", this.panList.IndexOf(this.activePanel).ToString());
            }
            foreach (DockContainer container in this.conList)
            {
                container.WriteXml(writer);
            }
            foreach (DockPanel panel in this.panList)
            {
                if (panel.Form.AllowSave)
                {
                    panel.WriteXml(writer);
                }
            }
            writer.WriteEndElement();
        }

        [Browsable(false)]
        internal DockPanel ActivePanel
        {
            get
            {
                return this.activePanel;
            }
            set
            {
                if (this.activePanel != value)
                {
                    for (int i = 0; i < this.panList.Count; i++)
                    {
                        DockPanel panel = this.panList[i] as DockPanel;
                        if ((this.activePanel != null) & (panel != value))
                        {
                            this.activePanel.Hide();
                            this.activePanel.SetFocus(false);
                        }
                    }
                }
                this.activePanel = value;
                if (this.activePanel != null)
                {
                    if (!this.activePanel.Visible)
                    {
                        this.activePanel.Show();
                    }
                    this.activePanel.BringToFront();
                    this.activePanel.SetFocus(true);
                    this.ShowButtons();
                    this.SetWindowText();
                }
                base.Invalidate();
            }
        }

        internal bool AllowSplit
        {
            get
            {
                foreach (DockPanel panel in this.panList)
                {
                    if (!panel.Form.AllowSplit)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        [Description("允许取消停靠该窗口。"), Category("DockDotNET")]
        public bool AllowUnDock { get; set; }

        internal bool AutoHide
        {
            get
            {
                return this.autoHide;
            }
            set
            {
                try
                {
                    if (this.TopLevelContainer is DockManager)
                    {
                        this.autoHide = value;
                        DockContainer parent = this;
                        while (parent.Parent != null)
                        {
                            if (((parent.Parent is DockManager) | !(parent.Parent is DockContainer)) || ((parent.Parent as DockContainer).DockType == DockContainerType.Document))
                            {
                                break;
                            }
                            parent = parent.Parent as DockContainer;
                        }
                        if (this.autoHide)
                        {
                            DockContainer container2 = base.Parent as DockContainer;
                            this.hideStorage = new AutoHideStorage(this.TopLevelContainer as DockManager, container2, this.Dock, parent.Dock);
                            if (this.hideStorage.parentDock == DockStyle.Fill)
                            {
                                foreach (DockContainer container3 in this.hideStorage.parent.conList)
                                {
                                    if (container3 != this)
                                    {
                                        if (container3.Dock == DockStyle.Left)
                                        {
                                            this.hideStorage.parentDock = DockStyle.Right;
                                        }
                                        else if (container3.Dock == DockStyle.Right)
                                        {
                                            this.hideStorage.parentDock = DockStyle.Left;
                                        }
                                        else if (container3.Dock == DockStyle.Top)
                                        {
                                            this.hideStorage.parentDock = DockStyle.Bottom;
                                        }
                                        else if (container3.Dock == DockStyle.Bottom)
                                        {
                                            this.hideStorage.parentDock = DockStyle.Top;
                                        }
                                        break;
                                    }
                                }
                            }
                            this.hideStorage.parent.Controls.Remove(this);
                            base.Hide();
                            this.hideStorage.manager.AutoHideContainer(this, this.hideStorage.toplevelDock, true);
                        }
                        else
                        {
                            if (this.hideStorage.manager == null)
                            {
                                this.autoHide = false;
                                Console.WriteLine("DockContainer.AutoHide: the DockManager was removed or not set properly. Exiting AutoHide silently.");
                                return;
                            }
                            this.StopAutoHide();
                        }
                        base.Invalidate();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("DockContainer.AutoHide: " + exception.Message);
                    this.autoHide = false;
                }
            }
        }

        [DefaultValue(20), Category("DockDotNET")]
        public int DockBorder
        {
            get
            {
                return this.dockBorder;
            }
            set
            {
                this.dockBorder = value;
            }
        }

        [Browsable(false)]
        public virtual DockContainerType DockType
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

        private bool HideContainer
        {
            get
            {
                return ((base.Parent is DockForm) & (this.panList.Count < 2));
            }
        }

        internal bool IsEmpty
        {
            get
            {
                return ((this.panList.Count == 0) & (this.conList.Count == 0));
            }
        }

        internal bool IsPanelContainer
        {
            get
            {
                return ((this.panList.Count != 0) & (this.conList.Count == 0));
            }
        }

        internal bool IsRootContainer
        {
            get
            {
                return (base.Parent is DockForm);
            }
        }

        [Browsable(false)]
        public int PanelCount
        {
            get
            {
                return this.panList.Count;
            }
        }

        [Category("DockDotNET"), DefaultValue(true)]
        public bool ShowIcons
        {
            get
            {
                return this.showIcons;
            }
            set
            {
                this.showIcons = value;
            }
        }

        [Category("DockDotNET"), DefaultValue(4)]
        public int SplitterWidth
        {
            get
            {
                return this.splitterWidth;
            }
            set
            {
                this.splitterWidth = value;
            }
        }

        [Browsable(false)]
        public DockContainer TopLevelContainer
        {
            get
            {
                DockContainer parent = this;
                while ((parent.Parent is DockContainer) | (parent.Parent is DockManager))
                {
                    parent = parent.Parent as DockContainer;
                }
                return parent;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AutoHideStorage
        {
            public DockStyle parentDock;
            public DockStyle toplevelDock;
            public DockContainer parent;
            public DockManager manager;
            public AutoHideStorage(DockManager manager, DockContainer parent, DockStyle parentDock, DockStyle toplevelDock)
            {
                this.manager = manager;
                this.parent = parent;
                this.parentDock = parentDock;
                this.toplevelDock = toplevelDock;
            }
        }
    }
}

