/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using System;
using ADX.Entities;
using ADX.Entities.DomainModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ADX.BusinessLayer
{
    public class UserNotificationManager : IUserNotificationManager
    {
        /// <summary>
        /// Sets the user notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        public bool SetUserNotification(Notification notification)
        {
            BusinessLayer businessLayer = new BusinessLayer();
            List<UserNotification> users = new List<UserNotification>();
            foreach (var notifyTo in notification.MailAddresses.Where(m => !string.IsNullOrEmpty(m.UserId)))
            {
                users.Add(new UserNotification
                {
                    UserId = notifyTo.UserId,
                    Attachments = JsonConvert.SerializeObject(notification.Attachments),
                    ByEmail = notifyTo.ByEmail,
                    BySMS = notifyTo.BySms,
                    DisplayName = notifyTo.DisplayName,
                    Email = notifyTo.EmailAddress,
                    Mobile = notifyTo.CellphoneVerified ? notifyTo.CellphoneNumber : string.Empty,
                    EmailBody = notification.NotificationEmailBody,
                    EmailSubject = notification.NotificationEmailSubject,
                    SMSText = notification.NotificationSmsText,
                    TemplateId = notification.NotificationTemplate,
                    CreatedAt = DateTimeOffset.Now
                });
            }

            businessLayer.SetUserNotification(users.ToArray());
            return true;
        }
    }
}