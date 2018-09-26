using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrnShop.Core;
using System.Data;

namespace BrnShop.Data
{
    public class ProductSubscribe
    {
        /// <summary>
		/// 客户订阅列表
		/// </summary>
		/// <returns></returns>
		public static ProductSubscribeListInfo[] GetProductSubscribeList()
        {
            DataTable dt = BrnShop.Core.BSPData.RDBS.GetProductSubscribelist();
            ProductSubscribeListInfo[] productSubscribelist = new ProductSubscribeListInfo[dt.Rows.Count];

            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                ProductSubscribeListInfo subscribelistInfo = new ProductSubscribeListInfo
                {
                    FID = Guid.Parse(row[0].ToString()),
                    FvchrEmail = row[1].ToString(),
                    FvchrIP = row[2].ToString(),
                    FdmCreateDate = Convert.ToDateTime(row[3]).ToString("yyyy-MM-dd"),
                    FintDisabled = TypeHelper.ObjectToInt(row[4])
                };
                productSubscribelist[index] = subscribelistInfo;
                index++;
            }
            return productSubscribelist;
        }

        /// <summary>
        /// 删除客户订阅
        /// </summary>
        /// <param name="consultTypeId">客户订阅id</param>
        public static void DeleteProductSubscribeById(string SubscribeId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteProductSubscribeById(SubscribeId);
        }
    }
}
