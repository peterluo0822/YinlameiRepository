using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinFramwwork.Messagebox;

namespace WinFramwwork.test
{
    public partial class frmTest2 : FineEx.Control.Forms.BaseForm
    {
        public frmTest2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlyMessagebox.ShowAuto(3,"保存成功！");
        }
    }
}
