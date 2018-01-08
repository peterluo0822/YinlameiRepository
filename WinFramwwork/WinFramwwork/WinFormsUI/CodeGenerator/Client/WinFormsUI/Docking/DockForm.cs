namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    internal class DockForm : Form
    {
        private bool allowDock;
        private IContainer components;
        private DockContainer dragTarget;
        private OverlayForm dragWindow;
        private bool moving;
        private Point ptRef;
        private Point ptStart;
        private DockContainer rootContainer;

        public DockForm()
        {
            this.dragWindow = null;
            this.dragTarget = null;
            this.rootContainer = null;
            this.moving = false;
            this.allowDock = true;
            this.components = null;
            this.InitializeComponent();
            this.rootContainer = new DockContainer();
            this.rootContainer.Dock = DockStyle.Fill;
            base.Controls.Add(this.rootContainer);
            this.RegisterToMdiContainer();
        }

        public DockForm(Panel dragObject)
        {
            this.dragWindow = null;
            this.dragTarget = null;
            this.rootContainer = null;
            this.moving = false;
            this.allowDock = true;
            this.components = null;
            this.InitializeComponent();
            base.Opacity = 0.0;
            if (dragObject is DockContainer)
            {
                DockContainer container = dragObject as DockContainer;
                if (container.panList.Count == 1)
                {
                    base.ClientSize = (container.panList[0] as DockPanel).Form.ClientSize;
                }
                else
                {
                    base.ClientSize = dragObject.Size;
                }
                if (container.removeable)
                {
                    this.rootContainer = container;
                }
                else
                {
                    this.rootContainer = new DockContainer();
                    this.rootContainer.Controls.AddRange((DockPanel[]) container.panList.ToArray(typeof(DockPanel)));
                    this.rootContainer.Controls.AddRange((DockContainer[]) container.conList.ToArray(typeof(DockContainer)));
                    ArrayList list = new ArrayList();
                    this.rootContainer.GetPanels(list);
                    if (list.Count > 0)
                    {
                        this.rootContainer.DockType = (list[0] as DockPanel).Form.DockType;
                    }
                }
            }
            else if (dragObject is DockPanel)
            {
                DockPanel panel = dragObject as DockPanel;
                base.ClientSize = panel.Form.ClientSize;
                this.rootContainer = new DockContainer();
                panel.Form.CopyToDockForm(this);
            }
            if (this.rootContainer.IsPanelContainer)
            {
                (this.rootContainer.panList[0] as DockPanel).Form.CopyPropToDockForm(this);
                this.rootContainer.SetFormSizeBounds(this);
                this.rootContainer.SelectTab(0);
            }
            this.rootContainer.Dock = DockStyle.Fill;
            base.Controls.Add(this.rootContainer);
            this.RegisterToMdiContainer();
        }

        private void CloseDragWindow()
        {
            if (this.dragWindow != null)
            {
                this.dragWindow.Close();
                this.dragWindow.Dispose();
                this.dragWindow = null;
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

        private void EndMoving()
        {
            this.CloseDragWindow();
            base.BringToFront();
            base.Capture = false;
            this.moving = false;
            DockManager.HideDockGuide();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x124, 0xfc);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.KeyPreview = true;
            base.Name = "DockForm";
            base.ResumeLayout(false);
        }

        internal void MoveWindow()
        {
            this.dragTarget = this.SendDockEvent(false);
            if (this.dragTarget != null)
            {
                if (this.dragWindow == null)
                {
                    this.dragWindow = new OverlayForm();
                    this.dragWindow.Size = this.dragTarget.Size;
                    this.dragWindow.Show();
                    base.BringToFront();
                }
                else
                {
                    this.dragWindow.Size = this.dragTarget.Size;
                }
                if (this.dragTarget.Parent != null)
                {
                    this.dragWindow.Location = this.dragTarget.RectangleToScreen(this.dragTarget.ClientRectangle).Location;
                }
                else
                {
                    this.dragWindow.Location = this.dragTarget.Location;
                }
            }
            else if (DockManager.FastMoveDraw)
            {
                if (this.dragWindow == null)
                {
                    this.dragWindow = new OverlayForm();
                    this.dragWindow.Size = base.Size;
                    this.dragWindow.Show();
                }
                else
                {
                    this.dragWindow.Size = base.Size;
                }
                this.dragWindow.Location = base.Location;
            }
            else
            {
                this.CloseDragWindow();
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            DockManager.FormActivated(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.rootContainer != null)
            {
                this.rootContainer.ActivePanel = null;
                this.rootContainer.CloseClick(this, EventArgs.Empty);
            }
            base.OnClosing(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.rootContainer.InvokeKeyDown(this, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.rootContainer.InvokeKeyUp(this, e);
        }

        internal void ReadXml(XmlReader reader)
        {
            try
            {
                int x = 0;
                int y = 0;
                reader.Read();
                string attribute = reader.GetAttribute("width");
                if (attribute != null)
                {
                    base.Width = int.Parse(attribute);
                }
                attribute = reader.GetAttribute("height");
                if (attribute != null)
                {
                    base.Height = int.Parse(attribute);
                }
                attribute = reader.GetAttribute("x");
                if (attribute != null)
                {
                    x = int.Parse(attribute);
                }
                attribute = reader.GetAttribute("y");
                if (attribute != null)
                {
                    y = int.Parse(attribute);
                }
                base.Location = new Point(x, y);
                while (!reader.EOF)
                {
                    if (reader.IsStartElement() & (reader.Name == "container"))
                    {
                        this.rootContainer.ReadXml(reader.ReadSubtree(), false);
                        return;
                    }
                    reader.Read();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockContainer.ReadXml: " + exception.Message);
            }
        }

        private void RegisterToMdiContainer()
        {
            if (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].AddOwnedForm(this);
            }
            DockManager.RegisterForm(this);
        }

        internal DockContainer SendDockEvent(bool confirm)
        {
            DockEventArgs e = new DockEventArgs(new Point(Control.MousePosition.X, Control.MousePosition.Y), this.rootContainer.DockType, confirm);
            DockManager.InvokeDragEvent(this, e);
            if (e.Release)
            {
                DockManager.HideDockGuide();
            }
            return e.Target;
        }

        public void StartMoving(Point start)
        {
            this.moving = true;
            this.ptStart = start;
            this.ptRef = new Point(base.Location.X, base.Location.Y);
            base.BringToFront();
            base.Capture = true;
            base.Opacity = 1.0;
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0xa1) & (m.WParam == ((IntPtr) 2L)))
            {
                this.StartMoving(new Point(Control.MousePosition.X, Control.MousePosition.Y));
            }
            else
            {
                if (this.moving)
                {
                    if (m.Msg == 0x200)
                    {
                        Application.DoEvents();
                        if (!base.IsDisposed)
                        {
                            if (Control.MouseButtons == MouseButtons.None)
                            {
                                this.EndMoving();
                                base.Show();
                            }
                            else
                            {
                                if (DockManager.FastMoveDraw & base.Visible)
                                {
                                    base.Hide();
                                }
                                base.Location = new Point((this.ptRef.X + Control.MousePosition.X) - this.ptStart.X, (this.ptRef.Y + Control.MousePosition.Y) - this.ptStart.Y);
                                this.MoveWindow();
                                base.Capture = true;
                            }
                        }
                        return;
                    }
                    if ((m.Msg == 0x202) | ((m.Msg == 160) & (Control.MouseButtons == MouseButtons.None)))
                    {
                        this.EndMoving();
                        if (this.SendDockEvent(true) != null)
                        {
                            this.rootContainer = null;
                            base.Close();
                            return;
                        }
                        base.Show();
                    }
                    else if (m.Msg == 0x2a3)
                    {
                        this.EndMoving();
                        base.Show();
                    }
                }
                base.WndProc(ref m);
            }
        }

        internal void WriteXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("form");
            writer.WriteAttributeString("width", base.Width.ToString());
            writer.WriteAttributeString("height", base.Height.ToString());
            writer.WriteAttributeString("x", base.Location.X.ToString());
            writer.WriteAttributeString("y", base.Location.Y.ToString());
            this.rootContainer.WriteXml(writer);
            writer.WriteEndElement();
        }

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

        public DockContainer DragTarget
        {
            get
            {
                return this.dragTarget;
            }
        }

        public bool Moving
        {
            get
            {
                return this.moving;
            }
        }

        public DockContainer RootContainer
        {
            get
            {
                return this.rootContainer;
            }
            set
            {
                if (this.rootContainer != null)
                {
                    base.Controls.Remove(this.rootContainer);
                    this.rootContainer.Dispose();
                }
                this.rootContainer = value;
                base.Controls.Add(this.rootContainer);
            }
        }
    }
}

