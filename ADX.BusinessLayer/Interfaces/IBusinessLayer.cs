/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities.DomainModel;
using System.Collections.Generic;

namespace ADX.BusinessLayer
{
    internal interface IBusinessLayer
    {
        /// <summary>
        /// Adds the mail template.
        /// </summary>
        /// <param name="mailTemplates">The mail templates.</param>
        void AddMailTemplate(params MailTemplate[] mailTemplates);

        /// <summary>
        /// Gets all mail templates.
        /// </summary>
        /// <returns></returns>
        IList<MailTemplate> GetAllMailTemplates();

        /// <summary>
        /// Gets the mail template by identifier.
        /// </summary>
        /// <param name="mailTemplateId">The mail template identifier.</param>
        /// <returns></returns>
        MailTemplate GetMailTemplateById(string mailTemplateId);

        /// <summary>
        /// Gets the mail template by identifier with notifications.
        /// </summary>
        /// <param name="mailTemplateId">The mail template identifier.</param>
        /// <returns></returns>
        MailTemplate GetMailTemplateByIdWithNotifications(string mailTemplateId);

        /// <summary>
        /// Gets the notifications by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<NotificationByUser> GetNotificationsByUserId(string userId);

        /// <summary>
        /// Gets the notifications by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notificationTemplateId">The notification template identifier.</param>
        /// <returns></returns>
        NotificationByUser GetNotificationsByUserId(string userId, string notificationTemplateId);

        /// <summary>
        /// Removes the mail template.
        /// </summary>
        /// <param name="mailTemplates">The mail templates.</param>
        void RemoveMailTemplate(params MailTemplate[] mailTemplates);

        /// <summary>
        /// Sets the user notification.
        /// </summary>
        /// <param name="userNotification">The user notification.</param>
        /// <returns></returns>
        bool SetUserNotification(params UserNotification[] userNotification);

        /// <summary>
        /// Updates the mail template.
        /// </summary>
        /// <param name="mailTemplates">The mail templates.</param>
        void UpdateMailTemplate(params MailTemplate[] mailTemplates);
    }
}