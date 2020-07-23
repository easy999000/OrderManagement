using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    /// <summary>
    /// 带有查询条件的分页查询参数
    /// </summary>
    /// <typeparam name="TCondition"></typeparam>
    [Serializable]
    public class PageQueryParam<TCondition> : PageQueryParam
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public TCondition Condition { get; set; }
        public PageQueryParam(TCondition searchData)
        {
            this.Condition = searchData;

            PageIndex = 0;

            PageSize = 0;

            OrderBy = OrderBys.Create("", "-1");

            Condition = searchData;
        }
        public static PageQueryParam<T> CreateReuestParam<T>(T searchData)
        {
            return new PageQueryParam<T>(searchData);
        }

    }


    /// <summary>
    /// 描述分页查询参数
    /// </summary>
    [Serializable]
    public class PageQueryParam
    {
        /// <summary>
        /// 查询页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public OrderBys OrderBy { get; set; }

        /// <summary>
        /// 实例化新的PageQueryParam
        /// </summary>
        public PageQueryParam()
        {
            OrderBy = new OrderBys();
        }
    }
    /// <summary>
    /// 分页查询排序字段集合
    /// </summary>
    public class OrderBys
    {
        /// <summary>
        /// 获取排序项
        /// </summary>
        public List<KeyValuePair<string, bool>> Items = new List<KeyValuePair<string, bool>>();

        /// <summary>
        /// 实例化
        /// </summary>
        public OrderBys()
        {

        }

        /// <summary>
        /// 创建排序对象
        /// </summary>
        /// <param name="property">排序属性名,以此属性名进行升序排序</param>
        /// <returns></returns>
        public static OrderBys Create(string property)
        {
            return Create(property, true);
        }

        /// <summary>
        /// 创建排序对象
        /// </summary>
        /// <param name="property">排序属性名</param>
        /// <param name="isAsc">指示是否升序排序,true=升序,false=降序</param>
        /// <returns></returns>
        public static OrderBys Create(string property, bool isAsc)
        {
            var rs = new OrderBys();
            if (!string.IsNullOrWhiteSpace(property))
            {
                rs.Add(property, isAsc);
            }
            return rs;
        }

        /// <summary>    
        /// 创建排序对象
        /// </summary> 
        /// <param name="property"></param>
        /// <param name="orderType">排序方式 [1：正序]  [-1：倒序]</param>
        /// <returns></returns>
        public static OrderBys Create(string property, string orderType)
        {
            var rs = new OrderBys();
            if (!string.IsNullOrWhiteSpace(property))
            {
                if (!string.IsNullOrWhiteSpace(orderType))
                {
                    if (orderType == "1")
                    {
                        rs.Add(property, true);
                    }
                    else if (orderType == "-1")
                    {
                        rs.Add(property, false);
                    }
                }
            }
            return rs;
        }


        /// <summary>
        /// 是否有排序项
        /// </summary>
        public bool HasItem { get { return !Items.IsNullEmpty(); } }

        /// <summary>
        /// 添加一个新的排序项
        /// </summary>
        /// <param name="property">排序属性名</param>
        /// <param name="isAsc">指示是否升序排序,true=升序,false=降序</param>
        /// <returns></returns>
        public OrderBys Add(string property, bool isAsc)
        {
            Items.Add(new KeyValuePair<string, bool>(property, isAsc));
            return this;
        }
        /// <summary>
        /// 添加一个新的排序项
        /// </summary>
        /// <param name="property">排序属性名,默认为升序模式</param> 
        public OrderBys Add(string property)
        {
            Add(property, true);
            return this;
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Items.IsNullEmpty() ? "" : string.Join(",", Items.Select(t => t.Key + (t.Value ? "" : " DESC")));
        }
    }

}
