using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace FineEx
{
    public interface IMainFrame
    {
        int CheckLogin(string UserCode, string PWD);
        void Init(int OperatorID);
        void Init(int OperatorID, int WarehouseID, string WarehouseName);
        int ChangePWD(int OperatorID, string OldPWD, string NewPWD);

        DataTable GetFuncGroup(int OperaterID);

        DataSet GetFunc(int OperaterID);

        /// <summary>
        /// 得到用户权限仓库
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>table</returns>
        DataTable GetUserWarehouseConection(int UserID);
        /// <summary>
        /// 得到用户权限仓库
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>table</returns>
        DataTable GetUserWarehouseConection(string LogionName);
        /// <summary>
        /// 检测用户是否有权限访问该仓库
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <returns>bool</returns>
        bool CheckUserWarehouse(int UserID, int WarehouseID);
        DataTable getUserRights(int UserID);

    }
}
