using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class QueryParamAttribute : Attribute
    {
        public OperatorType OperatorType { get; set; }
        public string FieldName { get; set; }

        //Expression<Func<TSource, bool>>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Operator">操作符</param>
        /// <param name="Operator">关联字段</param>
        public QueryParamAttribute(OperatorType Operator, string FieldName)
        {
            this.OperatorType = Operator;
            this.FieldName = FieldName;
        }



    }
}
