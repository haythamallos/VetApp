
namespace MainSite.Models
{
    public class FootModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

        public bool S62 { get; set; }
        public string S62Side { get; set; }
        public bool S63 { get; set; }
        public string S63Side { get; set; }
        public bool S64 { get; set; }
        public string S64Side { get; set; }
        public bool S65 { get; set; }
        public string S65Side { get; set; }
        public bool S66 { get; set; }
        public string S66Side { get; set; }
        public bool S67 { get; set; }
        public string S67Side { get; set; }
        public bool S68 { get; set; }
        public string S68Side { get; set; }
        public bool S69 { get; set; }
        public string S69Side { get; set; }
        public bool S70 { get; set; }
        public string S70Side { get; set; }
        public bool S71 { get; set; }
        public string S71Side { get; set; }
        public bool S72 { get; set; }
        public string S72Side { get; set; }
        public string S112Other { get; set; }
        public string S112Side { get; set; }

    }
}