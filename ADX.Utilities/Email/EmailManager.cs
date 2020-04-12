/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ADX.Utilities.Email
{
    public class EmailManager
    {
        public EmailManager(MailManagerConfiguration configuration)
        {
            Configuration = configuration;
        }

        public MailManagerConfiguration Configuration { get; set; }

        public async Task<string> SendMail(List<NotifyTo> toEmails, string emailBody, string emailSubject, List<Attachment> attachments, bool addBcc)
        {
            if (toEmails == null || toEmails.Count == 0)
            {
                return HttpStatusCode.NoContent.ToString();
            }

            MailFormat email = new MailFormat { HtmlContent = emailBody, Subject = emailSubject, ToEmails = toEmails, Attachments = attachments, AddBcc = addBcc };
            return await SendMail(email);
        }

        private async Task<string> SendMail(MailFormat email)
        {
            EmailAddress from = new EmailAddress(Configuration.FromEmail, Configuration.FromName);
            SendGridMessage msg;
            if (email.ToEmails.Count == 1)
            {
                EmailAddress to = new EmailAddress(email.ToEmails[0].EmailAddress, email.ToEmails[0].DisplayName);
                msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.PlainTextContent, email.HtmlContent);
            }
            else
            {
                List<EmailAddress> tos = new List<EmailAddress>();
                tos.AddRange(email.ToEmails.Select(e => new EmailAddress { Name = e.DisplayName, Email = e.EmailAddress }).ToList());
                msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, email.Subject, email.PlainTextContent, email.HtmlContent);
            }

            if (email.Attachments != null && email.Attachments.Any())
            {
                msg.AddAttachments(email.Attachments);
            }

            var bccs = email.AddBcc ? email.ToEmails.Where(e => string.IsNullOrEmpty(e.ManagerEmail) == false).Select(e => e.ManagerEmail).Distinct().ToList() : new List<string>();
            if (bccs.Any())
            {
                msg.AddBccs(bccs.Select(e => new EmailAddress { Email = e }).ToList());
            }

            SendGridClient client = new SendGridClient(Configuration.Host);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                var jsonResponse = JsonConvert.DeserializeObject(await response.Body.ReadAsStringAsync());
                throw new Exception(jsonResponse.ToString());
            }

            return response.StatusCode.ToString();
        }
    }
}