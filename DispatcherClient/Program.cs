/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using ADX.Dispatcher;
using System;

namespace DispatcherClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ListenerServer service = new ListenerServer();
            service.Start(args);
            Console.WriteLine(Messages.Service_Started);
            Console.Read();
            service.Stop();
        }
    }
}