/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Utilities;
using ADX.Utilities.Log;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;

namespace ADX.Dispatcher
{
    public partial class ListenerService : ServiceBase
    {
        private static readonly FileLogManager FileLogManager = new FileLogManager();

        public ListenerService()
        {
            InitializeComponent();
            ServiceName = "ADX.DispatcherSystem";
            CreateEventLog();
            ListenerServer = new ListenerServer();
        }

        public ListenerServer ListenerServer { get; set; }

        protected override void OnStart(string[] args)
        {
            ListenerServer.Start(args);
        }

        protected override void OnStop()
        {
            ListenerServer.Stop();
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