using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFramwwork.test
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            dockManager1.DockWindow(new frmTest1(), DockStyle.Fill);
            dockManager1.DockWindow(new frmTest2(), DockStyle.Fill);            
        }
    }
}
