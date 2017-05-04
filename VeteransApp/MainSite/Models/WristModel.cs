
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
        public bool S81 { get; set; }
        public string S81Side { get; set; }
        public bool S87 { get; set; }
        public string S87Side { get; set; }
        public string S103Other { get; set; }
        public string S103Side { get; set; }

        // Initial Right ROM
        public string S135 { get; set; }
        public string S131 { get; set; }
        public string S123 { get; set; }
        public string S127 { get; set; }
        // Post Right ROM
        public string S159 { get; set; }
        public string S160 { get; set; }
        public string S166 { get; set; }
        public string S167 { get; set; }
        // Flareup Right ROM
        public string S272 { get; set; }
        public string S265 { get; set; }
        public string S266 { get; set; }
        public string S268 { get; set; }

        // Initial Left ROM
        public string S156 { get; set; }
        public string S152 { get; set; }
        public string S144 { get; set; }
        public string S148 { get; set; }
        // Post Left ROM
        public string S179 { get; set; }
        public string S173 { get; set; }
        public string S171 { get; set; }
        public string S172 { get; set; }
        // Flareup Left ROM
        public string S283 { get; set; }
        public string S276 { get; set; }
        public string S280 { get; set; }
        public string S279 { get; set; }


        public bool S210 { get; set; }
        public bool S254 { get; set; }
        public bool S208 { get; set; }
        public bool S207 { get; set; }
        public bool S261 { get; set; }
        public bool S260 { get; set; }
        public bool S259 { get; set; }
        public bool S258 { get; set; }
        public bool S257 { get; set; }
        public bool S216 { get; set; }
        public bool S228 { get; set; }
        public bool S218 { get; set; }
        public bool S222 { get; set; }

        public string S210Side { get; set; }
        public string S254Side { get; set; }
        public string S208Side { get; set; }
        public string S207Side { get; set; }
        public string S261Side { get; set; }
        public string S260Side { get; set; }
        public string S259Side { get; set; }
        public string S258Side { get; set; }
        public string S257Side { get; set; }
        public string S216Side { get; set; }
        public string S228Side { get; set; }
        public string S218Side { get; set; }
        public string S222Side { get; set; }

    }
}