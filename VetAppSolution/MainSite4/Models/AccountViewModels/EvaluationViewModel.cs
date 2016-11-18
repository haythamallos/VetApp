using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainSite.Models.AccountViewModels
{
    public class EvaluationViewModel
    {
        public bool IsFirstTimeFiling { get; set; }
        public bool HasVAClaim { get; set; }
        public bool HasActiveAppeal { get; set; }
        public bool HasRating { get; set; }
        public int Rating { get; set; }
    }
}
