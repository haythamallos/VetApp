
using System.Collections.Generic;

namespace MainSite.Models
{
    public class DashboardModel
    {
        public EvaluationResults evaluationResults { get; set; }
        public EvaluationModel evaluationModel { get; set; }
        public Dictionary<long, BenefitStatus> BenefitStatuses { get; set; }
        public DashboardModel()
        {
        }
    }

    public class EvaluationResults
    {
        public int CurrentRating { get; set; }
        public int PotentialVARating { get; set; }
        public int AmountIncreasePerMonth { get; set; }
        public int TotalPerMonthAfterIncrease { get; set; }
        public int AmountIncreasePerYear { get; set; }
    }

    public class BenefitStatus
    {
        public long Key { get; set; }
        public string Progress { get; set; }
        public string ActionText { get; set; }
        public string TooltipText { get; set; }
        public string BenefitName { get; set; }
    }
}