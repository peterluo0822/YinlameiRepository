namespace WinFormsUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class QQRadioButton : RadioButton
    {
        private Font _defaultFont = new Font("微软雅黑", 9f);
        private Image _dotImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQRadioButton.Image.dot.png");
        private QQControlState _state = QQControlState.Normal;
        private static readonly ContentAlignment LeftAlignment = (ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft);
        private static readonly ContentAlignment RightAlignment = (ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight);

        public QQRadioButton()
        {
            this.SetStyles();
            this.BackColor = Color.Transparent;
            this.Font = this._defaultFont;
        }

        private void CalculateRect(out Rectangle circleRect, out Rectangle textRect)
        {
            circleRect = new Rectangle(0, 0, this.CheckRectWidth, this.CheckRectWidth);
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
                            circleRect.Y = 2;
                            textRect.Y = circleRect.Bottom + 2;
                            textRect.Height = (base.Height - this.CheckRectWidth) - 6;
                            break;

                        case ContentAlignment.MiddleCenter:
                            circleRect.Y = (base.Height - this.CheckRectWidth) / 2;
                            textRect.Y = 0;
                            textRect.Height = base.Height;
                            break;

                        case ContentAlignment.BottomCenter:
                            circleRect.Y = (base.Height - this.CheckRectWidth) - 2;
                            textRect.Y = 0;
                            textRect.Height = (base.Height - this.CheckRectWidth) - 6;
                            break;
                    }
                    circleRect.X = (base.Width - this.CheckRectWidth) / 2;
                    textRect.X = 2;
                    textRect.Width = base.Width - 4;
                    return;
                }
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        circleRect.Y = 2;
                        break;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        circleRect.Y = (base.Height - this.CheckRectWidth) / 2;
                        break;

                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        circleRect.Y = (base.Height - this.CheckRectWidth) - 2;
                        break;
                }
            }
            else
            {
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        circleRect.Y = 2;
                        break;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        circleRect.Y = (base.Height - this.CheckRectWidth) / 2;
                        break;

                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        circleRect.Y = (base.Height - this.CheckRectWidth) - 2;
                        break;
                }
                circleRect.X = 1;
                textRect = new Rectangle(circleRect.Right + 2, 0, (base.Width - circleRect.Right) - 4, base.Height);
                return;
            }
            circleRect.X = (base.Width - this.CheckRectWidth) - 1;
            textRect = new Rectangle(2, 0, (base.Width - this.CheckRectWidth) - 6, base.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._dotImg != null)
                {
                    this._dotImg.Dispose();
                }
                if (this._defaultFont != null)
                {
                    this._defaultFont.Dispose();
                }
            }
            this._dotImg = null;
            this._defaultFont = null;
            base.Dispose(disposing);
        }

        private void DrawDisabledCircle(Graphics g, Rectangle circleRect)
        {
            g.DrawEllipse(SystemPens.ControlDark, circleRect);
            if (base.Checked)
            {
                circleRect.Inflate(-2, -2);
                g.DrawImage(this._dotImg, new Rectangle(circleRect.X + 1, circleRect.Y + 1, circleRect.Width - 1, circleRect.Height - 1), 0, 0, this._dotImg.Width, this._dotImg.Height, GraphicsUnit.Pixel);
            }
        }

        private void DrawHighLightCircle(Graphics g, Rectangle circleRect)
        {
            this.DrawNormalCircle(g, circleRect);
            using (Pen pen = new Pen(ColorTable.QQHighLightInnerColor))
            {
                g.DrawEllipse(pen, circleRect);
                circleRect.Inflate(1, 1);
                pen.Color = ColorTable.QQHighLightColor;
                g.DrawEllipse(pen, circleRect);
            }
        }

        private void DrawNormalCircle(Graphics g, Rectangle circleRect)
        {
            g.FillEllipse(Brushes.White, circleRect);
            using (Pen pen = new Pen(ColorTable.QQBorderColor))
            {
                g.DrawEllipse(pen, circleRect);
            }
            if (base.Checked)
            {
                circleRect.Inflate(-2, -2);
                g.DrawImage(this._dotImg, new Rectangle(circleRect.X + 1, circleRect.Y + 1, circleRect.Width - 1, circleRect.Height - 1), 0, 0, this._dotImg.Width, this._dotImg.Height, GraphicsUnit.Pixel);
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
                    this.DrawHighLightCircle(g, rectangle);
                    break;

                case QQControlState.Disabled:
                    this.DrawDisabledCircle(g, rectangle);
                    break;

                default:
                    this.DrawNormalCircle(g, rectangle);
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

        [Description("获取QQRadioButton左边正方形的宽度")]
        protected virtual int CheckRectWidth
        {
            get
            {
                return 12;
            }
        }
    }
}

