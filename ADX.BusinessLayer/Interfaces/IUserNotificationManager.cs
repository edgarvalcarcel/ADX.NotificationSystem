/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;

namespace ADX.BusinessLayer
{
    public interface IUserNotificationManager
    {
        /// <summary>
        /// Sets the user notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        bool SetUserNotification(Notification notification);
    }
}