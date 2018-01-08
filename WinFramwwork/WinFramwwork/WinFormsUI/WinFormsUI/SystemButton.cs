namespace WinFormsUI
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class SystemButton
    {
        public event MouseDownEventHandler OnMouseDownEvent;

        public void OnMouseDown()
        {
            if (this.OnMouseDownEvent != null)
            {
                this.OnMouseDownEvent();
            }
        }

        public Image DownImg { get; set; }

        public Image HighLightImg { get; set; }

        public Rectangle LocationRect { get; set; }

        public Image NormalImg { get; set; }

        public SystemButtonState State { get; set; }

        public string ToolTip { get; set; }
    }
}

