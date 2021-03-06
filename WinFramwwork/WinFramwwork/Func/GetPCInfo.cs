using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Management;
using System.Net.NetworkInformation;

namespace FineEx.Func
{
    public class GetPCInfo
    {

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        //获取本机的IP
        public static string getLocalIP()
        {
            string strHostName = Dns.GetHostName(); //得到本机的主机名
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP
            string strAddr = ipEntry.AddressList[0].ToString();
            return (strAddr);
        }

        //获取本机的MAC
        public static string getLocalMac()
        {

            string mac = null;
            try
            {
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection queryCollection = query.Get();
                foreach (ManagementObject mo in queryCollection)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                    {
                        mac = mo["MacAddress"].ToString();
                    }
                }
                if (mac == null)
                {
                    foreach (ManagementObject mo in queryCollection)
                    {
                        if (mo["MacAddress"] != null && mo["MacAddress"].ToString().Trim().Length > 1)
                        {
                            mac = mo["MacAddress"].ToString();
                        }
                    }
                }
            }
            catch
            {
                mac = GetMacString();
            }
            if (mac == null || mac.Trim() == "")
            {
                mac = GetMacString();
            }
            return (mac);

        }
        //获取本机的MAC地址2，备用
        public static string GetMacString()
        {
            try
            {
                string strMac = "";
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        strMac += ni.GetPhysicalAddress().ToString() + "|";
                    }
                }
                string[] strmacnew = strMac.Split('|');
                if (strmacnew.Length == 0 || strmacnew[0].Length < 10)
                {

                    return "";
                }
                else
                {
                    StringBuilder Macret = new StringBuilder();
                    for (int i = 0; i < strmacnew[0].Length; i++)
                    {
                        Macret.Append(strmacnew[0].Substring(i, 1));
                        if (i != 0 && i % 2 == 1)
                        {
                            Macret.Append(":");
                        }
                    }
                    return Macret.ToString().Remove(Macret.ToString().Length - 1, 1);
                }
            }
            catch
            { return ""; }
        }

        //获取本机的外网IP地址
        public static string getInternetIP()
        {
            //return "211.95.7.6";
            string ip = "";
            //try
            //{
            //    //string strUrl = "http://www.ip138.com/ip2city.asp"; //获得IP的网址了  
            //    string strUrl = "http://www.ip138.com/ips1388.asp"; //获得IP的网址了  

            //    Uri uri = new Uri(strUrl);
            //    System.Net.WebRequest wr = System.Net.WebRequest.Create(uri);
            //    System.IO.Stream s = wr.GetResponse().GetResponseStream();
            //    System.IO.StreamReader sr = new System.IO.StreamReader(s, Encoding.Default);
            //    string all = sr.ReadToEnd(); //读取网站的数据  

            //    int i = all.IndexOf("您的IP地址是：[") + 9;
            //    string tempip = all.Substring(i, 15);
            //    ip = tempip.Replace("]", "").Replace(" ", "");//找出i  

            //}
            //catch
            //{
            //    ip = "0.0.0.0";
            //}
            ip = "0.0.0.0";
            return ip;
        }

        //获取远程主机IP
        public string[] getRemoteIP(string RemoteHostName)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(RemoteHostName);
            IPAddress[] IpAddr = ipEntry.AddressList;
            string[] strAddr = new string[IpAddr.Length];
            for (int i = 0; i < IpAddr.Length; i++)
            {
                strAddr[i] = IpAddr[i].ToString();
            }
            return (strAddr);
        }

        //获取远程主机MAC
        public string getRemoteMac(string localIP, string remoteIP)
        {
            Int32 ldest = inet_addr(remoteIP); //目的ip 
            Int32 lhost = inet_addr(localIP); //本地ip 

            try
            {
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                return Convert.ToString(macinfo, 16);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:{0}", err.Message);
            }
            return 0.ToString();
        }


        public static void Get()
        {
            //GetPCInfo gi = new GetPCInfo();
            //Console.WriteLine("本地网卡信息:");
            //Console.WriteLine(getLocalIP() + " - " + gi.getLocalMac() + "-[" + getInternetIP()+"]");

            //Console.WriteLine("\n\r远程网卡信息:");
            //string[] temp = gi.getRemoteIP("scmobile-tj2");
            //for (int i = 0; i < temp.Length; i++)
            //{
            //    Console.WriteLine(temp[i]);
            //}
            //Console.WriteLine(gi.getRemoteMac("192.168.0.3", "192.168.0.1"));
        }
    }
}
