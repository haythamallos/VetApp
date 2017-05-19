
using System;

namespace MainSite.Models
{
    public class SleepapneaModel : IBaseModel
    {
        public bool IsFormReadonly { get; set; }

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

        public bool S6 { get; set; }

        public bool S20 { get; set; }
        public string lastSleepStudyDate { get; set; }
        public string FacilityName { get; set; }

    }
}