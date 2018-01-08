namespace WinFormsUI
{
    using System;
    using System.Windows.Forms;

    public class FormBase : Form
    {
        private System.Windows.Forms.ToolTip _toolTip = new System.Windows.Forms.ToolTip();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._toolTip.Dispose();
            }
            this._toolTip = null;
        }

        public System.Windows.Forms.ToolTip ToolTip
        {
            get
            {
                return this._toolTip;
            }
        }
    }
}

