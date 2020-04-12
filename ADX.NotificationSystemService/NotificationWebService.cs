/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.BusinessLayer;
using ADX.Entities;
using ADX.Utilities;
using ADX.Utilities.Log;
using ADX.Utilities.Queue;
using System;

namespace ADX.NotificationSystemService
{
    public class NotificationWebService : INotificationWebService
    {
        public delegate bool ExecutesNotificationAsync(Notification notification);

        public bool RegisterNotification(Notification notification)
        {
            ExecutesNotificationAsync execute = ExecuteNotification;
            execute.BeginInvoke(notification, null, null);
            return true;
        }

        private static bool ExecuteNotification(Notification notification)
        {
            var fileLogManager = new FileLogManager();
            try
            {
                fileLogManager.RegisterLog("NotificationWebService", "RegisterNotification", notification.ToString(), LogStatus.Notification);
                MailTemplateManager mailTemplateManager = new MailTemplateManager();
                NotificationQueue newNotification = mailTemplateManager.GetNotificationQueue(notification);
                notification.NotificationEmailBody = newNotification.NotificationEmailBody;
                notification.NotificationEmailSubject = newNotification.NotificationEmailSubject;
                notification.NotificationSmsText = newNotification.NotificationSmsText;
                notification.NotificationSmsType = newNotification.NotificationSmsType;
                // Write the notification to the database
                UserNotificationManager userNotificationManager = new UserNotificationManager();
                userNotificationManager.SetUserNotification(notification);
                // Write message to the queue
                QueueManager.SendMessage(newNotification);
                return true;
            }
            catch (Exception exc)
            {
                fileLogManager.RegisterError("NotificationWebService", "RegisterNotification", exc, LogStatus.OperationFailed);
            }

            return false;
        }
    }
}