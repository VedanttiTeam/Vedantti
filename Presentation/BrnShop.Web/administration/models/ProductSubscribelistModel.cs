using System;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
	/// 客户订阅列表模型类
	/// </summary>
    public class ProductSubscribelistModel
    {
        public ProductSubscribeListInfo[] ProductSubscribeList { get; set; }
    }
}
