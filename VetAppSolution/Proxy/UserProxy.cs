namespace Vetapp.Client.Proxy
{
    public class UserProxy
    {
        public long UserID { get; set; }
        public string AuthUserid { get; set; }
        public string AuthConnection { get; set; }
        public string AuthProvider { get; set; }
        public string AuthAccessToken { get; set; }
        public string AuthIdToken { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Profileimageurl { get; set; }
        public bool IsDisabled { get; set; }
        public bool CanTextMsg { get; set; }

        // custom
        public string AuthName { get; set; }
        public string AuthNickname { get; set; }

    }
}
