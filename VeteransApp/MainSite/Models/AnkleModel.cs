
namespace MainSite.Models
{
    public class AnkleModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string SocialSecurity { get; set; }

        public string Side { get; set; }

        public bool S81 { get; set; }
        public string S81Side { get; set; }
        public bool S82 { get; set; }
        public string S82Side { get; set; }
        public bool S83 { get; set; }
        public string S83Side { get; set; }
        public bool S84 { get; set; }
        public string S84Side { get; set; }
        public bool S85 { get; set; }
        public string S85Side { get; set; }
        public bool S86 { get; set; }
        public string S86Side { get; set; }
        public bool S87 { get; set; }
        public string S87Side { get; set; }
        public bool S88 { get; set; }
        public string S88Side { get; set; }
        public bool S89 { get; set; }
        public string S89Side { get; set; }
        public bool S90 { get; set; }
        public string S90Side { get; set; }
        public bool S91 { get; set; }
        public string S91Side { get; set; }
        public bool S92 { get; set; }
        public string S92Side { get; set; }
        public string S93Other { get; set; }

        // Initial Right ROM
        public string S133 { get; set; }
        public string S137 { get; set; }
        // Post Right ROM
        public string S165 { get; set; }
        public string S164 { get; set; }
        // Flareup Right ROM
        public string S252 { get; set; }
        public string S255 { get; set; }

        // Initial Left ROM
        public string S121 { get; set; }
        public string S117 { get; set; }
        // Post Left ROM
        public string S150 { get; set; }
        public string S151 { get; set; }
        // Flareup Left ROM
        public string S237 { get; set; }
        public string S234 { get; set; }
    }
}