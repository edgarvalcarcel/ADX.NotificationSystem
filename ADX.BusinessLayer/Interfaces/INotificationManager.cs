/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using System.Threading.Tasks;

namespace ADX.Notifications.Business
{
    public interface INotificationManager
    {
        /// <summary>
        /// Dispatches the notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        Task<bool> DispatchNotification(Notification notification);
    }
}