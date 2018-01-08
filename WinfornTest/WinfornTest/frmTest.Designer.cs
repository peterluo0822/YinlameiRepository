namespace WinfornTest
{
    partial class frmTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.uvcvcv1 = new WinfornTest.Uvcvcv();
            this.uC_Test2 = new WinfornTest.UC_Test();
            this.uC_Test1 = new WinfornTest.UC_Test();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(513, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "messagebox";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // uvcvcv1
            // 
            this.uvcvcv1.Location = new System.Drawing.Point(80, 137);
            this.uvcvcv1.Name = "uvcvcv1";
            this.uvcvcv1.Size = new System.Drawing.Size(394, 117);
            this.uvcvcv1.TabIndex = 3;
            // 
            // uC_Test2
            // 
            this.uC_Test2.Location = new System.Drawing.Point(41, 92);
            this.uC_Test2.Name = "uC_Test2";
            this.uC_Test2.Size = new System.Drawing.Size(378, 26);
            this.uC_Test2.TabIndex = 1;
            this.uC_Test2.OnRemove += new System.Action<WinfornTest.UC_Test.Student>(this.uC_Test1_OnRemove);
            // 
            // uC_Test1
            // 
            this.uC_Test1.Location = new System.Drawing.Point(41, 32);
            this.uC_Test1.Name = "uC_Test1";
            this.uC_Test1.Size = new System.Drawing.Size(378, 26);
            this.uC_Test1.TabIndex = 0;
            this.uC_Test1.OnRemove += new System.Action<WinfornTest.UC_Test.Student>(this.uC_Test1_OnRemove);
            this.uC_Test1.Onxx += new System.Action<string>(this.uC_Test1_Onxx);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 305);
            this.Controls.Add(this.uvcvcv1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uC_Test2);
            this.Controls.Add(this.uC_Test1);
            this.Name = "frmTest";
            this.Text = "frmTest";
            this.ResumeLayout(false);

        }

        #endregion

        private UC_Test uC_Test1;
        private UC_Test uC_Test2;
        private System.Windows.Forms.Button button1;
        private Uvcvcv uvcvcv1;
    }
}