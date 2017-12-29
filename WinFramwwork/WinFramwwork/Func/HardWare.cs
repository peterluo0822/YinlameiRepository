using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Management;
namespace FineEx.Func
{
    public class HardWare
    {
        //取机器名 
        public  static string GetHostName()
        {
            return System.Net.Dns.GetHostName(); 
        } 

      //取CPU编号
        public static String GetCpuID() 
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                  
                String strCpuID = null ;
                foreach( ManagementObject mo in moc ) 
                {
                 strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                 break; 
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }
        }

        //取第一块硬盘编号
        public static String GetHardDiskID() 
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                String strHardDiskID = null ;
                foreach(ManagementObject mo in searcher.Get()) 
                {    
                     strHardDiskID = mo["SerialNumber"].ToString().Trim();
                     break;          
                }
                    return strHardDiskID ;
               }
               catch
               {
                    return "";
                }
        }
    }
}

