/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Attachment = SendGrid.Helpers.Mail.Attachment;

namespace ADX.Entities
{
    [Serializable]
    [DataContract]
    public class Notification
    {
        public Notification()
        {
        }

        public Notification(NotificationQueue notificationQueue)
        {
            Attachments = JsonConvert.DeserializeObject<List<Attachment>>(notificationQueue.JsonAttachments);
            //Dictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(notificationQueue.JsonDictionary);
            MailAddresses = JsonConvert.DeserializeObject<List<NotifyTo>>(notificationQueue.JsonMailAddresses);
            NotificationTemplate = notificationQueue.NotificationTemplate;
            NotificationEmailBody = notificationQueue.NotificationEmailBody;
            NotificationEmailSubject = notificationQueue.NotificationEmailSubject;
            NotificationSmsText = notificationQueue.NotificationSmsText;
            NotificationSmsType = notificationQueue.NotificationSmsType;
            NotifyByBcc = notificationQueue.NotifyByBcc;
        }

        [DataMember(IsRequired = false)]
        public List<Attachment> Attachments { get; set; }

        [DataMember(IsRequired = false)]
        public IDictionary<string, string> Dictionary { get; set; }

        [DataMember(IsRequired = true)]
        public List<NotifyTo> MailAddresses { get; set; }

        public string NotificationEmailBody { get; set; }

        public string NotificationEmailSubject { get; set; }

        public string NotificationSmsText { get; set; }

        public string NotificationSmsType { get; set; }

        public bool NotifyByBcc { get; set; }

        [DataMember(IsRequired = true)]
        public string NotificationTemplate { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}