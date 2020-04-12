/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Utilities;
using ADX.Utilities.Log;
using System;
using System.Configuration;
using System.ServiceModel;

namespace ADX.NotificationSystemService
{
    public class NotificationServer : IDisposable
    {
        private static readonly FileLogManager FileLogManager = new FileLogManager();
        private ServiceHost _serviceHost;

        public NotificationServer()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["GeneralCulture"]);
        }

        public void Dispose()
        {
        }

        public void Start(string[] args)
        {
            try
            {
                //FileLogManager.RegisterLog("NotificationWebService", "OnStart", args.ToString(), LogStatus.StartedService);
                _serviceHost = new ServiceHost(typeof(NotificationWebService));
                _serviceHost.Open();
            }
            catch (Exception exc)
            {
                FileLogManager.RegisterError("NotificationWebService", "OnStart", exc, LogStatus.OperationFailed);
            }
        }

        public void Stop()
        {
            try
            {
                //FileLogManager.RegisterLog("NotificationWebService", "OnStop", string.Empty, LogStatus.StoppedService);
                if (_serviceHost != null && _serviceHost.State == CommunicationState.Opened)
                {
                    _serviceHost.Close();
                }
            }
            catch (Exception exc)
            {
                FileLogManager.RegisterError("NotificationWebService", "OnStop", exc, LogStatus.OperationFailed);
            }
        }
    }
}