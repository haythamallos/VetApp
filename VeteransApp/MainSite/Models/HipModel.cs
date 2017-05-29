
namespace MainSite.Models
{
    public class HipModel : IBaseModel
    {
        public string VarianceHistory { get; set; }
        public string VarianceHistoryWriteIn { get; set; }
        public string VarianceFlareUps { get; set; }
        public string VarianceFlareUpsWriteIn { get; set; }
        public string VarianceFunctionLoss { get; set; }
        public string VarianceFunctionLossWriteIn { get; set; }

        public bool IsFormReadonly { get; set; }

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
        public string S75Side { get; set; }

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

        public bool S262 { get; set; }
        public bool S216 { get; set; }
        public bool S217 { get; set; }
        public string S217Side { get; set; }
        public bool S261 { get; set; }
        public string S261Side { get; set; }
        public bool S215 { get; set; }
        public string S215Side { get; set; }
        public bool S214 { get; set; }
        public string S214Side { get; set; }
        public bool S269 { get; set; }
        public string S269Side { get; set; }
        public bool S268 { get; set; }
        public string S268Side { get; set; }
        public bool S267 { get; set; }
        public string S267Side { get; set; }
        public bool S266 { get; set; }
        public string S266Side { get; set; }
        public bool S265 { get; set; }
        public string S265Side { get; set; }
        public bool S223 { get; set; }
        public string S223Side { get; set; }
        public bool S235 { get; set; }
        public string S235Side { get; set; }
        public bool S225 { get; set; }
        public string S225Side { get; set; }
        public bool S229 { get; set; }
        public string S229Side { get; set; }
        public bool S264 { get; set; }
        public string S264Other { get; set; }

        public bool S416 { get; set; }

        public bool S418 { get; set; }
        public bool S430 { get; set; }
        public bool S419 { get; set; }
        public bool S423 { get; set; }

        public string S418Choice { get; set; }
        public string S430Choice { get; set; }
        public string S419Choice { get; set; }
        public string S423Choice { get; set; }

    }
}