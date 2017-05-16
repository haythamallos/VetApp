
namespace MainSite.Models
{
    public class KneeModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

        public bool S57 { get; set; }
        public string S57Side { get; set; }
        public bool S58 { get; set; }
        public string S58Side { get; set; }
        public bool S59 { get; set; }
        public string S59Side { get; set; }
        public bool S60 { get; set; }
        public string S60Side { get; set; }
        public bool S61 { get; set; }
        public string S61Side { get; set; }
        public bool S62 { get; set; }
        public string S62Side { get; set; }
        public bool S63 { get; set; }
        public string S63Side { get; set; }
        public bool S64 { get; set; }
        public string S64Side { get; set; }
        public bool S65 { get; set; }
        public string S65Side { get; set; }
        public bool S79 { get; set; }
        public string S79Side { get; set; }
        public bool S127 { get; set; }
        public string S127Side { get; set; }
        public bool S121 { get; set; }
        public string S121Side { get; set; }
        public bool S115 { get; set; }
        public string S115Side { get; set; }
        public bool S109 { get; set; }
        public string S109Side { get; set; }
        public bool S103 { get; set; }
        public string S103Side { get; set; }
        public bool S85 { get; set; }
        public string S85Side { get; set; }
        public bool S97 { get; set; }
        public string S97Side { get; set; }
        public bool S91 { get; set; }
        public string S91Side { get; set; }
        public string S165Other { get; set; }
        public string S165Side { get; set; }

        // Initial Right ROM
        public string S171 { get; set; }
        public string S175 { get; set; }
        // Post Right ROM
        public string S203 { get; set; }
        public string S202 { get; set; }
        // Flareup Right ROM
        public string S313 { get; set; }
        public string S314 { get; set; }

        // Initial Left ROM
        public string S146 { get; set; }
        public string S142 { get; set; }
        // Post Left ROM
        public string S187 { get; set; }
        public string S188 { get; set; }
        // Flareup Left ROM
        public string S287 { get; set; }
        public string S284 { get; set; }


        public bool S226 { get; set; }
        public bool S270 { get; set; }
        public bool S224 { get; set; }
        public bool S223 { get; set; }
        public bool S278 { get; set; }
        public bool S277 { get; set; }
        public bool S276 { get; set; }
        public bool S275 { get; set; }
        public bool S274 { get; set; }
        public bool S232 { get; set; }
        public bool S244 { get; set; }
        public bool S234 { get; set; }
        public bool S238 { get; set; }

        public string S226Side { get; set; }
        public string S270Side { get; set; }
        public string S224Side { get; set; }
        public string S223Side { get; set; }
        public string S278Side { get; set; }
        public string S277Side { get; set; }
        public string S276Side { get; set; }
        public string S275Side { get; set; }
        public string S274Side { get; set; }
        public string S232Side { get; set; }
        public string S244Side { get; set; }
        public string S234Side { get; set; }
        public string S238Side { get; set; }

        public bool S505 { get; set; }

        public bool S519 { get; set; }
        public bool S531 { get; set; }
        public bool S520 { get; set; }
        public bool S524 { get; set; }

        public string S519Choice { get; set; }
        public string S531Choice { get; set; }
        public string S520Choice { get; set; }
        public string S524Choice { get; set; }

        public bool S462 { get; set; }
        public string surgeryDate { get; set; }
        public string S11ASide { get; set; }
        public string S11ASurgeryType { get; set; }


    }
}