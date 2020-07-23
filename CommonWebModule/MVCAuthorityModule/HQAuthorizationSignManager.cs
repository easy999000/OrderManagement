using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthorityModule
{
    /// <summary>
    /// 权限标识管理器
    /// </summary>
    [Serializable]
    public class HQAuthorizationSignManager
    {
        /// <summary>
        /// 权限字典
        /// </summary>
        public Dictionary<string, int> HQAuthorizationSignDic = new Dictionary<string, int>();


        public void LoadData(List<HQAuthorizationSign> Data)
        {
            foreach (var item in Data)
            {
                if (!HQAuthorizationSignDic.ContainsKey(item.FullName))
                {
                    HQAuthorizationSignDic.Add(item.FullName, item.ID);
                }
                else
                {
                    HQAuthorizationSignDic[item.FullName] = item.ID;
                }
            }
        }

        public int GetHQAuthorizationSignID(HQAuthorizationSign Sign)
        {
            if (!HQAuthorizationSignDic.ContainsKey(Sign.FullName))
            {
                return -1;
            }
            return HQAuthorizationSignDic[Sign.FullName];
        }



    }
}
