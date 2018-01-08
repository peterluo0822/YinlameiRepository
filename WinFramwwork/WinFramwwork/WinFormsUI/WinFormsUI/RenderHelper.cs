namespace WinFormsUI
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    public class RenderHelper
    {
        public static GraphicsPath CreateRoundPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int num = 1;
            path.AddArc(rect.X, rect.Y, radius, radius, 180f, 90f);
            path.AddArc((rect.Right - radius) - num, rect.Y, radius, radius, 270f, 90f);
            path.AddArc((rect.Right - radius) - num, (rect.Bottom - radius) - num, radius, radius, 0f, 90f);
            path.AddArc(rect.X, (rect.Bottom - radius) - num, radius, radius, 90f, 90f);
            path.CloseFigure();
            return path;
        }

        public static void DrawFormFringe(Form destForm, Graphics g, Image fringeImg, int radius)
        {
            DrawImageWithNineRect(g, fringeImg, new Rectangle(-radius, -radius, destForm.ClientSize.Width + (2 * radius), destForm.ClientSize.Height + (2 * radius)), new Rectangle(0, 0, fringeImg.Width, fringeImg.Height));
        }

        public static void DrawFromAlphaMainPart(Form form, Graphics g)
        {
            Color[] colorArray = new Color[] { Color.FromArgb(5, Color.White), Color.FromArgb(30, Color.White), Color.FromArgb(0x91, Color.White), Color.FromArgb(150, Color.White), Color.FromArgb(30, Color.White), Color.FromArgb(5, Color.White) };
            float[] numArray = new float[] { 0f, 0.04f, 0.1f, 0.9f, 0.97f, 1f };
            ColorBlend blend = new ColorBlend(6) {
                Colors = colorArray,
                Positions = numArray
            };
            RectangleF rect = new RectangleF(0f, 0f, (float) form.Width, (float) form.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, colorArray[0], colorArray[5], LinearGradientMode.Vertical))
            {
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
        }

        public static void DrawImageWithNineRect(Graphics g, Image img, Rectangle targetRect, Rectangle srcRect)
        {
            int num = 5;
            Rectangle rectangle = new Rectangle((img.Width / 2) - num, (img.Height / 2) - num, 2 * num, 2 * num);
            int x = 0;
            int y = 0;
            int srcX = 0;
            int srcY = 0;
            int width = targetRect.Width;
            int height = targetRect.Height;
            x = targetRect.Left;
            y = targetRect.Top;
            int srcWidth = rectangle.Left - srcRect.Left;
            int srcHeight = rectangle.Top - srcRect.Top;
            srcX = srcRect.Left;
            srcY = srcRect.Top;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);
            x = (targetRect.Left + rectangle.Left) - srcRect.Left;
            srcWidth = (width - srcWidth) - (srcRect.Right - rectangle.Right);
            srcX = rectangle.Left;
            int num8 = rectangle.Right - rectangle.Left;
            int num9 = rectangle.Top - srcRect.Top;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, num8, num9, GraphicsUnit.Pixel);
            x = targetRect.Right - (srcRect.Right - rectangle.Right);
            srcWidth = srcRect.Right - rectangle.Right;
            srcX = rectangle.Right;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);
            x = targetRect.Left;
            y = (targetRect.Top + rectangle.Top) - srcRect.Top;
            srcWidth = rectangle.Left - srcRect.Left;
            srcHeight = (targetRect.Bottom - y) - (srcRect.Bottom - rectangle.Bottom);
            srcX = srcRect.Left;
            srcY = rectangle.Top;
            num8 = rectangle.Left - srcRect.Left;
            num9 = rectangle.Bottom - rectangle.Top;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, num8, num9, GraphicsUnit.Pixel);
            x = (targetRect.Left + rectangle.Left) - srcRect.Left;
            srcWidth = (width - srcWidth) - (srcRect.Right - rectangle.Right);
            srcX = rectangle.Left;
            num8 = rectangle.Right - rectangle.Left;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, num8, num9, GraphicsUnit.Pixel);
            x = targetRect.Right - (srcRect.Right - rectangle.Right);
            srcWidth = srcRect.Right - rectangle.Right;
            srcX = rectangle.Right;
            num8 = srcRect.Right - rectangle.Right;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, num8, num9, GraphicsUnit.Pixel);
            x = targetRect.Left;
            y = targetRect.Bottom - (srcRect.Bottom - rectangle.Bottom);
            srcWidth = rectangle.Left - srcRect.Left;
            srcHeight = srcRect.Bottom - rectangle.Bottom;
            srcX = srcRect.Left;
            srcY = rectangle.Bottom;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);
            x = (targetRect.Left + rectangle.Left) - srcRect.Left;
            srcWidth = (width - srcWidth) - (srcRect.Right - rectangle.Right);
            srcX = rectangle.Left;
            num8 = rectangle.Right - rectangle.Left;
            num9 = srcRect.Bottom - rectangle.Bottom;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, num8, num9, GraphicsUnit.Pixel);
            x = targetRect.Right - (srcRect.Right - rectangle.Right);
            srcWidth = srcRect.Right - rectangle.Right;
            srcX = rectangle.Right;
            g.DrawImage(img, new Rectangle(x, y, srcWidth, srcHeight), srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel);
        }

        public static Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int num = colorBase.A;
            int num2 = colorBase.R;
            int num3 = colorBase.G;
            int num4 = colorBase.B;
            if ((a + num) > 0xff)
            {
                a = 0xff;
            }
            else
            {
                a = Math.Max(a + num, 0);
            }
            if ((r + num2) > 0xff)
            {
                r = 0xff;
            }
            else
            {
                r = Math.Max(r + num2, 0);
            }
            if ((g + num3) > 0xff)
            {
                g = 0xff;
            }
            else
            {
                g = Math.Max(g + num3, 0);
            }
            if ((b + num4) > 0xff)
            {
                b = 0xff;
            }
            else
            {
                b = Math.Max(b + num4, 0);
            }
            return Color.FromArgb(a, r, g, b);
        }

        public static Image GetImageFormResourceStream(string imagePath)
        {
            try
            {
               Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + imagePath);
                if (stream == null)
                {
                    return Image.FromStream(stream);
                }
                else
                {
                   return  global::WinFramwwork.Properties.Resources.WinFormsUI_ControlExs_FormEx_Res_fringe_bkg;
                }
            }
            catch (Exception)
            {

                return  global::WinFramwwork.Properties.Resources.WinFormsUI_ControlExs_FormEx_Res_fringe_bkg;
            }
        }

        public static Image GetStringImgWithShadowEffect(string str, Font font, Color foreColor, Color shadowColor, int shadowWidth)
        {
            Bitmap image = null;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                SizeF ef = graphics.MeasureString(str, font);
                using (Bitmap bitmap2 = new Bitmap((int) ef.Width, (int) ef.Height))
                {
                    using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(0x10, shadowColor.R, shadowColor.G, shadowColor.B)))
                        {
                            using (SolidBrush brush2 = new SolidBrush(foreColor))
                            {
                                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                                graphics2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                                graphics2.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                graphics2.DrawString(str, font, brush, (float) 0f, (float) 0f);
                                image = new Bitmap(bitmap2.Width + shadowWidth, bitmap2.Height + shadowWidth);
                                using (Graphics graphics3 = Graphics.FromImage(image))
                                {
                                    graphics3.SmoothingMode = SmoothingMode.HighQuality;
                                    graphics3.InterpolationMode = InterpolationMode.HighQualityBilinear;
                                    graphics3.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                    for (int i = 0; i <= shadowWidth; i++)
                                    {
                                        for (int j = 0; j <= shadowWidth; j++)
                                        {
                                            graphics3.DrawImageUnscaled(bitmap2, i, j);
                                        }
                                    }
                                    graphics3.DrawString(str, font, brush2, (float) (shadowWidth / 2), (float) (shadowWidth / 2));
                                }
                                return image;
                            }
                        }
                    }
                }
            }
        }

        public static int HIWORD(int value)
        {
            return (value >> 0x10);
        }

        public static int LOWORD(int value)
        {
            return (value & 0xffff);
        }

        public static void MoveWindow(Form form)
        {
            Win32.ReleaseCapture();
            Win32.SendMessage(form.Handle, 0xa1, 2, 0);
        }

        public static void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            int hRgn = 0;
            hRgn = Win32.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
            Win32.SetWindowRgn(form.Handle, hRgn, true);
            Win32.DeleteObject(hRgn);
        }
    }
}

