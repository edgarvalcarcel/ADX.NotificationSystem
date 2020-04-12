/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.NotificationSystemService;
using System;

namespace NotificationClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NotificationServer service = new NotificationServer();
            service.Start(args);
            Console.WriteLine(Messages.Service_Started);
            Console.Read();
            service.Stop();
        }
    }
}