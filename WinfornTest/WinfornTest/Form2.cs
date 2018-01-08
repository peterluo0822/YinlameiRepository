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
    public partial class Form2 : Form
    {
        public event Action<string> OnTextChange;

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (OnTextChange != null)
            {
                OnTextChange(textBox1.Text);
            }
        }
    }
}
