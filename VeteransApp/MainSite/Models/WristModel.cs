
namespace MainSite.Models
{
    public class WristModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

    }
}