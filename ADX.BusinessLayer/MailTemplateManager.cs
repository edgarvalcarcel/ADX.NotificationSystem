/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using ADX.Entities.DomainModel;
using DotLiquid;
using System.Collections.Generic;
using System.Linq;

namespace ADX.BusinessLayer
{
    public class MailTemplateManager
    {
        public NotificationQueue GetNotificationQueue(Notification notification)
        {
            BusinessLayer businessLayer = new BusinessLayer();
            MailTemplate mailTemplate = businessLayer.GetMailTemplateById(notification.NotificationTemplate);
            // Get notification preferences per user
            foreach (var notifyTo in notification.MailAddresses)
            {
                NotificationByUser preferences = businessLayer.GetNotificationsByUserId(notifyTo.UserId, notification.NotificationTemplate);
                if (preferences != null)
                {
                    // Get preferences set by the user
                    notifyTo.ByEmail = preferences.ByEmail;
                    notifyTo.BySms = preferences.BySMS;
                }
                else
                {
                    // Set preferences by default
                    notifyTo.ByEmail = !string.IsNullOrEmpty(mailTemplate.Body) && !string.IsNullOrEmpty(mailTemplate.Subject);
                    notifyTo.BySms = !string.IsNullOrEmpty(mailTemplate.SMSText) && notifyTo.CellphoneVerified;
                }
            }

            NotificationQueue notificationQueue = new NotificationQueue(notification);
            Dictionary<string, object> objDictionary = notification.Dictionary.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
            notificationQueue.NotificationEmailBody = GetFullBody(mailTemplate.Body, objDictionary);
            notificationQueue.NotificationEmailSubject = GetFullBody(mailTemplate.Subject, objDictionary);
            notificationQueue.NotificationSmsText = GetFullBody(mailTemplate.SMSText, objDictionary);
            notificationQueue.NotificationSmsType = mailTemplate.SmsTextType;
            notificationQueue.NotifyByBcc = mailTemplate.BccToAccountManager;
            return notificationQueue;
        }

        private static string RenderBody(string templateBody, IDictionary<string, object> notificationDictionary)
        {
            return Template.Parse(templateBody)
                .Render(Hash.FromDictionary(notificationDictionary.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value is string
                        ? kvp.Value
                        : kvp.Value.ToString())));
        }

        private string GetFullBody(string templateBody, IDictionary<string, object> notificationDictionary)
        {
            return string.IsNullOrEmpty(templateBody)
                ? string.Empty
                : (templateBody.Contains("{{")
                    ? RenderBody(templateBody, notificationDictionary)
                    : templateBody);
        }
    }
}