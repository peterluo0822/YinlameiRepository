using System.Windows.Forms;

namespace FineEx.Control.Forms
{
    public partial class StateForm : Form
    {
        public StateForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ChangeMessage(string message)
        {
            ShowMessage(message);

        }

        /// <summary>
        /// 显示信息
        /// </summary>
        public void ShowMessage()
        {
            ShowMessage(@"系统正在处理中...请稍候！");
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            label1.Text = message;
            label1.Refresh();

            try
            {
                if (IsDisposed)
                {

                }

                //base.Show();
                this.Visible = true;
                
                Application.DoEvents();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 关闭信息
        /// </summary>
        public void CloseMessage()
        {
            this.Close();
        }

    }
}
