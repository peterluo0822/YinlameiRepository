using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinfornTest
{
    public partial class UC_Test : UserControl
    {
        public struct Student
        {
            public string Name { get; set; }
            public string StuNo { get; set; }
            public UC_Test Parent { get; set; }
        }
        public event Action<Student> OnRemove;

        public event Action<string> Onxx;

        public UC_Test()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(OnRemove!=null)
            {
                OnRemove(new Student(){Name=Txt_Name.Text,StuNo=txt_StuNo.Text,Parent=this});
            }
        }

        private void Txt_Name_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Name.Text.Contains("hello"))
            {
                Onxx(Txt_Name.Text);
            }
        }
    }
}
