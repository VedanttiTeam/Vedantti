using BrnShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrnShop.Services
{
    public static class ProductSubscribe
    {
        /// <summary>
		/// 客户订阅
		/// </summary>
		/// <returns></returns>
		public static ProductSubscribeListInfo[] GetProductSubscribeList()
        {

            ProductSubscribeListInfo[] subscribeList = BrnShop.Data.ProductSubscribe.GetProductSubscribeList();
            return subscribeList;
        }

        /// <summary>
        /// 删除客户订阅
        /// </summary>
        /// <param name="SubscribeId">客户订阅id</param>
        /// <returns></returns>
        public static int DeleteProductSubscribeById(string SubscribeId)
        {
            BrnShop.Data.ProductSubscribe.DeleteProductSubscribeById(SubscribeId);
            return 1;
        }
    }
}
