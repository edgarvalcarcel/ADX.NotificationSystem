/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using Newtonsoft.Json;

namespace ADX.Entities
{
    public class NotificationQueue
    {
        public NotificationQueue(Notification notification)
        {
            JsonAttachments = JsonConvert.SerializeObject(notification.Attachments);
            JsonDictionary = JsonConvert.SerializeObject(notification.Dictionary);
            JsonMailAddresses = JsonConvert.SerializeObject(notification.MailAddresses);
            NotificationTemplate = notification.NotificationTemplate;
        }

        private NotificationQueue()
        {
        }

        public string JsonAttachments { get; set; }
        public string JsonDictionary { get; set; }
        public string JsonMailAddresses { get; set; }
        public string NotificationEmailBody { get; set; }
        public string NotificationEmailSubject { get; set; }
        public string NotificationSmsText { get; set; }
        public string NotificationSmsType { get; set; }
        public string NotificationTemplate { get; set; }
        public bool NotifyByBcc { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}