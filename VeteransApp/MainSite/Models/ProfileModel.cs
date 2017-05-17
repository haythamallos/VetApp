
using System.Collections.Generic;
using Vetapp.Engine.DataAccessLayer.Data;

namespace MainSite.Models
{
    public class ProfileModel
    {
        public UserModel userModel { get; set; }
        public List<UserDisability> lstUserDisability { get; set; }
        public bool IsAdmin { get; set; }
        public string RoleChoice { get; set; }
        public ProfileModel()
        {
        }
    }

    public class UserDisability
    {
        public JctUserContentType jctUserContentType { get; set; }
        public ContentType contentType { get; set; }
    }
}