using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;

namespace FineEx.Func
{
    public class CommonFunc
    {
        [DllImport("MessageBox.dll")]
        private static extern int OpenBox(string filename, string Msg);

        [DllImport("MessageBox.dll")]
        public static extern long DelBox();
        const string KEY_64 = "VavicApp";

        const string IV_64 = "VavicApp";

        public static int OpenWaitBox(string msg)
        {
            string fileName = Application.StartupPath + "\\setup.avi";
            return OpenBox(fileName, msg);
        }

        public static int OpenWaitBox()
        {
            return OpenWaitBox("正在查询，请稍候...");
        }

        public static DialogResult ShowMessage(string Msg)
        {
            return ShowMessage(Msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowMessage(string Msg, MessageBoxButtons MsgButtons, MessageBoxIcon msgIcon)
        {
            return MessageBox.Show(Msg, "系统提示", MsgButtons, msgIcon);
        }

        public static string GetGUID()
        {
            return Guid.NewGuid().ToString("N");

        }
        public static string EncryptMd5(string text)
        {
            string entext = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "MD5");
            return entext.Substring(8, 16);
        }

        public static String Encrypt(String Data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(Data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

        }

        public static String Decrypt(String Data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(Data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();

        }

        public static void OpenURL(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                ShowMessage("非法的地址！");
            }
        }

        public static string GetCPUID()
        {
            string cpuInfo = "";//cpu序列号
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            return cpuInfo;
        }

        public static DataTable ImportDataFromExcel(string fileName)
        {
            string _strOledDbConSheet2003 = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source ={0};Extended Properties=Excel 8.0;";
            string _strOledDbConSheet2007 = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source ={0};Extended Properties=Excel 12.0 Xml;HDR=yes;IMEX=1;";



            OleDbConnection curConn = new OleDbConnection();
            OleDbDataAdapter dbAda = null;
            DataTable dt = new DataTable();

            string extensionName = Path.GetExtension(fileName).ToUpper();
            switch (extensionName)
            {

                case ".XLS":
                    curConn.ConnectionString = string.Format(_strOledDbConSheet2003, fileName);
                    break;

                case ".XLSX":
                    curConn.ConnectionString = string.Format(_strOledDbConSheet2007, fileName);
                    break;

                default:
                    return null;

            }


            try
            {
                curConn.Open();
                dbAda = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", curConn);
                dbAda.Fill(dt);
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                return null;
            }
            finally
            {
                dbAda.Dispose();
                curConn.Close();
                curConn.Dispose();
            }
            return dt;
        }

        public static DataTable ImportDataFromCsv(string fileName)
        {
            return CSVFileHelper.OpenCSV(fileName);
        }

        public static DataTable ImportDataFromExcel()
        {
            OpenFileDialog opDia = new OpenFileDialog();
            opDia.Filter = "Excel 2003(*.xls)|*.xls|Excel 2007(*.xlsx)|*.xlsx|csv|*.csv";
            opDia.FilterIndex = 0;
            string fileName = "";
            if (opDia.ShowDialog() == DialogResult.OK)
            {
                fileName = opDia.FileName;
            }
            else
            {
                opDia.Dispose();
                return null;
            }
            //opDia.Dispose();

            try
            {
                //导入CSV文件 
                string extensionName = Path.GetExtension(fileName).ToUpper();
                if (extensionName == ".CSV")
                    return ImportDataFromCsv(fileName);

                return ImportDataFromExcel(fileName);

            }
            catch
            {
                return null;
            }

        }

        public static bool DownloadWebFile(string WebFileURL, string LocalFileName)
        {
            //创建目录
            string path = System.IO.Path.GetDirectoryName(LocalFileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (File.Exists(LocalFileName))
            {
                File.Delete(LocalFileName);
            }


            FileStream FS = new FileStream(LocalFileName, FileMode.Create);

            HttpWebRequest DownloadRequest = (HttpWebRequest)WebRequest.Create(WebFileURL);
            DownloadRequest.Timeout = 10000;

            try
            {
                Stream DownloadFS = DownloadRequest.GetResponse().GetResponseStream();
                byte[] Buffer = new byte[512];
                int ReadSize = 0;
                for (ReadSize = DownloadFS.Read(Buffer, 0, 512); ReadSize > 0; ReadSize = DownloadFS.Read(Buffer, 0, 512))
                {
                    FS.Write(Buffer, 0, ReadSize);
                }
                FS.Close();
                DownloadFS.Close();
                DownloadRequest.Abort();
                return true;
            }
            catch (Exception ex)
            {
                FS.Close();
                DownloadRequest.Abort();
                return false;
            }
        }

        #region 格式验证
        public static bool IsOnlyNumber(string value)
        {
            Regex r = new Regex(@"^[0-9]+$");

            return r.Match(value).Success;
        }

        public static bool IsNumberAndString(string value)
        {
            Regex r = new Regex(@"(\d+[a-zA-Z])|([a-zA-Z]\d+)");

            return r.Match(value).Success;
        }

        public static bool IsOnlyWord(string value)
        {
            Regex r = new Regex(@"^[a-zA-Z]+$");

            return r.Match(value).Success;
        }
        public static bool IsOnlyNumberPoint(string value)
        {
            Regex r = new Regex(@"^\d+(\.\d+)?$");

            return r.Match(value).Success;
        }
        #endregion

        public static int GetMasterID()
        {
            if (FineEx.Gloable.WarehouseIDDevelop.Equals(9156))
            {
                return 14;
            }
            return 8;
        }

        public static int GetPoolingID()
        {
            if (FineEx.Gloable.WarehouseIDDevelop.Equals(9156))
            {
                return 15;
            }
            return 9;
        }

        public static int GetUnipsCenterID()
        {
            return 10;
        }
        public static int GetOMSBLID()
        {
            return 12;
        }
        /// <summary>
        /// 将Linq的结果集转化成DataTable
        /// </summary>
        /// <param name="query">结果集</param>
        /// <returns>DataTable</returns>
        public static DataTable ConvertToTable(IQueryable query)
        {
            //var v = "fawe";
            //DataTable dt= ConvertToTable(v.AsQueryable());
            DataTable dtList = new DataTable();
            bool isAdd = false;
            PropertyInfo[] objProterties = null;
            foreach (var item in query)
            {
                if (!isAdd)
                {
                    objProterties = item.GetType().GetProperties();
                    foreach (var itemProterty in objProterties)
                    {
                        Type type = null;
                        if (itemProterty.PropertyType != typeof(string) && itemProterty.PropertyType != typeof(int) &&
                            itemProterty.PropertyType != typeof(DateTime))
                        {
                            type = typeof(string);
                        }
                        else
                        {
                            type = itemProterty.PropertyType;
                        }
                        dtList.Columns.Add(itemProterty.Name, type);
                    }
                    isAdd = true;
                }
                var row = dtList.NewRow();
                foreach (var pi in objProterties)
                {
                    row[pi.Name] = pi.GetValue(item, null);
                }
                dtList.Rows.Add(row);
            }

            return dtList;
        }
    }
}
