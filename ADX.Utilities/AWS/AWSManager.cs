/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ADX.Utilities.AWS
{
    public class AWSManager
    {
        private readonly AmazonSimpleNotificationServiceClient _sns;

        public AWSManager()
        {
            _sns = new AmazonSimpleNotificationServiceClient();
        }

        public string SendSMS(List<string> cellphoneNumbers, string message, string smsType)
        {
            Dictionary<String, MessageAttributeValue> smsAttributes = new Dictionary<String, MessageAttributeValue>
            {
                {
                    "AWS.SNS.SMS.SMSType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = ValidateType(smsType)
                    }
                }
            };

            message = SetPrefixToMessage(message);
            byte[] bytes = Encoding.Default.GetBytes(message);
            List<string> messageId = new List<string>();
            foreach (var cellphoneNumber in cellphoneNumbers)
            {
                var response = _sns.Publish(new PublishRequest
                {
                    Message = Encoding.UTF8.GetString(bytes),
                    MessageAttributes = smsAttributes,
                    PhoneNumber = ValidateNumber(cellphoneNumber)
                });

                messageId.Add(response.MessageId);
            }

            return JsonConvert.SerializeObject(messageId);
        }

        private string CleanStringOfNonDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = new string(s.Where(char.IsDigit).ToArray());
            return cleaned;
        }

        private string SetPrefixToMessage(string message)
        {
            string prefix = ConfigurationManager.AppSettings["PrefixTemplate"];
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = "ADX 365.";
            }

            return $"{prefix} {message}";
        }

        private string ValidateNumber(string cellphoneNumber)
        {
            string number = CleanStringOfNonDigits(cellphoneNumber);
            if (number.Length == 10)
            {
                return $"+1{number}"; // By Default is a USA number
            }

            return $"+{number}";
        }

        private string ValidateType(string smsType)
        {
            switch (smsType.ToUpper())
            {
                case "TRANSACTIONAL":
                case "T":
                    return "Transactional";

                case "PROMOTIONAL":
                case "P":
                    return "Promotional";

                default:
                    return "Promotional";
            }
        }
    }
}