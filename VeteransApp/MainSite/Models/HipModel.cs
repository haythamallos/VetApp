
namespace MainSite.Models
{
    public class HipModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

        public string Side { get; set; }

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
        public bool S73 { get; set; }
        public string S73Side { get; set; }
        public bool S74 { get; set; }
        public string S74Side { get; set; }
        public string S75Other { get; set; }

        // Initial Right ROM
        public string S123 { get; set; }
        public string S113 { get; set; }
        public string S114 { get; set; }
        public string S117 { get; set; }
        public string S103 { get; set; }
        public string S109 { get; set; }
        // Post Right ROM
        public string S159 { get; set; }
        public string S158 { get; set; }
        public string S160 { get; set; }
        public string S161 { get; set; }
        public string S167 { get; set; }
        public string S168 { get; set; }
        // Flareup Right ROM
        public string S282 { get; set; }
        public string S283 { get; set; }
        public string S272 { get; set; }
        public string S278 { get; set; }
        public string S273 { get; set; }
        public string S275 { get; set; }

        // Initial Left ROM
        public string S154 { get; set; }
        public string S144 { get; set; }
        public string S145 { get; set; }
        public string S148 { get; set; }
        public string S134 { get; set; }
        public string S140 { get; set; }
        // Post Left ROM
        public string S197 { get; set; }
        public string S198 { get; set; }
        public string S191 { get; set; }
        public string S188 { get; set; }
        public string S189 { get; set; }
        public string S190 { get; set; }
        // Flareup Left ROM
        public string S322 { get; set; }
        public string S313 { get; set; }
        public string S311 { get; set; }
        public string S312 { get; set; }
        public string S318 { get; set; }
        public string S317 { get; set; }

    }
}