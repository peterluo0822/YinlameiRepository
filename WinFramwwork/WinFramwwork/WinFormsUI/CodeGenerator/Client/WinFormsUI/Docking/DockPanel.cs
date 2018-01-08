namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class DockPanel : Panel
    {
        private Container components;
        private DockWindow form;
        private Size maxFormSize;
        private Size minFormSize;
        private RectangleF tabRect;

        public event EventHandler Activated;

        public event EventHandler Deactivate;

        public event PaintEventHandler PostPaint;

        public DockPanel()
        {
            this.components = null;
            this.tabRect = Rectangle.Empty;
            this.InitializeComponent();
            this.Init();
        }

        public DockPanel(IContainer container)
        {
            this.components = null;
            this.tabRect = Rectangle.Empty;
            container.Add(this);
            this.InitializeComponent();
            this.Init();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Init()
        {
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void InitializeComponent()
        {
            this.AutoScroll = true;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (base.Parent != null)
            {
                base.Parent.Focus();
            }
            base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.PostPaint != null)
            {
                this.PostPaint(this, e);
            }
        }

        internal bool ReadXml(XmlReader reader)
        {
            try
            {
                string str;
                int width = 100;
                int height = 100;
                string attribute = reader.GetAttribute("dock");
                if (attribute == null)
                {
                    goto Label_0093;
                }
                if (!(attribute == "Fill"))
                {
                    if (attribute == "Top")
                    {
                        goto Label_006B;
                    }
                    if (attribute == "Bottom")
                    {
                        goto Label_0075;
                    }
                    if (attribute == "Left")
                    {
                        goto Label_007F;
                    }
                    if (attribute == "Right")
                    {
                        goto Label_0089;
                    }
                    goto Label_0093;
                }
                this.Dock = DockStyle.Fill;
                goto Label_009B;
            Label_006B:
                this.Dock = DockStyle.Top;
                goto Label_009B;
            Label_0075:
                this.Dock = DockStyle.Bottom;
                goto Label_009B;
            Label_007F:
                this.Dock = DockStyle.Left;
                goto Label_009B;
            Label_0089:
                this.Dock = DockStyle.Right;
                goto Label_009B;
            Label_0093:
                return false;
            Label_009B:
                str = reader.GetAttribute("width");
                if (str != null)
                {
                    width = int.Parse(str);
                }
                str = reader.GetAttribute("height");
                if (str != null)
                {
                    height = int.Parse(str);
                }
                str = reader.GetAttribute("type");
                if (str == null)
                {
                    return false;
                }
                System.Type type = System.Type.GetType(str, true);
                if (type == null)
                {
                    return false;
                }
                FormLoadEventArgs e = new FormLoadEventArgs(type);
                DockManager.InvokeFormLoad(this, e);
                DockWindow form = e.Form;
                if (e.Cancel)
                {
                    return false;
                }
                if (form == null)
                {
                    ConstructorInfo constructor = type.GetConstructor(System.Type.EmptyTypes);
                    if (constructor == null)
                    {
                        return false;
                    }
                    form = constructor.Invoke(new object[0]) as DockWindow;
                }
                base.Size = form.ControlContainer.Size;
                int count = form.ControlContainer.Controls.Count;
                if (count > 0)
                {
                    Control[] array = new Control[count];
                    form.ControlContainer.Controls.CopyTo(array, 0);
                    base.Controls.AddRange(array);
                }
                base.Size = new Size(width, height);
                form.ControlContainer = this;
                this.form = form;
                form.ShowFormAtOnLoad = false;
                form.Show();
                form.ShowFormAtOnLoad = true;
                if (form.HostContainer != null)
                {
                    form.Release();
                }
                form.ReadXml(reader);
            }
            catch (Exception exception)
            {
                Console.WriteLine("DockPanel.ReadXml: " + exception.Message);
            }
            return true;
        }

        public void SelectTab()
        {
            if (base.Parent is DockContainer)
            {
                (base.Parent as DockContainer).SelectTab(this);
                (base.Parent as DockContainer).Select();
            }
        }

        public void SetFocus(bool activate)
        {
            if (activate && (this.Activated != null))
            {
                this.Activated(this, EventArgs.Empty);
            }
            else if (!(activate || (this.Deactivate == null)))
            {
                this.Deactivate(this, EventArgs.Empty);
            }
        }

        public override string ToString()
        {
            return this.form.Text;
        }

        internal void WriteXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("panel");
            writer.WriteAttributeString("dock", this.Dock.ToString());
            writer.WriteAttributeString("width", base.Width.ToString());
            writer.WriteAttributeString("height", base.Height.ToString());
            writer.WriteAttributeString("type", this.form.GetType().AssemblyQualifiedName);
            this.form.WriteXml(writer);
            writer.WriteEndElement();
        }

        [Browsable(false)]
        public DockWindow Form
        {
            get
            {
                return this.form;
            }
            set
            {
                this.form = value;
            }
        }

        [Browsable(false)]
        public Size MaxFormSize
        {
            get
            {
                return this.maxFormSize;
            }
            set
            {
                this.maxFormSize = value;
            }
        }

        [Browsable(false)]
        public Size MinFormSize
        {
            get
            {
                return this.minFormSize;
            }
            set
            {
                this.minFormSize = value;
            }
        }

        [Browsable(false)]
        public RectangleF TabRect
        {
            get
            {
                return this.tabRect;
            }
            set
            {
                this.tabRect = value;
            }
        }
    }
}

