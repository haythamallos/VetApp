
using System.Collections.Generic;

namespace MainSite.Models
{
    public class ProfileModel
    {
        public UserModel userModel { get; set; }
        public List<DisabilityItem> lstDisabilityItem { get; set; }
        public bool IsAdmin { get; set; }
        public string RoleChoice { get; set; }
        public ProfileModel()
        {
            lstDisabilityItem = new List<DisabilityItem>();
        }
    }

    public class DisabilityItem
    {
        public long jctUserContentTypeID { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public long SideID { get; set; }
    }
}