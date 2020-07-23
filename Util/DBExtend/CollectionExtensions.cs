using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Util
{
    /// <summary>
    /// 集合相关扩展
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 随机数
        /// </summary>
        [ThreadStatic]
        private static Random random;

        /// <summary>
        /// 获取随机数
        /// </summary>
        private static Random Random
        {
            get
            {
                return random ?? (random = new Random(Environment.TickCount));
            }
        }
        /// <summary>
        /// 计算总数
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int Count(this IEnumerable source)
        {
            int res = 0;

            foreach (var item in source)
                res++;

            return res;
        }

        /// <summary>
        /// 对集合中每一项执行一个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        /// <summary>
        /// 将集合中每个元素执行特定的方法
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="action">执行具体方法</param>
        public static void ForEach(this IEnumerable source, Action<object> action)
        {
            if (source == null)
            {
                return;
            }
            foreach (var item in source)
            {
                action(item);
            }

        }

        /// <summary>
        /// 是否是空集合对象
        /// </summary>
        /// <param name="self">数据源</param>
        /// <returns></returns>
        public static bool IsNullEmpty(this IList self) { return self == null || self.Count == 0; }


        /// <summary>
        /// 随机打散
        /// </summary>
        /// <typeparam name="T">待打散的列表</typeparam>
        /// <param name="list">返回打散后的列表</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// 尝试寻找符合条件的数据,若找不到会返回默认值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">数据源</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="fefresh">刷新数据源的方法</param>
        /// <returns></returns>
        public static T TryFind<T>(this List<T> list, Predicate<T> predicate, Func<List<T>> fefresh = null)
        {
            var model = list.Find(predicate);

            if (fefresh != null && model == null)
            {
                var temp = fefresh();
                temp.Clear();
                if (temp != null)
                {
                    temp.ForEach(t => model.Equals(t));
                }

                model = list.Find(predicate);
            }

            if (model != null)
            {
                return model;
            }

            Type type = typeof(T);

            if (type.IsValueType || type == typeof(string))
            {
                return default(T);
            }
            else
            {
                return (T)Activator.CreateInstance(type);
            }
        }

        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(this IList<T> list) where T : class
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = typeof(T).GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}
