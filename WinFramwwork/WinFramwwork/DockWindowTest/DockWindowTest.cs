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

namespace WinFramwwork
{
    public partial class DockWindowTest : Form
    {
        public DockWindowTest()
        {
            InitializeComponent();
        }

        private int childFormNumber = 0;
        private void ShowNewForm(object sender, EventArgs e)
        {
            childForm frm = new childForm();
            frm.MdiParent = this;
            frm.Text = "窗口 " + childFormNumber++;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;

            string Name = "child" + childFormNumber.ToString();
            frm.Name = Name;

            ToolStripButton fruitToolStripDropDownButton = new ToolStripButton(Name, null, null, Name);
            fruitToolStripDropDownButton.Click += FruitToolStripDropDownButton_Click;
            fruitToolStripDropDownButton.Image = global::WinFramwwork.Properties.Resources.no;
            fruitToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStrip1.Items.Add(fruitToolStripDropDownButton);


            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowNewForm(null, null);
        }

        private void FruitToolStripDropDownButton_Click(object sender, EventArgs e)
        {
            ToolStripButton ToolStrip =(ToolStripButton)sender;
            if (this.MdiChildren.Length > 0)
            {
                foreach (Form myForm in this.MdiChildren)// 遍历所有子窗体
                    if (ToolStrip.Name.Equals(myForm.Name))
                    {
                        myForm.Close(); //关闭子窗体
                        break;
                    }
            }
            toolStrip1.Items.Remove(ToolStrip);
        }

        private void DockWindowTest_Load(object sender, EventArgs e)
        {
            //frmTest2 frm = new frmTest2();
            //base.Controls.Add(frm.panel1);
        }
    }
}
