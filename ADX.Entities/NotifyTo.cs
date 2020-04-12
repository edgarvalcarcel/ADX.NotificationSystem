/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using SendGrid.Helpers.Mail;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ADX.Entities
{
    [Serializable]
    [DataContract]
    public class NotifyTo
    {
        public NotifyTo()
        {
        }

        public NotifyTo(string userId, UserType userType, string cellphoneNumber, string emailAddress, bool cellphoneVerified)
        {
            CellphoneNumber = cellphoneNumber;
            EmailAddress = emailAddress;
            UserId = userId;
            UserType = userType;
            CellphoneVerified = cellphoneVerified;
        }

        public NotifyTo(string userId, UserType userType, string cellphoneNumber, string emailAddress, string displayName, bool cellphoneVerified)
        {
            CellphoneNumber = cellphoneNumber;
            DisplayName = displayName;
            EmailAddress = emailAddress;
            UserId = userId;
            UserType = userType;
            CellphoneVerified = cellphoneVerified;
        }

        public NotifyTo(string userId, UserType userType, string cellphoneNumber, string emailAddress, string displayName, Encoding displayNameEncoding, bool cellphoneVerified)
        {
            CellphoneNumber = cellphoneNumber;
            DisplayName = displayNameEncoding.GetString(Convert.FromBase64String(displayName));
            EmailAddress = emailAddress;
            UserId = userId;
            UserType = userType;
            CellphoneVerified = cellphoneVerified;
        }

        [DataMember(IsRequired = true)]
        public bool ByEmail { get; set; }

        [DataMember(IsRequired = true)]
        public bool BySms { get; set; }

        [DataMember(IsRequired = false)]
        public string CellphoneNumber { get; set; }

        [DataMember(IsRequired = true)]
        public bool CellphoneVerified { get; set; }

        [DataMember(IsRequired = true)]
        public string DisplayName { get; set; }

        [DataMember(IsRequired = true)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [DataMember(IsRequired = true)]
        public string UserId { get; set; }

        [DataMember(IsRequired = true)]
        public UserType UserType { get; set; }
        [DataMember(IsRequired = false)]
        public string ManagerEmail { get; set; }
    }
}