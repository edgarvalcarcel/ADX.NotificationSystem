/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using System.Configuration;
using System.Messaging;

namespace ADX.Utilities.Queue
{
    public class QueueManager
    {
        private static readonly string Path = ConfigurationManager.AppSettings["adxQueue"];

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="notificationQueue">The notification queue.</param>
        /// <returns></returns>
        public static bool SendMessage(NotificationQueue notificationQueue)
        {
            var messageQueue = !MessageQueue.Exists(Path) ? MessageQueue.Create(Path) : new MessageQueue(Path);
            messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(NotificationQueue) });
            messageQueue.Label = notificationQueue.NotificationTemplate;
            messageQueue.Send(notificationQueue);
            messageQueue.Close();
            return true;
        }
    }
}