namespace WinFormsUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class FormEx : FormBase
    {
        private bool _canResize = true;
        private Image _formBkg;
        private Image _formFringe = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.fringe_bkg.png");
        private bool _isTextWithShadow = false;
        private int _radius = 5;
        private WinFormsUI.SystemButtonManager _systemButtonManager;
        private Font _textFont = new Font("微软雅黑", 10f, FontStyle.Bold);
        private Color _textForeColor = Color.FromArgb(250, Color.White);
        private Color _textShadowColor = Color.FromArgb(2, Color.Black);
        private int _textShadowWidth = 4;

        public FormEx()
        {
            this.InitializeComponent();
            this.FormExIni();
            this._systemButtonManager = new WinFormsUI.SystemButtonManager(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._systemButtonManager != null))
            {
                this._systemButtonManager.Dispose();
                this._systemButtonManager = null;
                this._formFringe.Dispose();
                this._formFringe = null;
                this._textFont.Dispose();
                this._textFont = null;
                if (this._formBkg != null)
                {
                    this._formBkg.Dispose();
                    this._formBkg = null;
                }
            }
            base.Dispose(disposing);
        }

        private void FormExIni()
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.SetStyles();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.ClientSize = new Size(0x2ac, 0x192);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Name = "FormEx";
            base.ResumeLayout(false);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            RenderHelper.SetFormRoundRectRgn(this, this.Radius);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.UpdateSystemButtonRect();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Down);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.SystemButtonManager.ProcessMouseOperate(Point.Empty, MouseOperate.Leave);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Move);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                this.SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Up);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (this.BackgroundImage != null)
            {
                switch (this.BackgroundImageLayout)
                {
                    case ImageLayout.None:
                    case ImageLayout.Tile:
                    case ImageLayout.Center:
                        e.Graphics.DrawImage(this._formBkg, base.ClientRectangle, base.ClientRectangle, GraphicsUnit.Pixel);
                        break;

                    case ImageLayout.Stretch:
                    case ImageLayout.Zoom:
                        e.Graphics.DrawImage(this._formBkg, base.ClientRectangle, new Rectangle(0, 0, this._formBkg.Width, this._formBkg.Height), GraphicsUnit.Pixel);
                        break;
                }
            }
            this.SystemButtonManager.DrawSystemButtons(e.Graphics);
            RenderHelper.DrawFormFringe(this, e.Graphics, this._formFringe, this.Radius);
            if ((base.Icon != null) && base.ShowIcon)
            {
                e.Graphics.DrawIcon(base.Icon, this.IconRect);
            }
            if (this.Text.Length != 0)
            {
                if (this.TextWithShadow)
                {
                    using (Image image = RenderHelper.GetStringImgWithShadowEffect(this.Text, this.TextFont, this.TextForeColor, this.TextShadowColor, this.TextShadowWidth))
                    {
                        e.Graphics.DrawImage(image, this.TextRect.Location);
                    }
                }
                else
                {
                    TextRenderer.DrawText(e.Graphics, this.Text, this.TextFont, this.TextRect, this.TextForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RenderHelper.SetFormRoundRectRgn(this, this.Radius);
            this.UpdateSystemButtonRect();
            this.UpdateMaxButton();
        }

        private void SetStyles()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        private void UpdateMaxButton()
        {
            if (base.WindowState == FormWindowState.Maximized)
            {
                this.SystemButtonManager.SystemButtonArray[1].NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.restore_normal.png");
                this.SystemButtonManager.SystemButtonArray[1].HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.restore_highlight.png");
                this.SystemButtonManager.SystemButtonArray[1].DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.restore_down.png");
                this.SystemButtonManager.SystemButtonArray[1].ToolTip = "还原";
            }
            else
            {
                this.SystemButtonManager.SystemButtonArray[1].NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_normal.png");
                this.SystemButtonManager.SystemButtonArray[1].HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_highlight.png");
                this.SystemButtonManager.SystemButtonArray[1].DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_down.png");
                this.SystemButtonManager.SystemButtonArray[1].ToolTip = "最大化";
            }
        }

        protected void UpdateSystemButtonRect()
        {
            bool maximizeBox = base.MaximizeBox;
            bool minimizeBox = base.MinimizeBox;
            Rectangle rectangle = new Rectangle(base.Width - this.SystemButtonManager.SystemButtonArray[0].NormalImg.Width, -1, this.SystemButtonManager.SystemButtonArray[0].NormalImg.Width, this.SystemButtonManager.SystemButtonArray[0].NormalImg.Height);
            this.SystemButtonManager.SystemButtonArray[0].LocationRect = rectangle;
            if (maximizeBox)
            {
                this.SystemButtonManager.SystemButtonArray[1].LocationRect = new Rectangle(rectangle.X - this.SystemButtonManager.SystemButtonArray[1].NormalImg.Width, -1, this.SystemButtonManager.SystemButtonArray[1].NormalImg.Width, this.SystemButtonManager.SystemButtonArray[1].NormalImg.Height);
            }
            else
            {
                this.SystemButtonManager.SystemButtonArray[1].LocationRect = Rectangle.Empty;
            }
            if (!minimizeBox)
            {
                this.SystemButtonManager.SystemButtonArray[2].LocationRect = Rectangle.Empty;
            }
            else if (maximizeBox)
            {
                this.SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(this.SystemButtonManager.SystemButtonArray[1].LocationRect.X - this.SystemButtonManager.SystemButtonArray[2].NormalImg.Width, -1, this.SystemButtonManager.SystemButtonArray[2].NormalImg.Width, this.SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
            else
            {
                this.SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(rectangle.X - this.SystemButtonManager.SystemButtonArray[2].NormalImg.Width, -1, this.SystemButtonManager.SystemButtonArray[2].NormalImg.Width, this.SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
        }

        private void WmNcHitTest(ref Message m)
        {
            int num = m.LParam.ToInt32();
            Point p = new Point(RenderHelper.LOWORD(num), RenderHelper.HIWORD(num));
            p = base.PointToClient(p);
            if ((base.WindowState != FormWindowState.Maximized) && this.CanResize)
            {
                if ((p.X < 5) && (p.Y < 5))
                {
                    m.Result = new IntPtr(13);
                    return;
                }
                if ((p.X > (base.Width - 5)) && (p.Y < 5))
                {
                    m.Result = new IntPtr(14);
                    return;
                }
                if ((p.X < 5) && (p.Y > (base.Height - 5)))
                {
                    m.Result = new IntPtr(0x10);
                    return;
                }
                if ((p.X > (base.Width - 5)) && (p.Y > (base.Height - 5)))
                {
                    m.Result = new IntPtr(0x11);
                    return;
                }
                if (p.Y < 3)
                {
                    m.Result = new IntPtr(12);
                    return;
                }
                if (p.Y > (base.Height - 3))
                {
                    m.Result = new IntPtr(15);
                    return;
                }
                if (p.X < 3)
                {
                    m.Result = new IntPtr(10);
                    return;
                }
                if (p.X > (base.Width - 3))
                {
                    m.Result = new IntPtr(11);
                    return;
                }
            }
            m.Result = new IntPtr(1);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 20:
                    m.Result = IntPtr.Zero;
                    break;

                case 0x84:
                    this.WmNcHitTest(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        public override Image BackgroundImage
        {
            get
            {
                return this._formBkg;
            }
            set
            {
                if (this._formBkg != value)
                {
                    this._formBkg = value;
                    base.Invalidate();
                }
            }
        }

        [Description("是否允许窗体改变大小")]
        public bool CanResize
        {
            get
            {
                return this._canResize;
            }
            set
            {
                if (this._canResize != value)
                {
                    this._canResize = value;
                }
            }
        }

        [Description("返回窗体关闭系统按钮所在的坐标矩形"), Browsable(false)]
        public Rectangle CloseBoxRect
        {
            get
            {
                return this.SystemButtonManager.SystemButtonArray[0].LocationRect;
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                if (!base.DesignMode)
                {
                    if (base.MaximizeBox)
                    {
                        createParams.Style |= 0x10000;
                    }
                    if (base.MinimizeBox)
                    {
                        createParams.Style |= 0x20000;
                    }
                    createParams.ClassStyle |= 0x20000;
                }
                return createParams;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            set
            {
                base.FormBorderStyle = value;
            }
        }

        internal Rectangle IconRect
        {
            get
            {
                if (base.ShowIcon && (base.Icon != null))
                {
                    return new Rectangle(8, 6, SystemInformation.SmallIconSize.Width, SystemInformation.SmallIconSize.Width);
                }
                return Rectangle.Empty;
            }
        }

        [Browsable(false), Description("返回窗体最大化或者还原系统按钮所在的坐标矩形")]
        public Rectangle MaximiziBoxRect
        {
            get
            {
                return this.SystemButtonManager.SystemButtonArray[1].LocationRect;
            }
        }

        [Description("返回窗体最小化系统按钮所在的坐标矩形"), Browsable(false)]
        public Rectangle MinimiziBoxRect
        {
            get
            {
                return this.SystemButtonManager.SystemButtonArray[2].LocationRect;
            }
        }

        [Description("窗体圆角的半径")]
        public int Radius
        {
            get
            {
                return this._radius;
            }
            set
            {
                if (this._radius != value)
                {
                    this._radius = value;
                    base.Invalidate();
                }
            }
        }

        internal WinFormsUI.SystemButtonManager SystemButtonManager
        {
            get
            {
                if (this._systemButtonManager == null)
                {
                    this._systemButtonManager = new WinFormsUI.SystemButtonManager(this);
                }
                return this._systemButtonManager;
            }
        }

        [Description("用于绘制窗体标题的字体")]
        public Font TextFont
        {
            get
            {
                return this._textFont;
            }
            set
            {
                if (this._textFont != value)
                {
                    this._textFont = value;
                }
            }
        }

        [Description("用于绘制窗体标题的颜色")]
        public Color TextForeColor
        {
            get
            {
                return this._textForeColor;
            }
            set
            {
                if (this._textForeColor != value)
                {
                    this._textForeColor = value;
                }
            }
        }

        internal Rectangle TextRect
        {
            get
            {
                if (base.Text.Length != 0)
                {
                    return new Rectangle(this.IconRect.Right + 2, 4, base.Width - ((8 + this.IconRect.Width) + 2), this.TextFont.Height);
                }
                return Rectangle.Empty;
            }
        }

        [Description("如果TextWithShadow属性为True,则使用该属性绘制阴影")]
        public Color TextShadowColor
        {
            get
            {
                return this._textShadowColor;
            }
            set
            {
                if (this._textShadowColor != value)
                {
                    this._textShadowColor = value;
                }
            }
        }

        [Description("如果TextWithShadow属性为True,则使用该属性获取或色泽阴影的宽度")]
        public int TextShadowWidth
        {
            get
            {
                return this._textShadowWidth;
            }
            set
            {
                if (this._textShadowWidth != value)
                {
                    this._textShadowWidth = value;
                }
            }
        }

        [Description("是否绘制带有阴影的窗体标题")]
        public bool TextWithShadow
        {
            get
            {
                return this._isTextWithShadow;
            }
            set
            {
                if (this._isTextWithShadow != value)
                {
                    this._isTextWithShadow = value;
                }
            }
        }
    }
}

