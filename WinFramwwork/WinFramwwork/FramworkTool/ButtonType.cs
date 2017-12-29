using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FineEx.Control.Forms
{
    /// <summary>
    /// 主框架上按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 0,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2,
        /// <summary>
        /// 保存
        /// </summary>
        Save = 3,

        /// <summary>
        /// 导出
        /// </summary>
        Export = 4,
        /// <summary>
        /// 导入
        /// </summary>
        Import = 5,

        /// <summary>
        /// 筛选
        /// </summary>
        Check = 6,
        /// <summary>
        /// 重置
        /// </summary>
        Reset = 7,
        /// <summary>
        /// 打印
        /// </summary>
        Prit = 8,

        /// <summary>
        /// 自定义1
        /// </summary>
        Other1 = 9,
        /// <summary>
        /// 自定义2
        /// </summary>
        Other2 = 10,
        /// <summary>
        /// 自定义3
        /// </summary>
        Other3 = 11,
        /// <summary>
        /// 帮助
        /// </summary>
        Help = 12,
        /// <summary>
        /// 退出
        /// </summary>
        signOut = 13,
    }
}
