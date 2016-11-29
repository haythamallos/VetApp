using System.Runtime.Serialization;

namespace Vetapp.Client.ProxyCore
{
    [DataContract(Name = "User")]
    public class UserProxy
    {
        [DataMember(Name = "UserID")]
        public long UserID { get; set; }
        [DataMember(Name = "Firstname")]
        public string Firstname { get; set; }
        [DataMember(Name = "Middlename")]
        public string Middlename { get; set; }
        [DataMember(Name = "Lastname")]
        public string Lastname { get; set; }
        [DataMember(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [DataMember(Name = "Username")]
        public string Username { get; set; }
        [DataMember(Name = "Passwd")]
        public string Passwd { get; set; }
        [DataMember(Name = "PictureUrl")]
        public string PictureUrl { get; set; }
        [DataMember(Name = "IsDisabled")]
        public bool IsDisabled { get; set; }
        [DataMember(Name = "Picture")]
        public byte[] Picture { get; set; }
    }
}
