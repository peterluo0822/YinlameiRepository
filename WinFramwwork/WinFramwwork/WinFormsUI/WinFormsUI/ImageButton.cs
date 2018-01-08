namespace WinFormsUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ImageButton : PictureBox, IButtonControl
    {
        private System.Windows.Forms.DialogResult _DialogResult;
        private bool _down = false;
        private System.Drawing.Image _DownImage;
        private bool _holdingSpace = false;
        private bool _hover = false;
        private System.Drawing.Image _HoverImage;
        private bool _isDefault = false;
        private System.Drawing.Image _NormalImage;
        private ToolTip _toolTip = new ToolTip();
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        public ImageButton()
        {
            this.BackColor = Color.Transparent;
            base.Size = new Size(0x4b, 0x17);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._toolTip != null))
            {
                this._toolTip.Dispose();
            }
            this._toolTip = null;
            base.Dispose(disposing);
        }

        private void HideToolTip()
        {
            this._toolTip.Active = false;
        }

        public void NotifyDefault(bool value)
        {
            if (this._isDefault != value)
            {
                this._isDefault = value;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this._holdingSpace = false;
            this.OnMouseUp(null);
            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.Focus();
            this.OnMouseUp(null);
            this._down = true;
            if (this._DownImage != null)
            {
                this.Image = this._DownImage;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.ToolTipText != string.Empty)
            {
                this.HideToolTip();
                this.ShowTooTip(this.ToolTipText);
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._hover = false;
            this.Image = this._NormalImage;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this._hover = true;
            if (this._down)
            {
                if ((this._DownImage != null) && (this.Image != this._DownImage))
                {
                    this.Image = this._DownImage;
                }
            }
            else if (this._HoverImage != null)
            {
                this.Image = this._HoverImage;
            }
            else
            {
                this.Image = this._NormalImage;
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this._down = false;
            if (this._hover)
            {
                if (this._HoverImage != null)
                {
                    this.Image = this._HoverImage;
                }
            }
            else
            {
                this.Image = this._NormalImage;
            }
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if ((!string.IsNullOrEmpty(this.Text) && (pe != null)) && (base.Font != null))
            {
                PointF tf;
                SizeF ef = pe.Graphics.MeasureString(base.Text, base.Font);
                if (base.Image != null)
                {
                    tf = new PointF((base.Image.Width / 2) - (ef.Width / 2f), (base.Image.Height / 2) - (ef.Height / 2f));
                }
                else
                {
                    tf = new PointF((base.Width / 2) - (ef.Width / 2f), (base.Height / 2) - (ef.Height / 2f));
                }
                using (SolidBrush brush = new SolidBrush(base.ForeColor))
                {
                    pe.Graphics.DrawString(base.Text, base.Font, brush, tf);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.Refresh();
            base.OnTextChanged(e);
        }

        public void PerformClick()
        {
            base.OnClick(EventArgs.Empty);
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == 0x101)
            {
                if (this._holdingSpace)
                {
                    if (((int) msg.WParam) == 0x20)
                    {
                        this.OnMouseUp(null);
                        this.PerformClick();
                    }
                    else if ((((int) msg.WParam) == 0x1b) || (((int) msg.WParam) == 9))
                    {
                        this._holdingSpace = false;
                        this.OnMouseUp(null);
                    }
                }
                return true;
            }
            if (msg.Msg == 0x100)
            {
                if (((int) msg.WParam) == 0x20)
                {
                    this._holdingSpace = true;
                    this.OnMouseDown(null);
                }
                else if (((int) msg.WParam) == 13)
                {
                    this.PerformClick();
                }
                return true;
            }
            return base.PreProcessMessage(ref msg);
        }

        private void ShowTooTip(string toolTipText)
        {
            this._toolTip.Active = true;
            this._toolTip.SetToolTip(this, toolTipText);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Drawing.Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }

        [Description("Controls what type of border the ImageButton should have.")]
        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
            set
            {
                base.BorderStyle = value;
            }
        }

        public System.Windows.Forms.DialogResult DialogResult
        {
            get
            {
                return this._DialogResult;
            }
            set
            {
                if (Enum.IsDefined(typeof(System.Windows.Forms.DialogResult), value))
                {
                    this._DialogResult = value;
                }
            }
        }

        [Category("Appearance"), Description("Image to show when the button is depressed.")]
        public System.Drawing.Image DownImage
        {
            get
            {
                return this._DownImage;
            }
            set
            {
                this._DownImage = value;
                if (this._down)
                {
                    this.Image = value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Image ErrorImage
        {
            get
            {
                return base.ErrorImage;
            }
            set
            {
                base.ErrorImage = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true), Category("Appearance"), Description("The font used to display text in the control.")]
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Category("Appearance"), Description("Image to show when the button is hovered over.")]
        public System.Drawing.Image HoverImage
        {
            get
            {
                return this._HoverImage;
            }
            set
            {
                this._HoverImage = value;
                if (this._hover)
                {
                    this.Image = value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Drawing.Image Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ImageLocation
        {
            get
            {
                return base.ImageLocation;
            }
            set
            {
                base.ImageLocation = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Drawing.Image InitialImage
        {
            get
            {
                return base.InitialImage;
            }
            set
            {
                base.InitialImage = value;
            }
        }

        [Description("Image to show when the button is not in any other state."), Category("Appearance")]
        public System.Drawing.Image NormalImage
        {
            get
            {
                return this._NormalImage;
            }
            set
            {
                this._NormalImage = value;
                if (!(this._hover || this._down))
                {
                    this.Image = value;
                }
            }
        }

        [Description("Controls how the ImageButton will handle image placement and control sizing.")]
        public PictureBoxSizeMode SizeMode
        {
            get
            {
                return base.SizeMode;
            }
            set
            {
                base.SizeMode = value;
            }
        }

        [Category("Appearance"), Description("The text associated with the control."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Description("当鼠标放在控件可见处的提示文本")]
        public string ToolTipText { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool WaitOnLoad
        {
            get
            {
                return base.WaitOnLoad;
            }
            set
            {
                base.WaitOnLoad = value;
            }
        }
    }
}

