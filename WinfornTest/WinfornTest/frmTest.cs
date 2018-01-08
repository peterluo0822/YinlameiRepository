using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinfornTest
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void uC_Test1_OnRemove(UC_Test.Student obj)
        {
            MessageBox.Show(string.Format("Name={0},StuNo={1}", obj.Name, obj.StuNo));
            base.Controls.Remove(obj.Parent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Messagebox.FlyMessagebox.ShowAuto(3, "hello world!");
        }

        private void uC_Test1_Onxx(string obj)
        {
            MessageBox.Show(obj);
        }
    }
}
