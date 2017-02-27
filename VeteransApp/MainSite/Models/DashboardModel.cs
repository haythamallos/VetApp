﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class DashboardModel
    {
        public EvaluationResults evaluationResults { get; set; }
        public DashboardModel()
        {
            evaluationResults = new EvaluationResults();
        }
    }

    public class EvaluationResults
    {
        public int CurrentRating { get; set; }
        public int PotentialVARating { get; set; }
        public int AmountIncreasePerMonth { get; set; }
        public int TotalPerMonthAfterIncrease { get; set; }

    }
}