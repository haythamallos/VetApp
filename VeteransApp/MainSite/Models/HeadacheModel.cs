
namespace MainSite.Models
{
    public class HeadacheModel : IBaseModel
    {
        public long ContentID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentStateID { get; set; }

        public string TemplatePath { get; set; }
        public long UserID { get; set; }

        public string NameOfPatient { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string SocialSecurity { get; set; }

        public bool S35 { get; set; }
        public bool S23 { get; set; }
        public bool S24 { get; set; }
        public string MedicationPlan { get; set; }

        public bool S26 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool S4 { get; set; }
        public bool S5 { get; set; }
        public bool S6 { get; set; }
        public string S3AOther { get; set; }
        public bool S3AYes { get; set; }

        public bool S75 { get; set; }
        public bool S68 { get; set; }
        public bool S69 { get; set; }
        public bool S70 { get; set; }
        public bool S71 { get; set; }
        public bool S72 { get; set; }
        public string S3BOther { get; set; }
        public bool S3BYes { get; set; }

        public bool S54 { get; set; }
        public bool S55 { get; set; }
        public bool S60 { get; set; }
        public string S3COther { get; set; }

        public bool S66 { get; set; }
        public bool S65 { get; set; }
        public bool S64 { get; set; }
        public string S3DOther { get; set; }

        public bool S56 { get; set; }
        public bool S57 { get; set; }
        public bool S58 { get; set; }
        public bool S59 { get; set; }
        public bool S4AYes { get; set; }

        public bool S45 { get; set; }
        public bool S44 { get; set; }
        public bool S43 { get; set; }
        public bool S42 { get; set; }
        public bool S4CYes { get; set; }

        public string WorkCondition { get; set; }

    }
}