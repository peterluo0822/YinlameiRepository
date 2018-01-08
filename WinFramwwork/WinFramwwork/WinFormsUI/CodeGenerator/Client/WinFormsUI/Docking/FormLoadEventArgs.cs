namespace CodeGenerator.Client.WinFormsUI.Docking
{
    using System;

    public class FormLoadEventArgs : EventArgs
    {
        private bool cancel = false;
        private DockWindow form = null;
        private System.Type type = null;

        public FormLoadEventArgs(System.Type type)
        {
            this.form = null;
            this.type = type;
            this.cancel = false;
        }

        public bool Cancel
        {
            get
            {
                return this.cancel;
            }
            set
            {
                this.cancel = value;
            }
        }

        public DockWindow Form
        {
            get
            {
                return this.form;
            }
            set
            {
                this.form = value;
            }
        }

        public System.Type Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

