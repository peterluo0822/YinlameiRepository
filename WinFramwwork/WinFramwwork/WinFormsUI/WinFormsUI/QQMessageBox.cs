namespace WinFormsUI
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class QQMessageBox : FormEx
    {
        private Image _msgBoxIcon;

        public QQMessageBox(string msgText, string caption, QQMessageBoxIcon msgBoxIcon, QQMessageBoxButtons msgBoxButtons)
        {
            this.MessageText = msgText;
            this.Text = caption;
            this.LoadMsgBoxIcon(msgBoxIcon);
            this.LoadMsgBoxButtons(msgBoxButtons);
            this.InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void CreateOKButton()
        {
            QQButton button = new QQButton();
            base.SuspendLayout();
            button.Font = new Font("微软雅黑", 9f);
            button.Location = new Point(260, 0x7d);
            button.Name = "OKBtn";
            button.Size = new Size(0x44, 0x17);
            button.TabIndex = 0;
            button.Text = "确定";
            button.UseVisualStyleBackColor = true;
            button.Click += new EventHandler(this.OKBtn_Click);
            base.Controls.Add(button);
            base.AcceptButton = button;
            base.ResumeLayout();
        }

        private void CreateOKCancelButton()
        {
            QQButton button = new QQButton();
            QQButton button2 = new QQButton();
            base.SuspendLayout();
            button.Font = new Font("微软雅黑", 9f);
            button.Location = new Point(0xb3, 0x7d);
            button.Name = "OKBtn";
            button.Size = new Size(0x44, 0x17);
            button.TabIndex = 0;
            button.Text = "确定";
            button.UseVisualStyleBackColor = true;
            button.Click += new EventHandler(this.OKBtn_Click);
            button2.Font = new Font("微软雅黑", 9f);
            button2.Location = new Point(260, 0x7d);
            button2.Name = "cancleBtn";
            button2.Size = new Size(0x44, 0x17);
            button2.TabIndex = 1;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(this.CancelBtn_Click);
            base.Controls.Add(button);
            base.Controls.Add(button2);
            base.AcceptButton = button;
            base.ResumeLayout();
        }

        private void CreateRetryCancleButton()
        {
            QQButton button = new QQButton();
            QQButton button2 = new QQButton();
            base.SuspendLayout();
            button.Font = new Font("微软雅黑", 9f);
            button.Location = new Point(0xb3, 0x7d);
            button.Name = "retryBtn";
            button.Size = new Size(0x44, 0x17);
            button.TabIndex = 0;
            button.Text = "重试";
            button.UseVisualStyleBackColor = true;
            button.Click += new EventHandler(this.RetryBtn_Click);
            button2.Font = new Font("微软雅黑", 9f);
            button2.Location = new Point(260, 0x7d);
            button2.Name = "cancleBtn";
            button2.Size = new Size(0x44, 0x17);
            button2.TabIndex = 1;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(this.CancelBtn_Click);
            base.Controls.Add(button);
            base.Controls.Add(button2);
            base.AcceptButton = button;
            base.ResumeLayout();
        }

        public void DrawAlphaPart(Form form, Graphics g)
        {
            Color[] colorArray = new Color[] { Color.FromArgb(0, Color.White), Color.FromArgb(0xe1, Color.White), Color.FromArgb(240, Color.White) };
            float[] numArray2 = new float[3];
            numArray2[1] = 0.38f;
            numArray2[2] = 1f;
            float[] numArray = numArray2;
            ColorBlend blend = new ColorBlend(3) {
                Colors = colorArray,
                Positions = numArray
            };
            int num = 0x23;
            RectangleF rect = new RectangleF(0f, 0f, (float) form.Width, (float) (form.Height - num));
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Vertical))
            {
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            using (Pen pen = new Pen(Color.FromArgb(0xff, Color.White), 0.1f))
            {
                g.DrawLine(pen, new Point(0, form.Height - num), new Point(form.Width, form.Height - num));
            }
            using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(0xcd, Color.White)))
            {
                g.FillRectangle(brush2, new Rectangle(0, (form.Height - num) + 1, form.Width, (form.Height - num) + 1));
            }
        }

        private void DrawMessageText(Graphics g)
        {
            using (StringFormat format = new StringFormat())
            {
                format.FormatFlags = StringFormatFlags.NoClip;
                format.Trimming = StringTrimming.EllipsisWord;
                using (Font font = new Font("微软雅黑", 9f))
                {
                    g.DrawString(this.MessageText, font, Brushes.Black, this.MessageTextRect, format);
                }
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.CanResize = false;
            base.ClientSize = new Size(0x153, 0x9a);
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "QQMessageBox";
            base.ShowInTaskbar = false;
            base.ResumeLayout(false);
        }

        private void LoadMsgBoxButtons(QQMessageBoxButtons msgBoxButtons)
        {
            switch (msgBoxButtons)
            {
                case QQMessageBoxButtons.OK:
                    this.CreateOKButton();
                    break;

                case QQMessageBoxButtons.OKCancel:
                    this.CreateOKCancelButton();
                    break;

                case QQMessageBoxButtons.RetryCancel:
                    this.CreateRetryCancleButton();
                    break;
            }
        }

        private void LoadMsgBoxIcon(QQMessageBoxIcon msgBoxIcon)
        {
            switch (msgBoxIcon)
            {
                case QQMessageBoxIcon.Error:
                    this._msgBoxIcon = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQMessageBox.Icons.qqmessagebox_error.png");
                    break;

                case QQMessageBoxIcon.Information:
                    this._msgBoxIcon = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQMessageBox.Icons.qqmessagebox_infor.png");
                    break;

                case QQMessageBoxIcon.OK:
                    this._msgBoxIcon = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQMessageBox.Icons.qqmessagebox_ok.png");
                    break;

                case QQMessageBoxIcon.Question:
                    this._msgBoxIcon = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQMessageBox.Icons.qqmessagebox_question.png");
                    break;

                case QQMessageBoxIcon.Warning:
                    this._msgBoxIcon = RenderHelper.GetImageFormResourceStream("ControlExs.QQControls.QQMessageBox.Icons.qqmessagebox_warning.png");
                    break;
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (MsgBoxBackgroundImg != null)
            {
                if ((MsgBoxBackgroundImg.Width > base.ClientSize.Width) && (MsgBoxBackgroundImg.Height > base.ClientSize.Height))
                {
                    e.Graphics.DrawImage(MsgBoxBackgroundImg, base.ClientRectangle, new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height), GraphicsUnit.Pixel);
                }
                else
                {
                    e.Graphics.DrawImage(MsgBoxBackgroundImg, base.ClientRectangle, new Rectangle(0, 0, MsgBoxBackgroundImg.Width, MsgBoxBackgroundImg.Height), GraphicsUnit.Pixel);
                }
            }
            this.DrawAlphaPart(this, e.Graphics);
            e.Graphics.DrawImage(this._msgBoxIcon, new Rectangle(30, 0x30, 40, 40));
            this.DrawMessageText(e.Graphics);
            base.OnPaint(e);
        }

        private void RetryBtn_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Retry;
            base.Close();
        }

        public static DialogResult Show(Form owner = null, string msgText = "请输入提示信息", string caption = "提示", QQMessageBoxIcon msgBoxIcon = QQMessageBoxIcon.Error, QQMessageBoxButtons msgBoxButtons = 0)
        {
            using (QQMessageBox box = new QQMessageBox(msgText, caption, msgBoxIcon, msgBoxButtons))
            {
                if (owner != null)
                {
                    box.StartPosition = FormStartPosition.CenterParent;
                    if (owner.BackgroundImage != null)
                    {
                        MsgBoxBackgroundImg = owner.BackgroundImage;
                    }
                    if (owner.Icon != null)
                    {
                        box.Icon = owner.Icon;
                    }
                }
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                }
                box.ShowDialog();
                return box.DialogResult;
            }
        }

        public string MessageText { get; set; }

        internal Rectangle MessageTextRect
        {
            get
            {
                return new Rectangle(0x54, 0x37, 220, 50);
            }
        }

        [CompilerGenerated]
        private static Image k__BackingField;


        internal static Image MsgBoxBackgroundImg
        {
            [CompilerGenerated]
            get
            {
                return k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                k__BackingField = value;
            }
        }
    }
}

