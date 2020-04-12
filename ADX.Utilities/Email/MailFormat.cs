/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace ADX.Utilities.Email
{
    internal class MailFormat
    {
        public List<Attachment> Attachments { get; set; }
        public string HtmlContent { get; set; }
        public string PlainTextContent { get; set; }
        public string Subject { get; set; }
        public List<NotifyTo> ToEmails { get; set; }
        public bool AddBcc { get; set; }
    }
}