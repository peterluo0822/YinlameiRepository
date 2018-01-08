namespace WinFormsUI
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    internal class SystemButtonManager : IDisposable
    {
        private bool _mouseDown;
        private FormBase _owner;
        private SystemButton[] _systemButtons = new SystemButton[3];

        public SystemButtonManager(FormBase owner)
        {
            this._owner = owner;
            this.IniSystemButtons();
        }

        private void CloseButtonEvent()
        {
            this._owner.Close();
        }

        public void Dispose()
        {
            this._owner = null;
        }

        public void DrawSystemButtons(Graphics g)
        {
            for (int i = 0; i < this.SystemButtonArray.Length; i++)
            {
                if (this.SystemButtonArray[i].LocationRect != Rectangle.Empty)
                {
                    switch (this[i])
                    {
                        case SystemButtonState.Normal:
                        case SystemButtonState.DownLeave:
                            g.DrawImage(this.SystemButtonArray[i].NormalImg, this.SystemButtonArray[i].LocationRect, new Rectangle(0, 0, this.SystemButtonArray[i].NormalImg.Width, this.SystemButtonArray[i].NormalImg.Height), GraphicsUnit.Pixel);
                            break;

                        case SystemButtonState.HighLight:
                            g.DrawImage(this.SystemButtonArray[i].HighLightImg, this.SystemButtonArray[i].LocationRect, new Rectangle(0, 0, this.SystemButtonArray[i].HighLightImg.Width, this.SystemButtonArray[i].HighLightImg.Height), GraphicsUnit.Pixel);
                            break;

                        case SystemButtonState.Down:
                            g.DrawImage(this.SystemButtonArray[i].DownImg, this.SystemButtonArray[i].LocationRect, new Rectangle(0, 0, this.SystemButtonArray[i].DownImg.Width, this.SystemButtonArray[i].DownImg.Height), GraphicsUnit.Pixel);
                            break;
                    }
                }
            }
        }

        private void HideToolTip()
        {
            if (this._owner != null)
            {
                this._owner.ToolTip.Active = false;
            }
        }

        private void IniSystemButtons()
        {
            bool maximizeBox = this._owner.MaximizeBox;
            bool minimizeBox = this._owner.MinimizeBox;
            SystemButton button = new SystemButton();
            this.SystemButtonArray[0] = button;
            button.ToolTip = "关闭";
            button.NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.close_normal.png");
            button.HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.close_highlight.png");
            button.DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.close_down.png");
            button.LocationRect = new Rectangle(this._owner.Width - button.NormalImg.Width, -1, button.NormalImg.Width, button.NormalImg.Height);
            button.OnMouseDownEvent += new MouseDownEventHandler(this.CloseButtonEvent);
            SystemButton button2 = new SystemButton();
            this.SystemButtonArray[1] = button2;
            button2.ToolTip = "最大化";
            if (maximizeBox)
            {
                button2.NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_normal.png");
                button2.HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_highlight.png");
                button2.DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.max_down.png");
                button2.OnMouseDownEvent += new MouseDownEventHandler(this.MaxButtonEvent);
                button2.LocationRect = new Rectangle(button.LocationRect.X - button2.NormalImg.Width, -1, button2.NormalImg.Width, button2.NormalImg.Height);
            }
            else
            {
                button2.LocationRect = Rectangle.Empty;
            }
            SystemButton button3 = new SystemButton();
            this.SystemButtonArray[2] = button3;
            button3.ToolTip = "最小化";
            if (!minimizeBox)
            {
                button3.LocationRect = Rectangle.Empty;
            }
            else
            {
                button3.NormalImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.min_normal.png");
                button3.HighLightImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.min_highlight.png");
                button3.DownImg = RenderHelper.GetImageFormResourceStream("ControlExs.FormEx.Res.SystemButtons.min_down.png");
                button3.OnMouseDownEvent += new MouseDownEventHandler(this.MinButtonEvent);
                if (maximizeBox)
                {
                    button3.LocationRect = new Rectangle(button2.LocationRect.X - button3.NormalImg.Width, -1, button3.NormalImg.Width, button3.NormalImg.Height);
                }
                else
                {
                    button3.LocationRect = new Rectangle(button.LocationRect.X - button3.NormalImg.Width, -1, button3.NormalImg.Width, button3.NormalImg.Height);
                }
            }
        }

        private void Invalidate(Rectangle rect)
        {
            this._owner.Invalidate(rect);
        }

        private void MaxButtonEvent()
        {
            if (this._owner.WindowState == FormWindowState.Maximized)
            {
                this._owner.WindowState = FormWindowState.Normal;
            }
            else
            {
                this._owner.WindowState = FormWindowState.Maximized;
            }
        }

        private void MinButtonEvent()
        {
            this._owner.WindowState = FormWindowState.Minimized;
        }

        private void ProcessMouseDown(Point mousePoint)
        {
            int num = this.SearchPointInRects(mousePoint);
            if (num != -1)
            {
                this._mouseDown = true;
                this[num] = SystemButtonState.Down;
            }
            else
            {
                RenderHelper.MoveWindow(this._owner);
            }
        }

        private void ProcessMouseLeave()
        {
            for (int i = 0; i < this.SystemButtonArray.Length; i++)
            {
                if (this[i] == SystemButtonState.Down)
                {
                    this[i] = SystemButtonState.DownLeave;
                }
                else
                {
                    this[i] = SystemButtonState.Normal;
                }
            }
        }

        private void ProcessMouseMove(Point mousePoint)
        {
            int num2;
            string toolTipText = string.Empty;
            bool flag = true;
            int index = this.SearchPointInRects(mousePoint);
            if (index != -1)
            {
                flag = false;
                if (!this._mouseDown)
                {
                    if (this[index] != SystemButtonState.HighLight)
                    {
                        toolTipText = this.SystemButtonArray[index].ToolTip;
                    }
                    this[index] = SystemButtonState.HighLight;
                }
                else if (this[index] == SystemButtonState.DownLeave)
                {
                    this[index] = SystemButtonState.Down;
                }
                for (num2 = 0; num2 < this.SystemButtonArray.Length; num2++)
                {
                    if (num2 != index)
                    {
                        this[num2] = SystemButtonState.Normal;
                    }
                }
            }
            else
            {
                for (num2 = 0; num2 < this.SystemButtonArray.Length; num2++)
                {
                    if (!this._mouseDown)
                    {
                        this[num2] = SystemButtonState.Normal;
                    }
                    else if (this[num2] == SystemButtonState.Down)
                    {
                        this[num2] = SystemButtonState.DownLeave;
                    }
                }
            }
            if (toolTipText != string.Empty)
            {
                this.HideToolTip();
                this.ShowTooTip(toolTipText);
            }
            if (flag)
            {
                this.HideToolTip();
            }
        }

        public void ProcessMouseOperate(Point mousePoint, MouseOperate operate)
        {
            switch (operate)
            {
                case MouseOperate.Move:
                    this.ProcessMouseMove(mousePoint);
                    break;

                case MouseOperate.Down:
                    this.ProcessMouseDown(mousePoint);
                    break;

                case MouseOperate.Up:
                    this.ProcessMouseUP(mousePoint);
                    break;

                case MouseOperate.Leave:
                    this.ProcessMouseLeave();
                    break;
            }
        }

        private void ProcessMouseUP(Point mousePoint)
        {
            this._mouseDown = false;
            int index = this.SearchPointInRects(mousePoint);
            if (index != -1)
            {
                if (this[index] == SystemButtonState.Down)
                {
                    this[index] = SystemButtonState.Normal;
                    this.SystemButtonArray[index].OnMouseDown();
                }
            }
            else
            {
                this.ProcessMouseLeave();
            }
        }

        private int SearchPointInRects(Point mousePoint)
        {
            bool flag = false;
            int num = 0;
            foreach (SystemButton button in this.SystemButtonArray)
            {
                if ((button.LocationRect != Rectangle.Empty) && button.LocationRect.Contains(mousePoint))
                {
                    flag = true;
                    break;
                }
                num++;
            }
            if (flag)
            {
                return num;
            }
            return -1;
        }

        private void ShowTooTip(string toolTipText)
        {
            if (this._owner != null)
            {
                this._owner.ToolTip.Active = true;
                this._owner.ToolTip.SetToolTip(this._owner, toolTipText);
            }
        }

        public SystemButtonState this[int buttonID]
        {
            get
            {
                return this.SystemButtonArray[buttonID].State;
            }
            set
            {
                if (this.SystemButtonArray[buttonID].State != value)
                {
                    this.SystemButtonArray[buttonID].State = value;
                    if (this._owner != null)
                    {
                        this.Invalidate(this.SystemButtonArray[buttonID].LocationRect);
                    }
                }
            }
        }

        public SystemButton[] SystemButtonArray
        {
            get
            {
                return this._systemButtons;
            }
        }
    }
}

