using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace FineEx
{
    public enum DataVisitMode
    {
        Web,
        Local
    }

    public class Gloable
    {
        public static DataVisitMode DataMode = DataVisitMode.Local;
        public static string DataServiceURL = "";//外网webURL
        public static string InSideDataServiceURL = "";//内网webURL
        public static string MiddleDBServiceURL = "";
        public static bool IsInSideNet = false;//是否启用内网
        public static string ConnectString = "";
        public static string ProductCode = "";
        public static string ProductName = "";
        public static string SoftCode = "";
        public static int CompanyID = 1;//公司ID
        public static List<int> WMSWarehouseIDs;//所有仓库ID链接，不重复

        public static string FineExURL = "http://222.73.181.99";

        public static int OperatorID = 0;
        public static int ScanOperatorID = 0;
        public static string ScanOperatorName = "";
        public static string LoginName = "";
        public static string PassWord = "";

        public static IMainFrame objFrameData = null;
        public static int UserID = 0;
        public static int WarehouseID = 0;
        public static int WarehouseIDDevelop = 0;
        public static int Role = 0;
        public static Dictionary<int, string> WarehouseConnection = new Dictionary<int, string>();//数据链接
        public static Dictionary<string, int> PictureType = new Dictionary<string, int>();//图片链接
        public static DataTable dtRights = null;
        public static string funcCode = "";//当前模块Code 111
    }
}
