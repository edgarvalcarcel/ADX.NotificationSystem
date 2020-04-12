/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Entities;
using ADX.Notifications.Business;
using ADX.Utilities;
using ADX.Utilities.Log;
using System;
using System.Configuration;
using System.Messaging;

namespace ADX.Dispatcher
{
    public class ListenerServer : IDisposable
    {
        private static readonly FileLogManager FileLogManager = new FileLogManager();
        private static readonly NotificationManager NotificationManager = new NotificationManager();
        private readonly string _queuePath;

        public ListenerServer()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["GeneralCulture"]);
            _queuePath = ConfigurationManager.AppSettings["adxQueue"];
        }

        public void Dispose()
        {
        }

        public void Start(string[] args)
        {
            try
            {
                //FileLogManager.RegisterLog("ListenerServer", "OnStart", args.ToString(), LogStatus.StartedService);
                if (!string.IsNullOrEmpty(_queuePath))
                {
                    // Create an instance of MessageQueue. Set its formatter.
                    MessageQueue adxQueue = new MessageQueue(_queuePath)
                    {
                        Formatter = new XmlMessageFormatter(new[] { typeof(NotificationQueue) })
                    };

                    // Add an event handler for the ReceiveCompleted event.
                    adxQueue.ReceiveCompleted += ReceiveMessageCompleted;

                    // Begin the asynchronous receive operation.
                    adxQueue.BeginReceive();
                }
            }
            catch (Exception exc)
            {
                FileLogManager.RegisterError("ListenerServer", "OnStart", exc, LogStatus.OperationFailed);
            }
        }

        public void Stop()
        {
            //FileLogManager.RegisterLog("ListenerServer", "OnStop", string.Empty, LogStatus.StoppedService);
        }

        private static async void ReceiveMessageCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            // Connect to the queue.
            MessageQueue mq = (MessageQueue)source;

            // End the asynchronous Receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            if (m.Body is NotificationQueue msg)
            {
                Notification notification = new Notification(msg);
                await FileLogManager.RegisterLogAsync("ADX.Dispatcher.Listener", "ReceiveMessageCompleted", notification.ToString(), LogStatus.Notification);

                // Pending send message for processing
                NotificationManager.ProcessNotification(notification);
            }

            // Restart the asynchronous Receive operation.
            mq.BeginReceive();
        }
    }
}