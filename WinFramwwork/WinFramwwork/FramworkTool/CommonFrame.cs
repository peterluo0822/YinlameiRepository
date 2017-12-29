using System.Data;

namespace FineEx.Control.Forms
{
    public class CommonFrame
    {

        private static DataSet _navigationDataSource;

        /// <summary>
        /// 模块数据源
        /// DataSource.Tables[0];//一级模块
        /// DataSource.Tables[1];//二级模块
        /// DataSource.Tables[2];//三级模块
        /// </summary>
        public static DataSet NavigationDataSource
        {
            get
            {
                if (_navigationDataSource == null)
                {
                    _navigationDataSource = new DataSet();
                    _navigationDataSource.ReadXml("navigationDataSource");
                    _navigationDataSource.Relations.Add("左联", _navigationDataSource.Tables[1].Columns["ModuleCode"], _navigationDataSource.Tables[2].Columns["ParentModuleCode"], false);

                }

                return _navigationDataSource;
            }

        }


    }
}
