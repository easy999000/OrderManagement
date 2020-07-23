using OrderManagementModel.DBModel.Authority;
using System;
using System.Collections.Generic;
using System.Text;
using Util;

namespace OrderManagementModel.DBWhere
{
    public class AccountWhere
    {
        /// <summary>
        /// id
        /// </summary>
        [QueryParam( OperatorType.Equal,nameof(Authority_Account.ID)) ]
        public int ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [QueryParam(OperatorType.Equal, nameof(Authority_Account.Account))]
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [QueryParam(OperatorType.Like, nameof(Authority_Account.Name))]
        public string Name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [QueryParam(OperatorType.StartWith, nameof(Authority_Account.Phone))]
        public string Phone { get; set; }
        /// <summary>
        /// 最小年龄,包含
        /// </summary>
        [QueryParam(OperatorType.GreaterThanOrEqual, nameof(Authority_Account.Age))]
        public int Age_Min { get; set; }
        /// <summary>
        /// 最大年龄不包含
        /// </summary>
        [QueryParam(OperatorType.LessThan, nameof(Authority_Account.Age))]
        public int Age_Max { get; set; }
    }
}
