using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public long CurrentRatingBack { get; set; }
        public long CurrentRatingShoulder { get; set; }
        public long CurrentRatingNeck { get; set; }
        public bool HasRatingBack { get; set; }
        public bool HasRatingShoulder { get; set; }
        public bool HasRatingNeck { get; set; }

    }
}