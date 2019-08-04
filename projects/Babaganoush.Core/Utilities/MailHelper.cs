using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A mail helper.
    /// </summary>
    public static class MailHelper
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        ///
        /// <param name="fromEmail">From email.</param>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public static void SendEmail(string fromEmail, string toEmail, string subject, string body)
        {
            SendEmail(fromEmail, new[] { toEmail }, subject, body);
        }
        /// <summary>
        /// Sends the email.
        /// </summary>
        ///
        /// <param name="fromEmail">From email.</param>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="attachments">(Optional) File attatchments.</param>
        /// <param name="config">(Optional) Configuration.</param>
        public static void SendEmail(string fromEmail, string[] toEmail, string subject, string body,
            Dictionary<Stream, string> attachments = null, SmtpNetworkElement config = null)
        {
            //GET SMTP SETTINGS IF APPLICABLE
            if (config == null)
            {
                config = (ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection).Network;
            }
            //CREATE SMTP CLIENT
            var smtpClient = new SmtpClient(config.Host);
            smtpClient.EnableSsl = config.EnableSsl;
            smtpClient.Port = config.Port;
            //SET CREDENTIALS IF APPLICABLE
            if (!string.IsNullOrWhiteSpace(config.UserName) && !string.IsNullOrWhiteSpace(config.Password))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(config.UserName, config.Password);
            }
            //CREATE MESSAGE
            var message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            //DETERMINE EMAIL ADDRESSES TO SEND TO
            foreach (var item in toEmail)
            {
                message.To.Add(item);
            }
            //ATTACH DOCUMENT FROM LIBRARY IF APPLICABLE
            if (attachments != null && attachments.Count > 0)
            {
                foreach (var item in attachments)
                {
                    message.Attachments.Add(new Attachment(item.Key, item.Value));
                }
            }
            //SEND EMAIL
            smtpClient.Send(message);
        }
    }
}