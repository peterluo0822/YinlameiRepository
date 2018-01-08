namespace WinFormsUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class QQCheckBox : CheckBox
    {
        private Image _checkImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQCheckBox.Image.check.png");
        private Font _defaultFont = new Font("微软雅黑", 9f);
        private QQControlState _state = QQControlState.Normal;
        private static readonly ContentAlignment LeftAlignment = (ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft);
        private static readonly ContentAlignment RightAlignment = (ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight);

        public QQCheckBox()
        {
            this.SetStyles();
            this.BackColor = Color.Transparent;
            this.Font = this._defaultFont;
        }

        private void CalculateRect(out Rectangle checkRect, out Rectangle textRect)
        {
            checkRect = new Rectangle(0, 0, this.CheckRectWidth, this.CheckRectWidth);
            textRect = Rectangle.Empty;
            bool flag = (LeftAlignment & base.CheckAlign) != ((ContentAlignment) 0);
            bool flag2 = (RightAlignment & base.CheckAlign) != ((ContentAlignment) 0);
            bool flag3 = this.RightToLeft == RightToLeft.Yes;
            if ((!flag || flag3) && (!flag2 || !flag3))
            {
                if ((!flag2 || flag3) && (!flag || !flag3))
                {
                    switch (base.CheckAlign)
                    {
                        case ContentAlignment.TopCenter:
                            checkRect.Y = 2;
                            textRect.Y = checkRect.Bottom + 2;
                            textRect.Height = (base.Height - this.CheckRectWidth) - 6;
                            break;

                        case ContentAlignment.MiddleCenter:
                            checkRect.Y = (base.Height - this.CheckRectWidth) / 2;
                            textRect.Y = 0;
                            textRect.Height = base.Height;
                            break;

                        case ContentAlignment.BottomCenter:
                            checkRect.Y = (base.Height - this.CheckRectWidth) - 2;
                            textRect.Y = 0;
                            textRect.Height = (base.Height - this.CheckRectWidth) - 6;
                            break;
                    }
                    checkRect.X = (base.Width - this.CheckRectWidth) / 2;
                    textRect.X = 2;
                    textRect.Width = base.Width - 4;
                    return;
                }
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        checkRect.Y = 2;
                        break;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        checkRect.Y = (base.Height - this.CheckRectWidth) / 2;
                        break;

                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        checkRect.Y = (base.Height - this.CheckRectWidth) - 2;
                        break;
                }
            }
            else
            {
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        checkRect.Y = 2;
                        break;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        checkRect.Y = (base.Height - this.CheckRectWidth) / 2;
                        break;

                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        checkRect.Y = (base.Height - this.CheckRectWidth) - 2;
                        break;
                }
                checkRect.X = 1;
                textRect = new Rectangle(checkRect.Right + 2, 0, (base.Width - checkRect.Right) - 4, base.Height);
                return;
            }
            checkRect.X = (base.Width - this.CheckRectWidth) - 1;
            textRect = new Rectangle(2, 0, (base.Width - this.CheckRectWidth) - 6, base.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._checkImg != null)
                {
                    this._checkImg.Dispose();
                }
                if (this._defaultFont != null)
                {
                    this._defaultFont.Dispose();
                }
            }
            this._checkImg = null;
            this._defaultFont = null;
            base.Dispose(disposing);
        }

        private void DrawDisabledCheckRect(Graphics g, Rectangle checkRect)
        {
            g.DrawRectangle(SystemPens.ControlDark, checkRect);
            if (base.Checked)
            {
                g.DrawImage(this._checkImg, checkRect, 0, 0, this._checkImg.Width, this._checkImg.Height, GraphicsUnit.Pixel);
            }
        }

        private void DrawHighLightCheckRect(Graphics g, Rectangle checkRect)
        {
            this.DrawNormalCheckRect(g, checkRect);
            using (Pen pen = new Pen(ColorTable.QQHighLightInnerColor))
            {
                g.DrawRectangle(pen, checkRect);
                checkRect.Inflate(1, 1);
                pen.Color = ColorTable.QQHighLightColor;
                g.DrawRectangle(pen, checkRect);
            }
        }

        private void DrawNormalCheckRect(Graphics g, Rectangle checkRect)
        {
            g.FillRectangle(Brushes.White, checkRect);
            using (Pen pen = new Pen(ColorTable.QQBorderColor))
            {
                g.DrawRectangle(pen, checkRect);
            }
            if (base.Checked)
            {
                g.DrawImage(this._checkImg, checkRect, 0, 0, this._checkImg.Width, this._checkImg.Height, GraphicsUnit.Pixel);
            }
        }

        private static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }
            ContentAlignment alignment2 = alignment;
            if (alignment2 <= ContentAlignment.MiddleCenter)
            {
                switch (alignment2)
                {
                    case ContentAlignment.TopLeft:
                        return flags;

                    case ContentAlignment.TopCenter:
                        return (flags | TextFormatFlags.HorizontalCenter);

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        return flags;

                    case ContentAlignment.TopRight:
                        return (flags | TextFormatFlags.Right);

                    case ContentAlignment.MiddleLeft:
                        return (flags | TextFormatFlags.VerticalCenter);

                    case ContentAlignment.MiddleCenter:
                        return (flags | (TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter));
                }
                return flags;
            }
            if (alignment2 <= ContentAlignment.BottomLeft)
            {
                switch (alignment2)
                {
                    case ContentAlignment.MiddleRight:
                        return (flags | (TextFormatFlags.VerticalCenter | TextFormatFlags.Right));

                    case ContentAlignment.BottomLeft:
                        return (flags | TextFormatFlags.Bottom);
                }
                return flags;
            }
            if (alignment2 != ContentAlignment.BottomCenter)
            {
                if (alignment2 != ContentAlignment.BottomRight)
                {
                    return flags;
                }
                return (flags | (TextFormatFlags.Bottom | TextFormatFlags.Right));
            }
            return (flags | (TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter));
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (base.Enabled)
            {
                this._state = QQControlState.Normal;
            }
            else
            {
                this._state = QQControlState.Disabled;
            }
            base.OnEnabledChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                this._state = QQControlState.Down;
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this._state = QQControlState.Highlight;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._state = QQControlState.Normal;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (base.ClientRectangle.Contains(mevent.Location))
                {
                    this._state = QQControlState.Highlight;
                }
                else
                {
                    this._state = QQControlState.Normal;
                }
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle rectangle;
            Rectangle rectangle2;
            base.OnPaint(pevent);
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            this.CalculateRect(out rectangle, out rectangle2);
            if (!base.Enabled)
            {
                this._state = QQControlState.Disabled;
            }
            switch (this._state)
            {
                case QQControlState.Highlight:
                case QQControlState.Down:
                    this.DrawHighLightCheckRect(g, rectangle);
                    break;

                case QQControlState.Disabled:
                    this.DrawDisabledCheckRect(g, rectangle);
                    break;

                default:
                    this.DrawNormalCheckRect(g, rectangle);
                    break;
            }
            Color foreColor = base.Enabled ? this.ForeColor : SystemColors.GrayText;
            TextRenderer.DrawText(g, this.Text, this.Font, rectangle2, foreColor, GetTextFormatFlags(this.TextAlign, this.RightToLeft == RightToLeft.Yes));
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

        [Description("获取QQCheckBox左边正方形的宽度")]
        protected virtual int CheckRectWidth
        {
            get
            {
                return 13;
            }
        }
    }
}

