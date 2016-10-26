using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.ViewModels
{
    public class EvaluatorViewModel
    {
        public long EvaluatorID { get; set; }
        public bool IsFirstTimeFiling { get; set; }
        public bool HasFiled { get; set; }
        public bool HasActiveAppeal { get; set; }
        public bool HasRating { get; set; }
        public int CurrentRating { get; set; }
    }
}