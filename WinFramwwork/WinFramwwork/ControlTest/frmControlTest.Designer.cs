namespace WinFramwwork.ControlTest
{
    partial class frmControlTest
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
            this.dockManager1 = new CodeGenerator.Client.WinFormsUI.Docking.DockManager(this.components);
            this.dockContainer1 = new CodeGenerator.Client.WinFormsUI.Docking.DockContainer(this.components);
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.AllowUnDock = false;
            this.dockManager1.BackColor = System.Drawing.SystemColors.Control;
            this.dockManager1.DockType = CodeGenerator.Client.WinFormsUI.Docking.DockContainerType.Document;
            this.dockManager1.Location = new System.Drawing.Point(0, 0);
            this.dockManager1.Name = "dockManager1";
            this.dockManager1.Padding = new System.Windows.Forms.Padding(2, 23, 2, 2);
            this.dockManager1.ShowIcons = false;
            this.dockManager1.Size = new System.Drawing.Size(810, 209);
            this.dockManager1.TabIndex = 2;
            // 
            // dockContainer1
            // 
            this.dockContainer1.AllowUnDock = false;
            this.dockContainer1.BackColor = System.Drawing.Color.Transparent;
            this.dockContainer1.DockType = CodeGenerator.Client.WinFormsUI.Docking.DockContainerType.None;
            this.dockContainer1.Location = new System.Drawing.Point(12, 225);
            this.dockContainer1.Name = "dockContainer1";
            this.dockContainer1.Size = new System.Drawing.Size(786, 195);
            this.dockContainer1.TabIndex = 3;
            // 
            // frmControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 432);
            this.Controls.Add(this.dockContainer1);
            this.Controls.Add(this.dockManager1);
            this.Name = "frmControlTest";
            this.Opacity = 1D;
            this.Text = "frmControlTest";
            this.Load += new System.EventHandler(this.frmControlTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Component1 component11;
        private CodeGenerator.Client.WinFormsUI.Docking.DockManager dockManager1;
        private CodeGenerator.Client.WinFormsUI.Docking.DockContainer dockContainer1;
    }
}