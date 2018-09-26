using BrnShop.Core;
using BrnShop.Web.Framework;
using BrnShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 其它页面控制器
    /// </summary>
    public class OtherController : BaseWebController
    {
        /// <summary>
        /// 品牌故事
        /// </summary>
        public ActionResult Story()
        {
            return View();
        }

        /// <summary>
        /// 设计师
        /// </summary>
        public ActionResult Designer()
        {
            return View();
        }

        /// <summary>
        /// 制作工艺
        /// </summary>
        public ActionResult Technics()
        {
            return View();
        }

        /// <summary>
        /// 订阅
        /// </summary>
        public ActionResult Subscribe()
        {
            string vEmail = WebHelper.GetFormString("NewsletterSubscribeForm_Email");
            SubscribeModel vSubscribeModel = new SubscribeModel()
            {
                Email= vEmail
            };
            return View(vSubscribeModel);
        }

        /// <summary>
        /// 订阅电子邮箱
        /// </summary>
        /// <returns></returns>
        public ActionResult SubscribeEmail()
        {
            string vEmail = WebHelper.GetQueryString("email");
            if (string.IsNullOrWhiteSpace(vEmail))
                return AjaxResult("fail", "电邮地址不能为空");

            string vIP = WebHelper.GetIP();
            bool vResult = Services.Other.SubscribeEmail(vEmail, vIP);
            if (!vResult)
                return AjaxResult("fail", "订阅失败");

            return AjaxResult("success", "成功");
        }

        /// <summary>
        /// 退订电子邮箱
        /// </summary>
        /// <returns></returns>
        public ActionResult DelSubscribeEmail()
        {
            string vEmail = WebHelper.GetQueryString("email");
            if (string.IsNullOrWhiteSpace(vEmail))
                return AjaxResult("fail", "电邮地址不能为空");
            
            bool vResult = Services.Other.DelSubscribeEmail(vEmail);
            if (!vResult)
                return AjaxResult("fail", "退订失败");

            return AjaxResult("success", "成功");
        }

        /// <summary>
        /// MAP
        /// </summary>
        /// <returns></returns>
        public ActionResult Map()
        {
            return View();
        }
    }
}