using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementModel
{
    [Serializable]
    [System.Diagnostics.DebuggerStepThrough]
    public class HQResult<T>
    {
        /// <summary>
        /// 状态编码 大于0 表示成功编码 ,小于等于0 表示失败编码
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 返回说明
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 泛型返回数据结果
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public string Value1 { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public int Value2 { get; set; }

        #region 方法
        /// <summary>
        /// 
        /// </summary> 
        public HQResult()
        {
            this.Status = 0;
            this.Msg = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <param name="value"></param>
        public HQResult(int status, string msg, T value)
        {
            this.Status = status;
            this.Msg = msg;
            this.Value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        public HQResult(int status, string msg)
        {
            this.Status = status;
            this.Msg = msg;
        }


        /// <summary>
        /// 设置返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <param name="value"></param>
        public HQResult<T> SetResult(int status, string msg, T value)
        {
            this.Status = status;
            this.Msg = msg;
            this.Value = value;
            return this;
        }
        /// <summary>
        /// 设置返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param> 
        public HQResult<T> SetResult(int status, string msg)
        {
            this.Status = status;
            this.Msg = msg;
            return this;
        }

        #endregion

    }
}
