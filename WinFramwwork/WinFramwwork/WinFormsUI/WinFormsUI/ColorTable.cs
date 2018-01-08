namespace WinFormsUI
{
    using System;
    using System.Drawing;

    internal class ColorTable
    {
        public static Color QQBorderColor = Color.LightBlue;
        public static Color QQHighLightColor = RenderHelper.GetColor(QQBorderColor, 0xff, -63, -11, 0x17);
        public static Color QQHighLightInnerColor = RenderHelper.GetColor(QQBorderColor, 0xff, -100, -44, 1);
    }
}

