namespace WinFramwwork.Messagebox
{
    partial class AutoMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbxClose = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer_In = new System.Windows.Forms.Timer(this.components);
            this.timer_out = new System.Windows.Forms.Timer(this.components);
            this.timer_Show = new System.Windows.Forms.Timer(this.components);
            this.lab_Message = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_ShowTome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxClose)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxClose
            // 
            this.pbxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxClose.BackColor = System.Drawing.Color.Teal;
            this.pbxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxClose.Image = global::WinFramwwork.Properties.Resources.close_normal;
            this.pbxClose.Location = new System.Drawing.Point(225, 10);
            this.pbxClose.MaximumSize = new System.Drawing.Size(39, 20);
            this.pbxClose.MinimumSize = new System.Drawing.Size(39, 20);
            this.pbxClose.Name = "pbxClose";
            this.pbxClose.Size = new System.Drawing.Size(39, 20);
            this.pbxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxClose.TabIndex = 3;
            this.pbxClose.TabStop = false;
            this.pbxClose.Click += new System.EventHandler(this.pbxClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.pbxClose);
            this.panel1.Location = new System.Drawing.Point(-3, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 41);
            this.panel1.TabIndex = 4;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseMove);
            // 
            // timer_In
            // 
            this.timer_In.Interval = 10;
            this.timer_In.Tick += new System.EventHandler(this.timer_In_Tick);
            // 
            // timer_out
            // 
            this.timer_out.Interval = 10;
            this.timer_out.Tick += new System.EventHandler(this.timer_out_Tick);
            // 
            // timer_Show
            // 
            this.timer_Show.Interval = 1000;
            this.timer_Show.Tick += new System.EventHandler(this.timer_Show_Tick);
            // 
            // lab_Message
            // 
            this.lab_Message.BackColor = System.Drawing.Color.Transparent;
            this.lab_Message.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Message.Location = new System.Drawing.Point(12, 41);
            this.lab_Message.Name = "lab_Message";
            this.lab_Message.Size = new System.Drawing.Size(260, 190);
            this.lab_Message.TabIndex = 5;
            this.lab_Message.Text = "label1";
            this.lab_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "倒计时";
            // 
            // lab_ShowTome
            // 
            this.lab_ShowTome.AutoSize = true;
            this.lab_ShowTome.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ShowTome.ForeColor = System.Drawing.Color.Red;
            this.lab_ShowTome.Location = new System.Drawing.Point(149, 240);
            this.lab_ShowTome.Name = "lab_ShowTome";
            this.lab_ShowTome.Size = new System.Drawing.Size(19, 12);
            this.lab_ShowTome.TabIndex = 7;
            this.lab_ShowTome.Text = "10";
            this.lab_ShowTome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "秒";
            // 
            // AutoMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WinFramwwork.Properties.Resources.dialog_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lab_ShowTome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lab_Message);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AutoMessage";
            this.ShowInTaskbar = false;
            this.Text = "AutoMessage";
            this.Load += new System.EventHandler(this.AutoMessage_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbxClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer_In;
        private System.Windows.Forms.Timer timer_out;
        private System.Windows.Forms.Timer timer_Show;
        private System.Windows.Forms.Label lab_Message;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_ShowTome;
        private System.Windows.Forms.Label label3;
    }
}