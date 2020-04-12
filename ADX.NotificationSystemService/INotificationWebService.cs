/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using System.ServiceModel;

namespace ADX.NotificationSystemService
{
    /// <summary>
    /// Web service to send notifications through email or SMS
    /// </summary>
    [ServiceContract]
    public interface INotificationWebService
    {
        /// <summary>
        /// Registers the notification to be sent
        /// </summary>
        /// <param name="notification">The notification information.</param>
        /// <returns>Indicates that the notification has been received</returns>
        [OperationContract]
        bool RegisterNotification(Notification notification);
    }
}