using System;
using System.Net;
using System.Text;
using System.Net.Mail;

using BrnShop.Core;

namespace BrnShop.EmailStrategy.DotNet
{
    /// <summary>
    /// 基于.Net自带的邮件框架的策略
    /// </summary>
    public partial class EmailStrategy : IEmailStrategy
    {
        private string _host;
        private int _port;
        private string _username;
        private string _password;
        private string _from;
        private string _fromname;
        private Encoding _bodyencoding = Encoding.GetEncoding("utf-8");
        private bool _isbodyhtml = true;

        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 发送邮件的账号
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 发送邮件的密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// 发送邮件的昵称
        /// </summary>
        public string FromName
        {
            get { return _fromname; }
            set { _fromname = value; }
        }

        /// <summary>
        /// 发送邮件(只能用25端口发送，而阿里云不支持25端口)
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功</returns>
        private bool SendA(string to, string subject, string body)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (_port != 25)
                smtp.EnableSsl = true;

            smtp.Host = _host;
            smtp.Port = _port;
            smtp.Credentials = new NetworkCredential(_username, _password);

            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(_from, subject, _bodyencoding);
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.BodyEncoding = _bodyencoding;
            mm.IsBodyHtml = _isbodyhtml;

            try
            {
                BSPLog.Instance.Write(string.Format("发送邮件：{0}:{1}", smtp.Host, smtp.Port));
                smtp.Send(mm);
            }
            catch (Exception Ex)
            {
                BSPLog.Instance.Write("发送邮件时出错：" + Ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="bodyEncoding">邮件内容编码</param>
        /// <param name="isBodyHtml">邮件内容是否html化</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string to, string subject, string body, Encoding bodyEncoding, bool isBodyHtml)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (_port != 25)
                smtp.EnableSsl = true;

            smtp.Host = _host;
            smtp.Port = _port;
            smtp.Credentials = new NetworkCredential(_username, _password);

            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(_from, subject, bodyEncoding);
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.BodyEncoding = bodyEncoding;
            mm.IsBodyHtml = isBodyHtml;

            try
            {
                smtp.Send(mm);
            }
            catch (Exception Ex)
            {
                BSPLog.Instance.Write("发送邮件时出错：" + Ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string to, string subject, string body)
        {
            System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
            try
            {
                mail.To = to;
                mail.From = _from;
                mail.Subject = subject;
                mail.BodyFormat = System.Web.Mail.MailFormat.Html;
                mail.Body = body;

                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", _username); //set your username here
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", _password); //set your password here
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", _port);//set port
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", _port != 25?"true":"false");//set is ssl
                System.Web.Mail.SmtpMail.SmtpServer = _host;
                BSPLog.Instance.Write(string.Format("发送邮件：{0}:{1}", _host, _port));
                System.Web.Mail.SmtpMail.Send(mail);
                BSPLog.Instance.Write("邮件发送成功");
                return true;
            }
            catch (Exception Ex)
            {
                BSPLog.Instance.Write("发送邮件时出错：" + Ex.Message);
                return false;
            }
        }
    }
}
