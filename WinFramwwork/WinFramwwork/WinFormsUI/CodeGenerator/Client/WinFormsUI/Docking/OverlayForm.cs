namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Reflection;
    using System.Windows.Forms;

    internal class OverlayForm : Form
    {
        private static Bitmap bmpDockAll = null;
        private static Bitmap bmpDockBottom = null;
        private static Bitmap bmpDockCenter = null;
        private static Bitmap bmpDockLeft = null;
        private static Bitmap bmpDockRight = null;
        private static Bitmap bmpDockTop = null;
        private IContainer components;
        private DockContainer targetHost;

        public OverlayForm()
        {
            this.targetHost = null;
            this.components = null;
            this.Init();
            this.targetHost = null;
        }

        public OverlayForm(DockContainer target)
        {
            this.targetHost = null;
            this.components = null;
            this.Init();
            this.targetHost = target;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public DockStyle HitTest(Point pt)
        {
            int width = 0x1d;
            int height = 0x20;
            int x = (base.Width / 2) - (width / 2);
            int y = (base.Height / 2) - (width / 2);
            Rectangle rectangle = base.RectangleToScreen(new Rectangle(x, y, width, width));
            Rectangle rectangle2 = base.RectangleToScreen(new Rectangle(x, y - height, width, height));
            Rectangle rectangle3 = base.RectangleToScreen(new Rectangle(x - height, y, height, width));
            Rectangle rectangle4 = base.RectangleToScreen(new Rectangle(x, y + width, width, height));
            Rectangle rectangle5 = base.RectangleToScreen(new Rectangle(x + width, y, height, width));
            if (rectangle.Contains(pt))
            {
                return DockStyle.Fill;
            }
            if (this.targetHost.AllowSplit)
            {
                if (rectangle2.Contains(pt))
                {
                    return DockStyle.Top;
                }
                if (rectangle3.Contains(pt))
                {
                    return DockStyle.Left;
                }
                if (rectangle4.Contains(pt))
                {
                    return DockStyle.Bottom;
                }
                if (rectangle5.Contains(pt))
                {
                    return DockStyle.Right;
                }
            }
            return DockStyle.None;
        }

        public DockStyle HitTest(int x, int y)
        {
            return this.HitTest(new Point(x, y));
        }

        private void Init()
        {
            this.InitializeComponent();
            Application.OpenForms[0].AddOwnedForm(this);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, false);
            if (bmpDockAll == null)
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                bmpDockAll = new Bitmap(executingAssembly.GetManifestResourceStream("WinFormsUI.Docking.img.DockAll.png"));
                bmpDockCenter = new Bitmap(executingAssembly.GetManifestResourceStream("WinFormsUI.Docking.img.DockCenter.bmp"));
                bmpDockLeft = new Bitmap(executingAssembly.GetManifestResourceStream("WinFormsUI.Docking.img.DockLeft.bmp"));
                bmpDockTop = new Bitmap(executingAssembly.GetManifestResourceStream("WinFormsUI.Docking.img.DockTop.bmp"));
                bmpDockRight = new Bitmap(executingAssembly.GetManifestResourceStream("WinFormsUI.Docking.img.DockRight.bmp"));
                bmpDockBottom = new Bitmap(executingAssembly.GetManifestResourceStream("WinFormsUI.Docking.img.DockBottom.bmp"));
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(1, 1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "OverlayForm";
            base.Opacity = 0.0;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "OverlayForm";
            base.TransparencyKey = Color.Black;
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            base.BringToFront();
            if (base.Opacity < 1.0)
            {
                base.Opacity = 1.0;
            }
            if (this.targetHost != null)
            {
                Bitmap bmpDockAll = OverlayForm.bmpDockAll;
                if (!this.targetHost.AllowSplit)
                {
                    bmpDockAll = bmpDockCenter;
                }
                int x = (base.Width / 2) - (bmpDockAll.Width / 2);
                int y = (base.Height / 2) - (bmpDockAll.Height / 2);
                graphics.DrawImageUnscaled(bmpDockAll, new Point(x, y));
            }
            else
            {
                Pen pen;
                Rectangle clientRectangle = base.ClientRectangle;
                if ((DockManager.Style == DockVisualStyle.VS2003) | DockManager.FastMoveDraw)
                {
                    HatchBrush brush = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(5, 5, 5), base.TransparencyKey);
                    pen = new Pen(brush, 6f);
                }
                else
                {
                    base.Opacity = 0.4;
                    graphics.Clear(Color.FromArgb(0x68, 10, 0x24, 0x6a));
                    pen = new Pen(SystemColors.Control, 2f);
                    clientRectangle.Inflate(-1, -1);
                }
                graphics.DrawRectangle(pen, clientRectangle);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(base.TransparencyKey);
        }

        public DockContainer TargetHost
        {
            get
            {
                return this.targetHost;
            }
            set
            {
                this.targetHost = value;
            }
        }
    }
}

