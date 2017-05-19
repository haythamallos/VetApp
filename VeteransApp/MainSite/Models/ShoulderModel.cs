
namespace MainSite.Models
{
    public class ShoulderModel : IBaseModel
    {
        public bool IsFormReadonly { get; set; }

        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

        public string Side { get; set; }
        public string LeftOrRightHanded { get; set; }

        public bool S63 { get; set; }

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
        public bool S111 { get; set; }
        public string S111Side { get; set; }
        public bool S105 { get; set; }
        public string S105Side { get; set; }
        public bool S99 { get; set; }
        public string S99Side { get; set; }
        public bool S93 { get; set; }
        public string S93Side { get; set; }
        public bool S87 { get; set; }
        public string S87Side { get; set; }
        public string S128Other { get; set; }
        public string S128Side { get; set; }

        //public bool DoInitialROMLeft { get; set; }
        //public bool DoInitialROMRight { get; set; }

        // Initial Right ROM
        public string S156 { get; set; }
        public string S152 { get; set; }
        public string S144 { get; set; }
        public string S148 { get; set; }
        // Post Right ROM
        public string S180 { get; set; }
        public string S181 { get; set; }
        public string S187 { get; set; }
        public string S188 { get; set; }
        // Flareup Right ROM
        public string S293 { get; set; }
        public string S286 { get; set; }
        public string S287 { get; set; }
        public string S289 { get; set; }

        // Initial Left ROM
        public string S177 { get; set; }
        public string S173 { get; set; }
        public string S165 { get; set; }
        public string S169 { get; set; }
        // Post Left ROM
        public string S200 { get; set; }
        public string S194 { get; set; }
        public string S192 { get; set; }
        public string S193 { get; set; }
        // Flareup Left ROM
        public string S304 { get; set; }
        public string S297 { get; set; }
        public string S301 { get; set; }
        public string S300 { get; set; }

        public bool S231 { get; set; }
        public bool S275 { get; set; }
        public bool S229 { get; set; }
        public bool S228 { get; set; }
        public bool S282 { get; set; }
        public bool S281 { get; set; }
        public bool S280 { get; set; }
        public bool S279 { get; set; }
        public bool S278 { get; set; }
        public bool S237 { get; set; }
        public bool S249 { get; set; }
        public bool S239 { get; set; }
        public bool S243 { get; set; }

        public string S231Side { get; set; }
        public string S275Side { get; set; }
        public string S229Side { get; set; }
        public string S228Side { get; set; }
        public string S282Side { get; set; }
        public string S281Side { get; set; }
        public string S280Side { get; set; }
        public string S279Side { get; set; }
        public string S278Side { get; set; }
        public string S237Side { get; set; }
        public string S249Side { get; set; }
        public string S239Side { get; set; }
        public string S243Side { get; set; }

        public bool S10A { get; set; }
        public string S10ASide { get; set; }
        public string S10C { get; set; }

    }
}