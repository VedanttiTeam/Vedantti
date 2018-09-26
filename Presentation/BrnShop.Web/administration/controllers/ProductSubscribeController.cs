using System;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    public class ProductSubscribeController : BaseAdminController
    {
        /// <summary>
		/// 客户订阅
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public ActionResult ProductSubscribelist()
        {
            ProductSubscribelistModel model = new ProductSubscribelistModel()
            {
                ProductSubscribeList = ProductSubscribe.GetProductSubscribeList()
            };
            ShopUtils.SetAdminRefererCookie(Url.Action("ProductSubscribelist"));
            return View(model);
        }

        /// <summary>
        /// 删除客户订阅
        /// </summary>
        /// <param name="SubscribeId"></param>
        /// <returns></returns>
        public ActionResult DeleteProductSubscribeById(string SubscribeId)
        {
            if(string.IsNullOrWhiteSpace(SubscribeId))
                return PromptView("删除失败,没有指定的订阅ID");

            int result = ProductSubscribe.DeleteProductSubscribeById(SubscribeId);
            if (result == 0)
                return PromptView("删除失败");

            AddAdminOperateLog("删除客户订阅", "删除客户订阅,ID为:" + SubscribeId);
            return PromptView("客户订阅删除成功");

        }
    }
}
