using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Core.Data
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}