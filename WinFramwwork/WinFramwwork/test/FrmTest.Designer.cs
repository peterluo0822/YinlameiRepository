namespace WinFramwwork.test
{
    partial class FrmTest
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dockManager1 = new CodeGenerator.Client.WinFormsUI.Docking.DockManager(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewEx1 = new CodeGenerator.Client.WinFormsUI.Controls.ListViewEx();
            this.xListView1 = new CodeGenerator.Client.WinFormsUI.Controls.XListView();
            this.flatButton1 = new CodeGenerator.Client.WinFormsUI.Docking.FlatButton(this.components);
            this.qqTextBox1 = new WinFormsUI.QQTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(777, 505);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dockManager1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(769, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dockManager1
            // 
            this.dockManager1.AllowUnDock = false;
            this.dockManager1.BackColor = System.Drawing.Color.Transparent;
            this.dockManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockManager1.DockType = CodeGenerator.Client.WinFormsUI.Docking.DockContainerType.Document;
            this.dockManager1.Location = new System.Drawing.Point(3, 3);
            this.dockManager1.Name = "dockManager1";
            this.dockManager1.Padding = new System.Windows.Forms.Padding(2, 23, 2, 2);
            this.dockManager1.ShowIcons = false;
            this.dockManager1.Size = new System.Drawing.Size(763, 473);
            this.dockManager1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.qqTextBox1);
            this.tabPage2.Controls.Add(this.flatButton1);
            this.tabPage2.Controls.Add(this.xListView1);
            this.tabPage2.Controls.Add(this.listViewEx1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(769, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewEx1
            // 
            this.listViewEx1.Location = new System.Drawing.Point(29, 25);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(121, 97);
            this.listViewEx1.TabIndex = 0;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            // 
            // xListView1
            // 
            this.xListView1.Location = new System.Drawing.Point(391, 65);
            this.xListView1.Name = "xListView1";
            this.xListView1.Size = new System.Drawing.Size(121, 97);
            this.xListView1.TabIndex = 1;
            this.xListView1.UseCompatibleStateImageBehavior = false;
            // 
            // flatButton1
            // 
            this.flatButton1.LightColor = System.Drawing.Color.White;
            this.flatButton1.Location = new System.Drawing.Point(391, 238);
            this.flatButton1.Name = "flatButton1";
            this.flatButton1.SelectColor = System.Drawing.Color.Transparent;
            this.flatButton1.ShadowColor = System.Drawing.Color.Black;
            this.flatButton1.Size = new System.Drawing.Size(75, 23);
            this.flatButton1.TabIndex = 2;
            this.flatButton1.Text = "flatButton1";
            this.flatButton1.UseVisualStyleBackColor = true;
            // 
            // qqTextBox1
            // 
            this.qqTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.qqTextBox1.EmptyTextTip = null;
            this.qqTextBox1.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.qqTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.qqTextBox1.Location = new System.Drawing.Point(107, 265);
            this.qqTextBox1.Name = "qqTextBox1";
            this.qqTextBox1.Size = new System.Drawing.Size(100, 23);
            this.qqTextBox1.TabIndex = 3;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 505);
            this.Controls.Add(this.tabControl1);
            this.IsMdiContainer = true;
            this.Name = "FrmTest";
            this.Text = "FrmTest";
            this.Load += new System.EventHandler(this.FrmTest_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private CodeGenerator.Client.WinFormsUI.Docking.DockManager dockManager1;
        private WinFormsUI.QQTextBox qqTextBox1;
        private CodeGenerator.Client.WinFormsUI.Docking.FlatButton flatButton1;
        private CodeGenerator.Client.WinFormsUI.Controls.XListView xListView1;
        private CodeGenerator.Client.WinFormsUI.Controls.ListViewEx listViewEx1;
    }
}