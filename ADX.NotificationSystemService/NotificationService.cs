/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Utilities;
using ADX.Utilities.Log;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;

namespace ADX.NotificationSystemService
{
    public sealed partial class NotificationService : ServiceBase
    {
        private static readonly FileLogManager FileLogManager = new FileLogManager();

        public NotificationService()
        {
            InitializeComponent();
            ServiceName = "ADX.NotificationSystem";
            CreateEventLog();
            NotificationServer = new NotificationServer();
        }

        public NotificationServer NotificationServer { get; set; }

        protected override void OnStart(string[] args)
        {
            NotificationServer.Start(args);
        }

        protected override void OnStop()
        {
            NotificationServer.Stop();
        }

        private void CreateEventLog()
        {
            try
            {
                EventLog.Source = ConfigurationManager.AppSettings["EventLogSource"];
                EventLog.Log = ConfigurationManager.AppSettings["EventLogName"];
                CanHandlePowerEvent = true;
                CanHandleSessionChangeEvent = true;
                CanPauseAndContinue = true;
                CanShutdown = true;
                CanStop = true;

                if (!EventLog.SourceExists(EventLog.Source))
                {
                    EventLog.CreateEventSource(EventLog.Source, EventLog.Log);
                }
            }
            catch (Exception exc)
            {
                FileLogManager.RegisterError(ServiceName, "CreateEventLog", exc, LogStatus.OperationFailed);
            }
        }
    }
}