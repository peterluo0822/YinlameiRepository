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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.OnTextChange += Frm_OnTextChange;
            frm.ShowDialog();
        }

        private void Frm_OnTextChange(string obj)
        {
            textBox1.Text = obj;
        }
    }
}
