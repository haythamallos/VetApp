using System.Runtime.Serialization;

namespace RESTAPI.Models
{
    [DataContract(Name = "User")]
    public class UserModel
    {
        [DataMember(Name = "UserID")]
        public long UserID { get; set; }
        [DataMember(Name = "AuthUserid")]
        public string AuthUserid { get; set; }
        [DataMember(Name = "AuthConnection")]
        public string AuthConnection { get; set; }
        [DataMember(Name = "AuthProvider")]
        public string AuthProvider { get; set; }
        [DataMember(Name = "AuthAccessToken")]
        public string AuthAccessToken { get; set; }
        [DataMember(Name = "AuthIdToken")]
        public string AuthIdToken { get; set; }
        [DataMember(Name = "Firstname")]
        public string Firstname { get; set; }
        [DataMember(Name = "Middlename")]
        public string Middlename { get; set; }
        [DataMember(Name = "Lastname")]
        public string Lastname { get; set; }
        [DataMember(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [DataMember(Name = "EmailAddress")]
        public string EmailAddress { get; set; }
        [DataMember(Name = "Profileimageurl")]
        public string Profileimageurl { get; set; }
        [DataMember(Name = "IsDisabled")]
        public bool IsDisabled { get; set; }
        [DataMember(Name = "CanTextMsg")]
        public bool CanTextMsg { get; set; }
        [DataMember(Name = "AuthName")]
        public string AuthName { get; set; }
        [DataMember(Name = "AuthNickname")]
        public string AuthNickname { get; set; }

    }
}
