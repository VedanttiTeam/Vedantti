using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrnShop.Data
{
    /// <summary>
    /// 其它处理
    /// </summary>
    public partial class Other
    {
        /// <summary>
        /// 订阅电子邮件
        /// </summary>
        /// <param name="email">电子邮件</param>
        /// <param name="ip">IP</param>
        public static bool SubscribeEmail(string email,string ip)
        {
            return BrnShop.Core.BSPData.RDBS.SubscribeEmail(email,ip);
        }

        /// <summary>
        /// 退订
        /// </summary>
        /// <param name="email">电子邮件</param>
        public static bool DelSubscribeEmail(string email)
        {
            return BrnShop.Core.BSPData.RDBS.DelSubscribeEmail(email);
        }
    }
}
