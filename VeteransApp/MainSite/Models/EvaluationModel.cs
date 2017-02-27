using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class EvaluationModel
    {
        public bool IsFirstTimeFiling { get; set; }
        public bool HasAClaim { get; set; }
        public bool HasActiveAppeal { get; set; }
        public int CurrentRating { get; set; }
        public bool HasEvaluation { get; set; }

    }
}