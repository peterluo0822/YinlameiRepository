namespace FineEx.Control.Forms
{
    partial class ucToolMenu
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucToolMenu));
            this.imgToolUpDown = new System.Windows.Forms.ImageList(this.components);
            this.imgOther = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSignIn = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbPrit = new System.Windows.Forms.ToolStripButton();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbReset = new System.Windows.Forms.ToolStripButton();
            this.tsbOther1 = new System.Windows.Forms.ToolStripButton();
            this.tsbOther2 = new System.Windows.Forms.ToolStripButton();
            this.tsbOther3 = new System.Windows.Forms.ToolStripButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imgToolUpDown
            // 
            this.imgToolUpDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolUpDown.ImageStream")));
            this.imgToolUpDown.TransparentColor = System.Drawing.Color.Transparent;
            this.imgToolUpDown.Images.SetKeyName(0, "Up.png");
            this.imgToolUpDown.Images.SetKeyName(1, "Down.png");
            // 
            // imgOther
            // 
            this.imgOther.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgOther.ImageStream")));
            this.imgOther.TransparentColor = System.Drawing.Color.Transparent;
            this.imgOther.Images.SetKeyName(0, "自定义1.png");
            this.imgOther.Images.SetKeyName(1, "自定义2.png");
            this.imgOther.Images.SetKeyName(2, "自定义3.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Teal;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSignIn,
            this.tsbHelp,
            this.tsbAdd,
            this.tsbEdit,
            this.tsbDelete,
            this.tsbSave,
            this.tsbExport,
            this.tsbImport,
            this.tsbPrit,
            this.tsbCheck,
            this.tsbReset,
            this.tsbOther1,
            this.tsbOther2,
            this.tsbOther3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(8);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1005, 69);
            this.toolStrip1.TabIndex = 7;
            // 
            // tsbSignIn
            // 
            this.tsbSignIn.ForeColor = System.Drawing.Color.White;
            this.tsbSignIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbSignIn.Image")));
            this.tsbSignIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSignIn.Margin = new System.Windows.Forms.Padding(8);
            this.tsbSignIn.Name = "tsbSignIn";
            this.tsbSignIn.Size = new System.Drawing.Size(36, 53);
            this.tsbSignIn.Text = "退出";
            this.tsbSignIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSignIn.Click += new System.EventHandler(this.tsbSignIn_Click);
            // 
            // tsbHelp
            // 
            this.tsbHelp.ForeColor = System.Drawing.Color.White;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Margin = new System.Windows.Forms.Padding(8, 8, 18, 8);
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(36, 53);
            this.tsbHelp.Text = "帮助";
            this.tsbHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // tsbAdd
            // 
            this.tsbAdd.ForeColor = System.Drawing.Color.White;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Margin = new System.Windows.Forms.Padding(8);
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(36, 53);
            this.tsbAdd.Text = "添加";
            this.tsbAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbEdit
            // 
            this.tsbEdit.ForeColor = System.Drawing.Color.White;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Margin = new System.Windows.Forms.Padding(8);
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(36, 53);
            this.tsbEdit.Text = "编辑";
            this.tsbEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbDelete
            // 
            this.tsbDelete.ForeColor = System.Drawing.Color.White;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Margin = new System.Windows.Forms.Padding(8);
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(36, 53);
            this.tsbDelete.Text = "删除";
            this.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbSave
            // 
            this.tsbSave.ForeColor = System.Drawing.Color.White;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Margin = new System.Windows.Forms.Padding(8);
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(36, 53);
            this.tsbSave.Text = "保存";
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbExport
            // 
            this.tsbExport.ForeColor = System.Drawing.Color.White;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Margin = new System.Windows.Forms.Padding(8);
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(36, 53);
            this.tsbExport.Text = "导出";
            this.tsbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbImport
            // 
            this.tsbImport.ForeColor = System.Drawing.Color.White;
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Margin = new System.Windows.Forms.Padding(8);
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(36, 53);
            this.tsbImport.Text = "导入";
            this.tsbImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbPrit
            // 
            this.tsbPrit.ForeColor = System.Drawing.Color.White;
            this.tsbPrit.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrit.Image")));
            this.tsbPrit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrit.Margin = new System.Windows.Forms.Padding(8);
            this.tsbPrit.Name = "tsbPrit";
            this.tsbPrit.Size = new System.Drawing.Size(36, 53);
            this.tsbPrit.Text = "打印";
            this.tsbPrit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbCheck
            // 
            this.tsbCheck.ForeColor = System.Drawing.Color.White;
            this.tsbCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheck.Image")));
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Margin = new System.Windows.Forms.Padding(8);
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.Size = new System.Drawing.Size(36, 53);
            this.tsbCheck.Text = "重置";
            this.tsbCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbReset
            // 
            this.tsbReset.ForeColor = System.Drawing.Color.White;
            this.tsbReset.Image = ((System.Drawing.Image)(resources.GetObject("tsbReset.Image")));
            this.tsbReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReset.Margin = new System.Windows.Forms.Padding(8);
            this.tsbReset.Name = "tsbReset";
            this.tsbReset.Size = new System.Drawing.Size(36, 53);
            this.tsbReset.Text = "筛选";
            this.tsbReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbOther1
            // 
            this.tsbOther1.ForeColor = System.Drawing.Color.White;
            this.tsbOther1.Image = ((System.Drawing.Image)(resources.GetObject("tsbOther1.Image")));
            this.tsbOther1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOther1.Margin = new System.Windows.Forms.Padding(8);
            this.tsbOther1.Name = "tsbOther1";
            this.tsbOther1.Size = new System.Drawing.Size(36, 53);
            this.tsbOther1.Text = "自定";
            this.tsbOther1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbOther2
            // 
            this.tsbOther2.ForeColor = System.Drawing.Color.White;
            this.tsbOther2.Image = ((System.Drawing.Image)(resources.GetObject("tsbOther2.Image")));
            this.tsbOther2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOther2.Margin = new System.Windows.Forms.Padding(8);
            this.tsbOther2.Name = "tsbOther2";
            this.tsbOther2.Size = new System.Drawing.Size(36, 53);
            this.tsbOther2.Text = "自定";
            this.tsbOther2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbOther3
            // 
            this.tsbOther3.ForeColor = System.Drawing.Color.White;
            this.tsbOther3.Image = ((System.Drawing.Image)(resources.GetObject("tsbOther3.Image")));
            this.tsbOther3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOther3.Margin = new System.Windows.Forms.Padding(8);
            this.tsbOther3.Name = "tsbOther3";
            this.tsbOther3.Size = new System.Drawing.Size(36, 53);
            this.tsbOther3.Text = "自定";
            this.tsbOther3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Teal;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(871, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(134, 67);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Teal;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(106, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(5, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // ucToolMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucToolMenu";
            this.Size = new System.Drawing.Size(1005, 67);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ImageList imgToolUpDown;
        private System.Windows.Forms.ToolStripButton tsbPrit;
        private System.Windows.Forms.ToolStripButton tsbReset;
        private System.Windows.Forms.ToolStripButton tsbSignIn;
        private System.Windows.Forms.ToolStripButton tsbHelp;
 
        private System.Windows.Forms.ToolStripButton tsbOther1;
        private System.Windows.Forms.ToolStripButton tsbOther2;
        private System.Windows.Forms.ToolStripButton tsbOther3;
        private System.Windows.Forms.ImageList imgOther;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
