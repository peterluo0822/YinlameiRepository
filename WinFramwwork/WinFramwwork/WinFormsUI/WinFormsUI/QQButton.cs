namespace WinFormsUI
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class QQButton : Button
    {
        private Font _defaultFont = new Font("微软雅黑", 9f);
        private Image _downImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQButton.Image.qqbtn_down.png");
        private Image _focusImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQButton.Image.qqbtn_focus.png");
        private Image _highlightImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQButton.Image.qqbtn_highlight.png");
        private Image _normalImg = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQButton.Image.qqbtn_normal.png");
        private QQControlState _state = QQControlState.Normal;

        public QQButton()
        {
            this.SetStyles();
            this.Font = this._defaultFont;
            base.Size = new Size(0x44, 0x17);
        }

        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect)
        {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (base.Image == null)
            {
                textRect = new Rectangle(3, 0, base.Width - 6, base.Height);
            }
            else
            {
                switch (base.TextImageRelation)
                {
                    case TextImageRelation.Overlay:
                        imageRect = new Rectangle(3, (base.Height - this.ImageWidth) / 2, this.ImageWidth, this.ImageWidth);
                        textRect = new Rectangle(3, 0, base.Width - 6, base.Height);
                        break;

                    case TextImageRelation.ImageAboveText:
                        imageRect = new Rectangle((base.Width - this.ImageWidth) / 2, 3, this.ImageWidth, this.ImageWidth);
                        textRect = new Rectangle(3, imageRect.Bottom, base.Width - 6, (base.Height - imageRect.Bottom) - 2);
                        break;

                    case TextImageRelation.TextAboveImage:
                        imageRect = new Rectangle((base.Width - this.ImageWidth) / 2, (base.Height - this.ImageWidth) - 3, this.ImageWidth, this.ImageWidth);
                        textRect = new Rectangle(0, 3, base.Width, (base.Height - imageRect.Y) - 3);
                        break;

                    case TextImageRelation.ImageBeforeText:
                        imageRect = new Rectangle(3, (base.Height - this.ImageWidth) / 2, this.ImageWidth, this.ImageWidth);
                        textRect = new Rectangle(imageRect.Right + 3, 0, (base.Width - imageRect.Right) - 6, base.Height);
                        break;

                    case TextImageRelation.TextBeforeImage:
                        imageRect = new Rectangle((base.Width - this.ImageWidth) - 6, (base.Height - this.ImageWidth) / 2, this.ImageWidth, this.ImageWidth);
                        textRect = new Rectangle(3, 0, imageRect.X - 3, base.Height);
                        break;
                }
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    imageRect.X = base.Width - imageRect.Right;
                    textRect.X = base.Width - textRect.Right;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._normalImg != null)
                {
                    this._normalImg.Dispose();
                }
                if (this._highlightImg != null)
                {
                    this._highlightImg.Dispose();
                }
                if (this._downImg != null)
                {
                    this._downImg.Dispose();
                }
                if (this._focusImg != null)
                {
                    this._focusImg.Dispose();
                }
                if (this._defaultFont != null)
                {
                    this._defaultFont.Dispose();
                }
            }
            this._normalImg = null;
            this._highlightImg = null;
            this._focusImg = null;
            this._downImg = null;
            this._defaultFont = null;
            base.Dispose(disposing);
        }

        private void DrawDisabledButton(Graphics g)
        {
            int radius = 4;
            using (GraphicsPath path = RenderHelper.CreateRoundPath(new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width, base.ClientRectangle.Height - 1), radius))
            {
                using (Pen pen = new Pen(Color.FromArgb(0x9c, 0xa5, 0xb1)))
                {
                    g.DrawPath(pen, path);
                }
                Rectangle rect = new Rectangle(base.ClientRectangle.X + 1, base.ClientRectangle.Y + 1, base.ClientRectangle.Width - 2, (base.ClientRectangle.Height - 2) - 1);
                using (GraphicsPath path2 = RenderHelper.CreateRoundPath(rect, radius))
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0xf7, 0xfc, 0xfe), Color.FromArgb(230, 240, 0xf3), LinearGradientMode.Vertical))
                    {
                        g.FillPath(brush, path2);
                    }
                }
            }
        }

        internal static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft)
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

        protected override void OnLostFocus(EventArgs e)
        {
            this._state = QQControlState.Normal;
            base.OnLostFocus(e);
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
            if ((this._state == QQControlState.Highlight) && this.Focused)
            {
                this._state = QQControlState.Focus;
            }
            else if (this._state == QQControlState.Focus)
            {
                this._state = QQControlState.Focus;
            }
            else
            {
                this._state = QQControlState.Normal;
            }
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
                    this._state = QQControlState.Focus;
                }
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle rectangle;
            Rectangle rectangle2;
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            this.CalculateRect(out rectangle, out rectangle2);
            if (!base.Enabled)
            {
                this._state = QQControlState.Disabled;
            }
            switch (this._state)
            {
                case QQControlState.Normal:
                    RenderHelper.DrawImageWithNineRect(g, this._normalImg, base.ClientRectangle, new Rectangle(0, 0, this._normalImg.Width, this._normalImg.Height));
                    break;

                case QQControlState.Highlight:
                    RenderHelper.DrawImageWithNineRect(g, this._highlightImg, base.ClientRectangle, new Rectangle(0, 0, this._highlightImg.Width, this._highlightImg.Height));
                    break;

                case QQControlState.Down:
                    RenderHelper.DrawImageWithNineRect(g, this._downImg, base.ClientRectangle, new Rectangle(0, 0, this._downImg.Width, this._downImg.Height));
                    break;

                case QQControlState.Focus:
                    RenderHelper.DrawImageWithNineRect(g, this._focusImg, base.ClientRectangle, new Rectangle(0, 0, this._focusImg.Width, this._focusImg.Height));
                    break;

                case QQControlState.Disabled:
                    this.DrawDisabledButton(g);
                    break;
            }
            if (base.Image != null)
            {
                g.DrawImage(base.Image, rectangle, 0, 0, base.Image.Width, base.Image.Height, GraphicsUnit.Pixel);
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

        private int ImageWidth
        {
            get
            {
                if (base.Image == null)
                {
                    return 0x10;
                }
                return base.Image.Width;
            }
        }
    }
}

