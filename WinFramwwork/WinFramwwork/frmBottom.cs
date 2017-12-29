using FineEx.Control.Forms;

namespace MainFrame
{
    public partial class frmBottom : BaseForm
    {
        public frmBottom()
        {
            InitializeComponent();
        }


        public void Init(string name)
        {
            richTextBox1.Text = name;
        }
      

    }
}
