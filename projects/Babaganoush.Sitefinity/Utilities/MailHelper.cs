// file:	Utilities\MailHelper.cs
//
// summary:	Implements the mail helper class
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;

namespace Babaganoush.Sitefinity.Utilities
{
    /// <summary>
    /// A mail helper.
    /// </summary>
    public static class MailHelper
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
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
        /// <param name="fromEmail">From email.</param>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public static void SendEmail(string fromEmail, string[] toEmail, string subject, string body)
        {
            // TODO: Find means of supporting email attachments with NotificationService.
            // TODO: Find way to prevent Sitefinity from persisting ad-hoc subscriber list in database indefinitely.
            var notificationService = SystemManager.GetNotificationService();
            var context = new ServiceContext("GenericEmailSender", "Babaganoush");

            var messageTemplate = new MessageTemplateRequestProxy
            {
                Subject = subject,
                BodyHtml = body
            };

            string resolveKey = string.Format("{0}-{1}-{2}", context.AccountName, context.ModuleName, Guid.NewGuid());
            IMessageJobRequest job = new MessageJobRequestProxy
            {
                MessageTemplate = messageTemplate,
                Subscribers = toEmail.Select(email => new SubscriberRequestProxy { Email = email, ResolveKey = resolveKey })
            };

            notificationService.SendMessage(context, job, new Dictionary<string, string>());
        }
    }
}
