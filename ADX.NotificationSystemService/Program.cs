/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using System.ServiceProcess;

namespace ADX.NotificationSystemService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new NotificationService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}