/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.DataAccessLayer.Repositories;
using ADX.DataAccessLayer.Repositories.Interfaces;
using ADX.Entities.DomainModel;
using ADX.Utilities;
using ADX.Utilities.Log;
using System;
using System.Collections.Generic;

namespace ADX.BusinessLayer
{
    /// <summary>
    /// Control the bussiness access to data
    /// </summary>
    /// <seealso cref="ADX.BusinessLayer.IBusinessLayer"/>
    internal class BusinessLayer : IBusinessLayer
    {
        private readonly FileLogManager _fileLogManager;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly INotificationByUserRepository _notificationByUserRepository;
        private readonly IUserNotificationRepository _userNotificationRepository;

        public BusinessLayer()
        {
            _fileLogManager = new FileLogManager();
            _mailTemplateRepository = new MailTemplateRepository();
            _notificationByUserRepository = new NotificationByUserRepository();
            _userNotificationRepository = new UserNotificationRepository();
        }

        public BusinessLayer(IMailTemplateRepository mailTemplateRepository,
            INotificationByUserRepository notificationByUserRepository,
            IUserNotificationRepository userNotificationRepository)
        {
            _fileLogManager = new FileLogManager();
            _mailTemplateRepository = mailTemplateRepository;
            _notificationByUserRepository = notificationByUserRepository;
            _userNotificationRepository = userNotificationRepository;
        }

        public void AddMailTemplate(params MailTemplate[] mailTemplates)
        {
            /* Validation and error handling omitted */
            _mailTemplateRepository.Add(mailTemplates);
        }

        public IList<MailTemplate> GetAllMailTemplates()
        {
            return _mailTemplateRepository.GetAll();
        }

        public MailTemplate GetMailTemplateById(string mailTemplateId)
        {
            return _mailTemplateRepository.GetSingle(d => d.Id.Equals(mailTemplateId));
        }

        public MailTemplate GetMailTemplateByIdWithNotifications(string mailTemplateId)
        {
            return _mailTemplateRepository.GetSingle(d => d.Id.Equals(mailTemplateId),
                d => d.NotificationByUsers); //include related notifications by user
        }

        public IList<NotificationByUser> GetNotificationsByUserId(string userId)
        {
            return _notificationByUserRepository.GetList(u => u.UserId == userId);
        }

        public NotificationByUser GetNotificationsByUserId(string userId, string notificationTemplateId)
        {
            return _notificationByUserRepository.GetSingle(u =>
                u.UserId == userId &&
                u.MailTemplateId.Equals(notificationTemplateId, StringComparison.OrdinalIgnoreCase));
        }

        public void RemoveMailTemplate(params MailTemplate[] mailTemplates)
        {
            /* Validation and error handling omitted */
            _mailTemplateRepository.Remove(mailTemplates);
        }

        public bool SetUserNotification(params UserNotification[] userNotification)
        {
            try
            {
                _userNotificationRepository.Add(userNotification);
                return true;
            }
            catch (Exception exc)
            {
                _fileLogManager.RegisterError("SetUserNotification", exc.Message, exc, LogStatus.OperationFailed);
            }

            return false;
        }

        public void UpdateMailTemplate(params MailTemplate[] mailTemplates)
        {
            /* Validation and error handling omitted */
            _mailTemplateRepository.Update(mailTemplates);
        }
    }
}