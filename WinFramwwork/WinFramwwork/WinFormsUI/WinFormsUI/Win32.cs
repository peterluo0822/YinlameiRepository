namespace WinFormsUI
{
    using System;
    using System.Runtime.InteropServices;

    public class Win32
    {
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 0x11;
        public const int HTCAPTION = 2;
        public const int HTCLIENT = 1;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int WM_ACTIVATE = 6;
        public const int WM_CREATE = 1;
        public const int WM_CTLCOLOREDIT = 0x133;
        public const int WM_ERASEBKGND = 20;
        public const int WM_FALSE = 0;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCMOUSEMOVE = 160;
        public const int WM_NCPAINT = 0x85;
        public const int WM_PAINT = 15;
        public const int WM_TRUE = 1;
        public const int WM_WINDOWPOSCHANGING = 70;

        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);
        [DllImport("gdi32.dll", CharSet=CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, bool bRedraw);
    }
}

