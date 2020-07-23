using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    /// <summary>
    /// 操作类别
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal = 1,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual = 2,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 3,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual = 4,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 5,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual = 6,
        /// <summary>
        /// Like
        /// </summary>
        Like = 7,
        /// <summary>
        /// StartWith
        /// </summary>
        StartWith = 8,
        /// <summary>
        /// EndWith
        /// </summary>
        EndWith = 9,
        /// <summary>
        /// In
        /// </summary>
        In = 10,
        ///// <summary>
        ///// 11留空,先不要用.
        ///// </summary>
        //EqualOr = 11
    }
}
