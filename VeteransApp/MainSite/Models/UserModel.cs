
using System;

namespace MainSite.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public int CurrentRating { get; set; }
        public string SSN { get; set; }
        public bool HasCurrentRating { get; set; }
        public long InternalCalculatedRating { get; set; }
        public bool IsRatingProfileFinished { get; set; }
        public DateTime DateCreated { get; set; }
        public string CookieID { get; set; }
        public long UserRoleID { get; set; }
    }
}