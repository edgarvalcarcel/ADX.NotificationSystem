/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using System;
using System.Configuration;

namespace ADX.Utilities.Email
{
    public class MailManagerConfiguration
    {
        public MailManagerConfiguration()
        {
            FromName = ConfigurationManager.AppSettings["EmailManagerConfiguration.FromName"];
            FromEmail = ConfigurationManager.AppSettings["EmailManagerConfiguration.FromEmail"];
            UserName = ConfigurationManager.AppSettings["EmailManagerConfiguration.UserName"];
            Password = ConfigurationManager.AppSettings["EmailManagerConfiguration.Password"];
            Host = ConfigurationManager.AppSettings["EmailManagerConfiguration.Host"];
            PortTls = Convert.ToInt32(ConfigurationManager.AppSettings["EmailManagerConfiguration.PortTLS"]);
            PortSsl = Convert.ToInt32(ConfigurationManager.AppSettings["EmailManagerConfiguration.PortSSL"]);
            UseSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailManagerConfiguration.UseSSL"]);
        }

        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public int PortSsl { get; set; }
        public int PortTls { get; set; }
        public string UserName { get; set; }
        public bool UseSsl { get; set; }
    }
}