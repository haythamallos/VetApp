using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class EvaluationModel
    {
        public bool IsFirstTimeFiling { get; set; }
        public bool HaveAClaim { get; set; }
        public bool HaveActiveAppeal { get; set; }
        public int CurrentRating { get; set; }

    }
}