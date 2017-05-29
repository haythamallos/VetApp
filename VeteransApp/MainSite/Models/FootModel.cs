
namespace MainSite.Models
{
    public class FootModel : IBaseModel
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

        public bool HasPainOnUse { get; set; }
        public bool HasSwelling { get; set; }
        public bool HasCalluses { get; set; }
        public bool HasTenderness { get; set; }
        public bool HasMarkedDeformity { get; set; }

        public bool DeviceArchSupport { get; set; }
        public bool DeviceBuiltupShoes { get; set; }
        public bool DeviceOrthotics { get; set; }


        // contributing factors
        public bool S430 { get; set; }
        public bool S474 { get; set; }
        public bool S428 { get; set; }
        public bool S427 { get; set; }
        public bool S490 { get; set; }
        public bool S481 { get; set; }
        public bool S489 { get; set; }
        public bool S485 { get; set; }
        public bool S480 { get; set; }
        public bool S479 { get; set; }
        public bool S478 { get; set; }
        public bool S436 { get; set; }
        public bool S448 { get; set; }
        public bool S438 { get; set; }
        public bool S442 { get; set; }



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