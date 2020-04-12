/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using ADX.Utilities;
using ADX.Utilities.AWS;
using ADX.Utilities.Email;
using ADX.Utilities.Log;
using System;
using System.Linq;

namespace ADX.Notifications.Business
{
    public class NotificationManager
    {
        private static readonly FileLogManager FileLogManager = new FileLogManager();

        public NotificationManager()
        {
        }

        public async void ProcessNotification(Notification notification)
        {
            try
            {
                string sendEmailStatus = "Email Body Empty";
                if (string.IsNullOrEmpty(notification.NotificationEmailBody) == false)
                {
                    var notifyByEmail = notification.MailAddresses.Where(n => n.ByEmail && !string.IsNullOrEmpty(n.EmailAddress)).ToList();
                    EmailManager emailManager = new EmailManager(new MailManagerConfiguration());
                    sendEmailStatus = await emailManager.SendMail(notifyByEmail,
                        notification.NotificationEmailBody, notification.NotificationEmailSubject,
                        notification.Attachments, notification.NotifyByBcc);
                }

                FileLogManager.RegisterLog("ProcessNotification.Email", "SendEmail", $"Result: {sendEmailStatus}. Content: {notification}", LogStatus.Notification);
            }
            catch (Exception exception)
            {
                FileLogManager.RegisterError("ProcessNotification.Email", exception.Message, exception, LogStatus.OperationFailed);
            }

            try
            {
                string smsId = "SMS Text Empty";
                if (string.IsNullOrEmpty(notification.NotificationSmsText) == false)
                {
                    var notifyBySms = notification.MailAddresses
                        .Where(n => n.BySms && !string.IsNullOrEmpty(n.CellphoneNumber)).ToList();
                    AWSManager smsManager = new AWSManager();
                    smsId = smsManager.SendSMS(notifyBySms.Select(n => n.CellphoneNumber).ToList(),
                        notification.NotificationSmsText, notification.NotificationSmsType);
                }

                FileLogManager.RegisterLog("ProcessNotification.SMS", "SendSms", $"SMS Id: {smsId}. Content: {notification}", LogStatus.Notification);
            }
            catch (Exception exception)
            {
                FileLogManager.RegisterError("ProcessNotification.SMS", exception.Message, exception, LogStatus.OperationFailed);
            }
        }
    }
}