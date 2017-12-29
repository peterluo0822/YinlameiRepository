
using System;
using System.Text;
using System.Runtime;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace FineEx.Func
{

    public class DBPaperSize
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        internal struct structPrinterDefaults
        {

            [MarshalAs(UnmanagedType.LPTStr)]
            public String pDatatype;

            public IntPtr pDevMode;

            [MarshalAs(UnmanagedType.I4)]
            public int DesiredAccess;

        };

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPTStr)]
string printerName,

         out IntPtr phPrinter,

         ref structPrinterDefaults pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern bool ClosePrinter(IntPtr phPrinter);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        internal struct structSize
        {

            public Int32 width;

            public Int32 height;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        internal struct structRect
        {

            public Int32 left;

            public Int32 top;

            public Int32 right;

            public Int32 bottom;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        internal struct structFormInfo1
        {

            [MarshalAs(UnmanagedType.I4)]
            public int Flags;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String pName;

            public structSize Size;

            public structRect ImageableArea;

        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        internal struct structDevMode
        {

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public String dmDeviceName;

            [MarshalAs(UnmanagedType.U2)]
            public short dmSpecVersion;

            [MarshalAs(UnmanagedType.U2)]
            public short dmDriverVersion;

            [MarshalAs(UnmanagedType.U2)]
            public short dmSize;

            [MarshalAs(UnmanagedType.U2)]
            public short dmDriverExtra;

            [MarshalAs(UnmanagedType.U4)]
            public int dmFields;

            [MarshalAs(UnmanagedType.I2)]
            public short dmOrientation;

            [MarshalAs(UnmanagedType.I2)]
            public short dmPaperSize;

            [MarshalAs(UnmanagedType.I2)]
            public short dmPaperLength;

            [MarshalAs(UnmanagedType.I2)]
            public short dmPaperWidth;

            [MarshalAs(UnmanagedType.I2)]
            public short dmScale;

            [MarshalAs(UnmanagedType.I2)]
            public short dmCopies;

            [MarshalAs(UnmanagedType.I2)]
            public short dmDefaultSource;

            [MarshalAs(UnmanagedType.I2)]
            public short dmPrintQuality;

            [MarshalAs(UnmanagedType.I2)]
            public short dmColor;

            [MarshalAs(UnmanagedType.I2)]
            public short dmDuplex;

            [MarshalAs(UnmanagedType.I2)]
            public short dmYResolution;

            [MarshalAs(UnmanagedType.I2)]
            public short dmTTOption;

            [MarshalAs(UnmanagedType.I2)]
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public String dmFormName;

            [MarshalAs(UnmanagedType.U2)]
            public short dmLogPixels;

            [MarshalAs(UnmanagedType.U4)]
            public int dmBitsPerPel;

            [MarshalAs(UnmanagedType.U4)]
            public int dmPelsWidth;

            [MarshalAs(UnmanagedType.U4)]
            public int dmPelsHeight;

            [MarshalAs(UnmanagedType.U4)]
            public int dmNup;

            [MarshalAs(UnmanagedType.U4)]
            public int dmDisplayFrequency;

            [MarshalAs(UnmanagedType.U4)]
            public int dmICMMethod;

            [MarshalAs(UnmanagedType.U4)]
            public int dmICMIntent;

            [MarshalAs(UnmanagedType.U4)]
            public int dmMediaType;

            [MarshalAs(UnmanagedType.U4)]
            public int dmDitherType;

            [MarshalAs(UnmanagedType.U4)]
            public int dmReserved1;

            [MarshalAs(UnmanagedType.U4)]
            public int dmReserved2;

        }

        [DllImport("winspool.Drv", EntryPoint = "AddForm", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern bool AddForm(IntPtr phPrinter, [MarshalAs(UnmanagedType.I4)] int level, ref structFormInfo1 form);

        [DllImport("winspool.Drv", EntryPoint = "DeleteForm", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern bool DeleteForm(IntPtr phPrinter, [MarshalAs(UnmanagedType.LPTStr)] string pName);

        [DllImport("winspool.Drv", EntryPoint = "SetForm", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern bool SetForm(IntPtr phPrinter, [MarshalAs(UnmanagedType.LPTStr)] string pName, [MarshalAs(UnmanagedType.I4)] int level, ref structFormInfo1 form);

        [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false, ExactSpelling = true, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern Int32 GetLastError();
        [DllImport("GDI32.dll", EntryPoint = "CreateDC", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern IntPtr CreateDC([MarshalAs(UnmanagedType.LPTStr)]string pDrive, [MarshalAs(UnmanagedType.LPTStr)] string pName, [MarshalAs(UnmanagedType.LPTStr)] string pOutput, ref structDevMode pDevMode);
        [DllImport("GDI32.dll", EntryPoint = "ResetDC", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern IntPtr ResetDC(IntPtr hDC, ref structDevMode pDevMode);
        [DllImport("GDI32.dll", EntryPoint = "DeleteDC", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute()]
        internal static extern bool DeleteDC(IntPtr hDC);

        public System.Drawing.Printing.PaperSize GetPrintForm(string printerName, string paperName)
        {
            
            System.Drawing.Printing.PaperSize paper = null;
            System.Drawing.Printing.PrinterSettings printer = new System.Drawing.Printing.PrinterSettings();
            printer.PrinterName = printerName;
    
            foreach (System.Drawing.Printing.PaperSize ps in printer.PaperSizes)
            {
                if (ps.PaperName.ToLower() == paperName.ToLower())
                {
                    paper = ps;
                    break;
                }
            }
            return paper;

        }

        public void SetPrintForm(string printerName, string paperName, float width, float height)
        {

            if (PlatformID.Win32NT == Environment.OSVersion.Platform)
            {
                const int PRINTER_ACCESS_USE = 0x00000008;
                const int PRINTER_ACCESS_ADMINISTER = 0x00000004;

                structPrinterDefaults defaults = new structPrinterDefaults();

                defaults.pDatatype = null;

                defaults.pDevMode = IntPtr.Zero;

                defaults.DesiredAccess = PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE;

                IntPtr hPrinter = IntPtr.Zero;
                if (OpenPrinter(printerName, out hPrinter, ref defaults))
                {

                    try
                    {


                        structFormInfo1 formInfo = new structFormInfo1();

                        formInfo.Flags = 0;

                        formInfo.pName = paperName;

                        formInfo.Size.width = (int)(width * 100.0);

                        formInfo.Size.height = (int)(height * 100.0);

                        formInfo.ImageableArea.left = 0;

                        formInfo.ImageableArea.right = formInfo.Size.width;

                        formInfo.ImageableArea.top = 0;

                        formInfo.ImageableArea.bottom = formInfo.Size.height;

                        bool rslt = false;
                        if (GetPrintForm(printerName, paperName) != null)
                        {
                            rslt = SetForm(hPrinter, paperName, 1, ref formInfo);
                        }
                        else
                        {
                            this.AddCustomPaperSize(printerName, paperName, width, height);
                            rslt = true;
                        }
                        if (!rslt)
                        {

                            StringBuilder strBuilder = new StringBuilder();

                            strBuilder.AppendFormat("添加纸张{0}时发生错误!, 系统错误号: {1}", paperName, GetLastError());

                            //throw new ApplicationException(strBuilder.ToString());
                            MessageBox.Show(strBuilder.ToString());

                        }

                    }

                    finally
                    {

                        ClosePrinter(hPrinter);

                    }

                }
            }

        }

        public void AddCustomPaperSize(string printerName, string paperName, float width, float height)
        {

            if (PlatformID.Win32NT == Environment.OSVersion.Platform)
            {
                const int PRINTER_ACCESS_USE = 0x00000008;
                const int PRINTER_ACCESS_ADMINISTER = 0x00000004;

                structPrinterDefaults defaults = new structPrinterDefaults();

                defaults.pDatatype = null;

                defaults.pDevMode = IntPtr.Zero;

                defaults.DesiredAccess = PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE;

                IntPtr hPrinter = IntPtr.Zero;
                if (OpenPrinter(printerName, out hPrinter, ref defaults))
                {

                    try
                    {

                        DeleteForm(hPrinter, paperName);

                        structFormInfo1 formInfo = new structFormInfo1();

                        formInfo.Flags = 0;

                        formInfo.pName = paperName;

                        formInfo.Size.width = (int)(width * 100.0);

                        formInfo.Size.height = (int)(height * 100.0);

                        formInfo.ImageableArea.left = 0;

                        formInfo.ImageableArea.right = formInfo.Size.width;

                        formInfo.ImageableArea.top = 0;

                        formInfo.ImageableArea.bottom = formInfo.Size.height;

                        if (!AddForm(hPrinter, 1, ref formInfo))
                        {

                            StringBuilder strBuilder = new StringBuilder();

                            strBuilder.AppendFormat("添加纸张{0}时发生错误!, 系统错误号: {1}", paperName, GetLastError());

                            throw new ApplicationException(strBuilder.ToString());

                        }

                    }

                    finally
                    {

                        ClosePrinter(hPrinter);

                    }

                }

                else
                {

                    StringBuilder strBuilder = new StringBuilder();

                    strBuilder.AppendFormat("打开打印机{0} 时出现异常!, 系统错误号: {1}", printerName, GetLastError());

                    throw new ApplicationException(strBuilder.ToString());

                }

            }

            else
            {

                structDevMode pDevMode = new structDevMode();

                IntPtr hDC = CreateDC(null, printerName, null, ref pDevMode);

                if (hDC != IntPtr.Zero)
                {

                    const long DM_PAPERSIZE = 0x00000002L;

                    const long DM_PAPERLENGTH = 0x00000004L;

                    const long DM_PAPERWIDTH = 0x00000008L;

                    pDevMode.dmFields = (int)(DM_PAPERSIZE | DM_PAPERWIDTH | DM_PAPERLENGTH);

                    pDevMode.dmPaperSize = 256;

                    pDevMode.dmPaperWidth = (short)(width * 2.54 * 10000.0);

                    pDevMode.dmPaperLength = (short)(height * 2.54 * 10000.0);

                    ResetDC(hDC, ref pDevMode);

                    DeleteDC(hDC);

                }

            }

        }

    }
}



