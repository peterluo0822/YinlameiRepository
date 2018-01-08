using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFramwwork.ControlTest
{
    public partial class Component1 : Form
    {
        public Component1()
        {
            InitializeComponent();
        }


        public Component1(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
