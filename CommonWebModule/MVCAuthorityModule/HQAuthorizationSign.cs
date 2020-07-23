using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthorityModule
{
    /// <summary>
    /// 基础权限标志  所有的名字都是没有前后 斜线的,不区分大小写
    /// </summary>
    public class HQAuthorizationSign
    {
        /// <summary>
        /// 区域
        /// </summary>
        string _Area;
        /// <summary>
        /// 控制器
        /// </summary>
        string _Controller;
        /// <summary>
        /// 方法
        /// </summary>
        string _Action;

        /// <summary>
        /// 区域
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area
        {
            get { return _Area; }
            set
            {
                string str = FormatName(value);
                if (string.IsNullOrWhiteSpace(str))
                {
                    str = "empty--name--area";
                }
                _Area = str;
            }
        }
        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller
        {
            get { return _Controller; }
            set
            {
                string str = FormatName(value);
                if (string.IsNullOrWhiteSpace(str))
                {
                    str = "empty--name--controller";
                }
                _Controller = str;
            }
        }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action
        {
            get { return _Action; }
            set
            {
                string str = FormatName(value);
                if (string.IsNullOrWhiteSpace(str))
                {
                    str = "empty--name--action";
                }
                _Action = str;
            }
        }


        public string FullName
        {
            get
            {
                return System.IO.Path.Combine(Area, Controller, Action).Replace("\\", "/");
                //return System.IO.Path.Combine(Area, Controller, Action);
                // return System.IO.Path.(Area, Controller, Action);
            }
        }

        /// <summary>
        /// 规范名称格式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string FormatName(string name)
        {
            return name.Trim('/').Trim('\\').ToLower();
        }



    }
}
