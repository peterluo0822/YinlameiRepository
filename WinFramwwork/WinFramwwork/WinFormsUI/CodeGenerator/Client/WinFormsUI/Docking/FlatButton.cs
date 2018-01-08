namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxItem(true), ToolboxBitmap(typeof(FlatButton))]
    public class FlatButton : Button
    {
        private Container components;
        private Color lightColor;
        private bool pressed;
        private Color selectColor;
        private Color shadowColor;

        public event PaintEventHandler PostPaint;

        public FlatButton()
        {
            this.components = null;
            this.pressed = false;
            this.lightColor = Color.White;
            this.shadowColor = Color.Black;
            this.selectColor = Color.Transparent;
            this.InitializeComponent();
        }

        public FlatButton(IContainer container)
        {
            this.components = null;
            this.pressed = false;
            this.lightColor = Color.White;
            this.shadowColor = Color.Black;
            this.selectColor = Color.Transparent;
            container.Add(this);
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.pressed = false;
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(this.BackColor);
            Point mousePosition = Control.MousePosition;
            if (base.RectangleToScreen(base.ClientRectangle).Contains(mousePosition) && base.Enabled)
            {
                Pen pen;
                Pen pen2;
                if (this.selectColor != Color.Transparent)
                {
                    graphics.FillRectangle(new SolidBrush(this.selectColor), base.ClientRectangle);
                }
                if (Control.MouseButtons == MouseButtons.Left)
                {
                    pen = new Pen(this.shadowColor);
                    pen2 = new Pen(this.lightColor);
                    this.pressed = true;
                }
                else
                {
                    pen = new Pen(this.lightColor);
                    pen2 = new Pen(this.shadowColor);
                    this.pressed = false;
                }
                graphics.DrawLine(pen, 0, 0, base.Width - 1, 0);
                graphics.DrawLine(pen, 0, 1, 0, base.Height - 2);
                graphics.DrawLine(pen2, 0, base.Height - 1, base.Width - 1, base.Height - 1);
                graphics.DrawLine(pen2, base.Width - 1, 1, base.Width - 1, base.Height - 2);
            }
            if (this.PostPaint != null)
            {
                this.PostPaint(this, e);
            }
        }

        public Color LightColor
        {
            get
            {
                return this.lightColor;
            }
            set
            {
                this.lightColor = value;
            }
        }

        public bool Pressed
        {
            get
            {
                return this.pressed;
            }
        }

        public Color SelectColor
        {
            get
            {
                return this.selectColor;
            }
            set
            {
                this.selectColor = value;
            }
        }

        public Color ShadowColor
        {
            get
            {
                return this.shadowColor;
            }
            set
            {
                this.shadowColor = value;
            }
        }
    }
}

