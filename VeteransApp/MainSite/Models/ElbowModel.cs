
namespace MainSite.Models
{
    public class ElbowModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

        public string Side { get; set; }

        public bool S72 { get; set; }
        public string S72Side { get; set; }
        public bool S73 { get; set; }
        public string S73Side { get; set; }
        public bool S74 { get; set; }
        public string S74Side { get; set; }
        public bool S75 { get; set; }
        public string S75Side { get; set; }
        public bool S76 { get; set; }
        public string S76Side { get; set; }
        public bool S77 { get; set; }
        public string S77Side { get; set; }
        public bool S78 { get; set; }
        public string S78Side { get; set; }
        public bool S79 { get; set; }
        public string S79Side { get; set; }
        public bool S80 { get; set; }
        public string S80Side { get; set; }
        public string S81Other { get; set; }

        // Initial Right ROM
        public string S130 { get; set; }
        public string S134 { get; set; }
        public string S137 { get; set; }
        public string S140 { get; set; }
        // Post Right ROM
        public string S170 { get; set; }
        public string S169 { get; set; }
        public string S171 { get; set; }
        public string S172 { get; set; }
        // Flareup Right ROM
        public string S292 { get; set; }
        public string S290 { get; set; }
        public string S288 { get; set; }
        public string S286 { get; set; }

        // Initial Left ROM
        public string S122 { get; set; }
        public string S112 { get; set; }
        public string S113 { get; set; }
        public string S116 { get; set; }
        // Post Left ROM
        public string S154 { get; set; }
        public string S155 { get; set; }
        public string S148 { get; set; }
        public string S147 { get; set; }
        // Flareup Left ROM
        public string S260 { get; set; }
        public string S255 { get; set; }
        public string S253 { get; set; }
        public string S254 { get; set; }

    }
}