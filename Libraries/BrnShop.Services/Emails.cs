using System;
using System.Text;

using BrnShop.Core;
using System.Web;

namespace BrnShop.Services
{
    /// <summary>
    /// 邮箱操作管理类
    /// </summary>
    public partial class Emails
    {
        private static object _locker = new object();//锁对象
        private static IEmailStrategy _iemailstrategy = null;//邮件策略
        private static EmailConfigInfo _emailconfiginfo = null;//邮件配置信息
        private static ShopConfigInfo _shopconfiginfo = null;//商城配置信息

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Emails()
        {
            _iemailstrategy = BSPEmail.Instance;
            _emailconfiginfo = BSPConfig.EmailConfig;
            _shopconfiginfo = BSPConfig.ShopConfig;
            _iemailstrategy.Host = _emailconfiginfo.Host;
            _iemailstrategy.Port = _emailconfiginfo.Port;
            _iemailstrategy.UserName = _emailconfiginfo.UserName;
            _iemailstrategy.Password = _emailconfiginfo.Password;
            _iemailstrategy.From = _emailconfiginfo.From;
            _iemailstrategy.FromName = _emailconfiginfo.FromName;
        }

        /// <summary>
        /// 重置邮件配置信息
        /// </summary>
        public static void ResetEmail()
        {
            lock (_locker)
            {
                _emailconfiginfo = BSPConfig.EmailConfig;
                _iemailstrategy.Host = _emailconfiginfo.Host;
                _iemailstrategy.Port = _emailconfiginfo.Port;
                _iemailstrategy.UserName = _emailconfiginfo.UserName;
                _iemailstrategy.Password = _emailconfiginfo.Password;
                _iemailstrategy.From = _emailconfiginfo.From;
                _iemailstrategy.FromName = _emailconfiginfo.FromName;
            }
        }

        /// <summary>
        /// 重置商城信息
        /// </summary>
        public static void ResetShop()
        {
            lock (_locker)
            {
                _shopconfiginfo = BSPConfig.ShopConfig;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功</returns>
        public static bool SendEmail(string to, string subject, string body)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                _iemailstrategy.Send(to, subject, body);
            });
            return true;
        }

        /// <summary>
        /// 发送找回密码邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <param name="userName">接收人</param>
        /// <param name="url">url</param>
        public static bool SendFindPwdEmail(string to, string userName, string url)
        {
            //标题
            string subject = _shopconfiginfo.ShopName + "找回密码邮件";

            StringBuilder body = new StringBuilder(_emailconfiginfo.FindPwdBody);
            body.Replace("{shopname}", _shopconfiginfo.ShopName);
            body.Replace("{siteurl}", _shopconfiginfo.SiteUrl);
            body.Replace("{username}", userName);
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", url);

            return SendEmail(to, subject, body.ToString());
        }

        /// <summary>
        /// 安全中心发送验证邮箱邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <param name="userName">接收人</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static bool SendSCVerifyEmail(string to, string userName, string url)
        {
            string subject = string.Format("{0}安全中心邮箱验证或激活提醒", _shopconfiginfo.ShopName);

            StringBuilder body = new StringBuilder(_emailconfiginfo.SCVerifyBody);
            body.Replace("{shopname}", _shopconfiginfo.ShopName);
            body.Replace("{siteurl}", _shopconfiginfo.SiteUrl);
            body.Replace("{username}", userName);
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", url);

            return SendEmail(to, subject, body.ToString());
        }

        /// <summary>
        /// 安全中心发送确认更新邮箱邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <param name="userName">接收人</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static bool SendSCUpdateEmail(string to, string userName, string url)
        {
            string subject = string.Format("{0}安全中心邮箱确认提醒", _shopconfiginfo.ShopName);

            StringBuilder body = new StringBuilder(_emailconfiginfo.SCUpdateBody);
            body.Replace("{shopname}", _shopconfiginfo.ShopName);
            body.Replace("{siteurl}", _shopconfiginfo.SiteUrl);
            body.Replace("{username}", userName);
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", url);

            return SendEmail(to, subject, body.ToString());
        }

        /// <summary>
        /// 发送注册欢迎邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <returns></returns>
        public static bool SendWebcomeEmail(string to)
        {
            string subject = string.Format("恭喜您成功注册为{0}会员", _shopconfiginfo.ShopName);

            StringBuilder body = new StringBuilder(_emailconfiginfo.WebcomeBody);
            body.Replace("{shopname}", _shopconfiginfo.ShopName);
            body.Replace("{regtime}", CommonHelper.GetDateTime());
            body.Replace("{email}", to);

            return _iemailstrategy.Send(to, subject, body.ToString());
        }

        /// <summary>
        /// 发送激活邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool SendActivateEmail(string to, string url)
        {
            string subject = string.Format("{0}注册激活", _shopconfiginfo.ShopName);
            // return SendEmail(to, subject, string.Format("<p>感谢注册{0}({1})，请点击下面链接完成激活<br/>{2}</p>", _shopconfiginfo.ShopName, _shopconfiginfo.SiteUrl, url));
            
            return SendEmail(to, subject,string.Format(@"<div style='text - align: center; white - space: normal; '>
         <div style = 'background-color: rgb(204, 204, 204);'>
              <p style = 'text-align: center;'>
                   <img src = 'http://www.vedantti.cn/themes/default/images/logo21.png' />
                  </p>      
              </div>
              <p>
                  亲爱的会员您好：
    </p>
    <p>
        <span style = 'white-space: normal;'>欢迎加入VEDANTTI，感谢您注册VEDANTTI会员，成为我们VEDANTTI的一员。
请点击下方链接激活您的账户： </span>：
    </p>
    <p>
        {2}
    </p>
    <div style = 'padding-top: 10px; padding-bottom: 10px; background-color: rgb(0, 0, 0); color: rgb(255, 255, 255); font-size: 14px; line-height: 25px;'>
 
         <div style = 'text-align: center;'>
              See you at Vedantti.cn
          </div>
  
          <div style = 'text-align: center;'>
               Thanks & amp; Regards
              </div>
              <div style = 'text-align: center;'>
                   Vedantti Team
               </div>
           </div>
       </div>
       <p style = 'white-space: normal;'></p> ", _shopconfiginfo.ShopName, _shopconfiginfo.SiteUrl, url));

        }

        /// <summary>
        /// 发送支付通知邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="payName">支付方式名称</param>
        /// <param name="payAmt">支付金额</param>
        /// <param name="osn">订单编号</param>
        /// <returns></returns>
        public static bool SendOrderPayEmail(string to, string payName, string payAmt, string osn)
        {
            string subject = string.Format("{0}订单支付成功通知", _shopconfiginfo.ShopName);
            //string body = string.Format("<p>尊敬的客户，你好！<br/><br/>&emsp;&emsp;您通过{0}完成了一笔金额为{1}的支付，订单号：{2}。<br/><br/>{3}</p>", payName, payAmt, osn, _shopconfiginfo.SiteUrl);

 string body = string.Format(@"<div style='text - align: center; white - space: normal; '>
         <div style = 'background-color: rgb(204, 204, 204);'>
              <p style = 'text-align: center;'>
                   <img src = 'http://www.vedantti.cn/themes/default/images/logo21.png' />
                  </p>      
              </div>
              <p>
                  亲爱的客户您好：
    </p>
    <p>
        <span style = 'white-space: normal;'> 您通过{0}完成了一笔金额为{1}的支付，订单号为：{2} </span>：
    </p>
    <div style = 'padding-top: 10px; padding-bottom: 10px; background-color: rgb(0, 0, 0); color: rgb(255, 255, 255); font-size: 14px; line-height: 25px;'>
 
         <div style = 'text-align: center;'>
              See you at Vedantti.cn
          </div>
  
          <div style = 'text-align: center;'>
               Thanks & amp; Regards
              </div>
              <div style = 'text-align: center;'>
                   Vedantti Team
               </div>
           </div>
       </div>
       <p style = 'white-space: normal;'></p> ", payName, payAmt, osn, _shopconfiginfo.SiteUrl);


            return SendEmail(to, subject, body);
        }

        /// <summary>
        /// 卖家确认定单邮件通知
        /// </summary>
        /// <param name="to"></param>
        /// <param name="osn">订单编号</param>
        /// <returns></returns>
        public static bool SendOrderConfirmEmail(string to, string osn)
        {
            string subject = string.Format("{0}卖家确认定单通知", _shopconfiginfo.ShopName);

  string body = string.Format(@"<div style='text - align: center; white - space: normal; '>
         <div style = 'background-color: rgb(204, 204, 204);'>
              <p style = 'text-align: center;'>
                   <img src = 'http://www.vedantti.cn/themes/default/images/logo21.png' />
                  </p>      
              </div>
              <p>
                  亲爱的客户您好：
    </p>
    <p>
        <span style = 'white-space: normal;'>您的订单：{0}，卖家已确认。 </span>：
    </p>
    <div style = 'padding-top: 10px; padding-bottom: 10px; background-color: rgb(0, 0, 0); color: rgb(255, 255, 255); font-size: 14px; line-height: 25px;'>
 
         <div style = 'text-align: center;'>
              See you at Vedantti.cn
          </div>
  
          <div style = 'text-align: center;'>
               Thanks & amp; Regards
              </div>
              <div style = 'text-align: center;'>
                   Vedantti Team
               </div>
           </div>
       </div>
       <p style = 'white-space: normal;'></p> ", osn, _shopconfiginfo.SiteUrl);


            return SendEmail(to, subject, body);
        }

        /// <summary>
        /// 卖家发货后邮件通知
        /// </summary>
        /// <param name="to"></param>
        /// <param name="osn">订单编号</param>
        /// <returns></returns>
        public static bool SendOrderFinishedEmail(string to, string osn, string uidStr, string shipSN)
        {
            string subject = string.Format("{0}卖家发货通知", _shopconfiginfo.ShopName);
           // string body = string.Format("<p>尊敬的客户，你好！<br/><br/>&emsp;&emsp;您的订单：{0}，卖家已发货。<br/><br/>&emsp;&emsp;快递单号：{1}<br/><br/>&emsp;&emsp;UID：{2}<br/><br/>{3}</p>", osn, shipSN, uidStr, _shopconfiginfo.SiteUrl);

            string body = string.Format(@"<div style='text - align: center; white - space: normal; '>
         <div style = 'background-color: rgb(204, 204, 204);'>
              <p style = 'text-align: center;'>
                   <img src = 'http://www.vedantti.cn/themes/default/images/logo21.png' />
                  </p>      
              </div>
              <p>
                  亲爱的客户您好：
    </p>
    <p>
        <span style = 'white-space: normal;'>您的订单：{0}，卖家已发货。 </span>：
    </p>
    <p>
        快递单号：{1}&emsp;&emsp;UID：{2}
    </p>
    <div style = 'padding-top: 10px; padding-bottom: 10px; background-color: rgb(0, 0, 0); color: rgb(255, 255, 255); font-size: 14px; line-height: 25px;'>
 
         <div style = 'text-align: center;'>
              See you at Vedantti.cn
          </div>
  
          <div style = 'text-align: center;'>
               Thanks & amp; Regards
              </div>
              <div style = 'text-align: center;'>
                   Vedantti Team
               </div>
           </div>
       </div>
       <p style = 'white-space: normal;'></p> ", osn, shipSN, uidStr, _shopconfiginfo.SiteUrl);
            return SendEmail(to, subject, body);
        }
    }
}
