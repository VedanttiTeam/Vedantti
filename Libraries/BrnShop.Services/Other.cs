using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrnShop.Services
{
    /// <summary>
    /// 其它处理
    /// </summary>
    public partial class Other
    {
        /// <summary>
        /// 订阅邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public static bool SubscribeEmail(string email, string ip)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return BrnShop.Data.Other.SubscribeEmail(email, ip);
        }

        /// <summary>
        /// 退订阅邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public static bool DelSubscribeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return BrnShop.Data.Other.DelSubscribeEmail(email);
        }
    }
}
