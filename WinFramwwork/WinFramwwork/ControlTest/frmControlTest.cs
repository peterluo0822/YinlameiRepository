using CodeGenerator.Client.WinFormsUI.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFramwwork.ParentForm;
using WinFramwwork.test;

namespace WinFramwwork.ControlTest
{
    public partial class frmControlTest : DockWindow
    {
        public frmControlTest()
        {
            InitializeComponent();
        }

        private void frmControlTest_Load(object sender, EventArgs e)
        {
            ShowNewForm(null,null);
        }
        private int childFormNumber = 0;
        private void ShowNewForm(object sender, EventArgs e)
        {
            frmTest2 frm = new frmTest2();
            dockManager1.DockWindow(frm, DockStyle.Fill);

            // dockManager1.Controls.Add(frm.ControlContainer);


            //DockPanel test = new DockPanel();
            //test.Form = frm;
            //dockManager1.Controls.Add(test);

        }
    }
}
