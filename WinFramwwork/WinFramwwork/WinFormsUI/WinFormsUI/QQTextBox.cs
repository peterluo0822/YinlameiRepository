namespace WinFormsUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class QQTextBox : TextBox
    {
        private Font _defaultFont = new Font("微软雅黑", 9f);
        private string _emptyTextTip;
        private Color _emptyTextTipColor = Color.DarkGray;
        private QQControlState _state = QQControlState.Normal;

        public QQTextBox()
        {
            this.SetStyles();
            this.Font = this._defaultFont;
            base.BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._defaultFont != null))
            {
                this._defaultFont.Dispose();
            }
            this._defaultFont = null;
            base.Dispose(disposing);
        }

        private void DrawDisabledTextBox(Graphics g)
        {
            using (Pen pen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(pen, new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1));
            }
        }

        private void DrawFocusTextBox(Graphics g)
        {
            using (Pen pen = new Pen(ColorTable.QQHighLightInnerColor))
            {
                g.DrawRectangle(pen, new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1));
            }
        }

        private void DrawHighLightTextBox(Graphics g)
        {
            using (Pen pen = new Pen(ColorTable.QQHighLightColor))
            {
                Rectangle rect = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
                g.DrawRectangle(pen, rect);
                rect.Inflate(-1, -1);
                pen.Color = ColorTable.QQHighLightInnerColor;
                g.DrawRectangle(pen, rect);
            }
        }

        private void DrawNormalTextBox(Graphics g)
        {
            using (Pen pen = new Pen(Color.LightGray))
            {
                g.DrawRectangle(pen, new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1));
            }
        }

        private static TextFormatFlags GetTextFormatFlags(HorizontalAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    return (flags | TextFormatFlags.VerticalCenter);

                case HorizontalAlignment.Right:
                    return (flags | (TextFormatFlags.VerticalCenter | TextFormatFlags.Right));

                case HorizontalAlignment.Center:
                    return (flags | (TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter));
            }
            return flags;
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
                this._state = QQControlState.Highlight;
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

        private void SetStyles()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        private void WmPaint(ref Message m)
        {
            Graphics g = Graphics.FromHwnd(base.Handle);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (!base.Enabled)
            {
                this._state = QQControlState.Disabled;
            }
            switch (this._state)
            {
                case QQControlState.Normal:
                    this.DrawNormalTextBox(g);
                    break;

                case QQControlState.Highlight:
                    this.DrawHighLightTextBox(g);
                    break;

                case QQControlState.Focus:
                    this.DrawFocusTextBox(g);
                    break;

                case QQControlState.Disabled:
                    this.DrawDisabledTextBox(g);
                    break;
            }
            if (!(((this.Text.Length != 0) || string.IsNullOrEmpty(this.EmptyTextTip)) || this.Focused))
            {
                TextRenderer.DrawText(g, this.EmptyTextTip, this.Font, base.ClientRectangle, this.EmptyTextTipColor, GetTextFormatFlags(base.TextAlign, this.RightToLeft == RightToLeft.Yes));
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if ((m.Msg == 15) || (m.Msg == 0x133))
            {
                this.WmPaint(ref m);
            }
        }

        [Description("当Text属性为空时编辑框内出现的提示文本")]
        public string EmptyTextTip
        {
            get
            {
                return this._emptyTextTip;
            }
            set
            {
                if (this._emptyTextTip != value)
                {
                    this._emptyTextTip = value;
                    base.Invalidate();
                }
            }
        }

        [Description("获取或设置EmptyTextTip的颜色")]
        public Color EmptyTextTipColor
        {
            get
            {
                return this._emptyTextTipColor;
            }
            set
            {
                if (this._emptyTextTipColor != value)
                {
                    this._emptyTextTipColor = value;
                    base.Invalidate();
                }
            }
        }
    }
}

