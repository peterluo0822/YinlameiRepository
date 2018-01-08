namespace WinFormsUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class QQGlassButton : PictureBox, IButtonControl
    {
        private System.Drawing.Font _defaultFont = new System.Drawing.Font("微软雅黑", 9f);
        private System.Windows.Forms.DialogResult _DialogResult;
        private Image _glassDownImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQGlassButton.Image.glassbtn_down.png");
        private Image _glassHotImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQGlassButton.Image.glassbtn_hot.png");
        private bool _holdingSpace = false;
        private bool _isDefault = false;
        private QQControlState _state = QQControlState.Normal;
        private ToolTip _toolTip = new ToolTip();

        public QQGlassButton()
        {
            this.BackColor = Color.Transparent;
            base.Size = new Size(0x4b, 0x17);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Font = this._defaultFont;
        }

        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect)
        {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (!((base.Image != null) || string.IsNullOrEmpty(this.Text)))
            {
                textRect = new Rectangle(3, 0, base.Width - 6, base.Height);
            }
            else if ((base.Image != null) && string.IsNullOrEmpty(this.Text))
            {
                imageRect = new Rectangle((base.Width - base.Image.Width) / 2, (base.Height - base.Image.Height) / 2, base.Image.Width, base.Image.Height);
            }
            else if (!((base.Image == null) || string.IsNullOrEmpty(this.Text)))
            {
                imageRect = new Rectangle(4, (base.Height - base.Image.Height) / 2, base.Image.Width, base.Image.Height);
                textRect = new Rectangle(imageRect.Right + 1, 0, ((base.Width - 8) - imageRect.Width) - 1, base.Height);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._defaultFont != null)
                {
                    this._defaultFont.Dispose();
                }
                if (this._glassDownImg != null)
                {
                    this._glassDownImg.Dispose();
                }
                if (this._glassHotImg != null)
                {
                    this._glassHotImg.Dispose();
                }
                if (this._toolTip != null)
                {
                    this._toolTip.Dispose();
                }
            }
            this._defaultFont = null;
            this._glassDownImg = null;
            this._glassHotImg = null;
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
            this._state = QQControlState.Normal;
            base.Invalidate();
            this._holdingSpace = false;
            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._state = QQControlState.Down;
            }
            base.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.ToolTipText != string.Empty)
            {
                this.HideToolTip();
                this.ShowTooTip(this.ToolTipText);
            }
            this._state = QQControlState.Highlight;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._state = QQControlState.Normal;
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (base.ClientRectangle.Contains(e.Location))
                {
                    this._state = QQControlState.Highlight;
                }
                else
                {
                    this._state = QQControlState.Normal;
                }
            }
            base.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle rectangle;
            Rectangle rectangle2;
            this.CalculateRect(out rectangle, out rectangle2);
            switch (this._state)
            {
                case QQControlState.Highlight:
                    RenderHelper.DrawImageWithNineRect(pe.Graphics, this._glassHotImg, base.ClientRectangle, new Rectangle(0, 0, this._glassDownImg.Width, this._glassDownImg.Height));
                    break;

                case QQControlState.Down:
                    RenderHelper.DrawImageWithNineRect(pe.Graphics, this._glassDownImg, base.ClientRectangle, new Rectangle(0, 0, this._glassDownImg.Width, this._glassDownImg.Height));
                    break;
            }
            if (base.Image != null)
            {
                pe.Graphics.DrawImage(base.Image, rectangle, new Rectangle(0, 0, base.Image.Width, base.Image.Height), GraphicsUnit.Pixel);
            }
            if (this.Text.Trim().Length != 0)
            {
                TextRenderer.DrawText(pe.Graphics, this.Text, this.Font, rectangle2, SystemColors.ControlText);
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
        public Image BackgroundImage
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image ErrorImage
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

        [Browsable(true), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("The font used to display text in the control.")]
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
        public Image InitialImage
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

        [Description("The text associated with the control."), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Category("Appearance")]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

