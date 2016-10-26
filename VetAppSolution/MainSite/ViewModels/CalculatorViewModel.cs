using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainSite.ViewModels
{
    public class CalculatorViewModel
    {
        public List<CalculatorItem> lstCalculatorItem { get; set; }
        public CalculatorItem workingItem { get; set; }
        public int CombinedRating { get; set; }
        public CalculatorViewModel()
        {
            lstCalculatorItem = new List<CalculatorItem>();
            CombinedRating = 0;
            workingItem = new CalculatorItem();
        }
        public string WorkingItemText()
        {
            string s = string.Empty;
            if ((workingItem.RatingID != null) && (workingItem.BilateralFactorID != null))
            {
                s = RatingCodeDictionary[workingItem.RatingID] + " " + BilateralFactorDictionary[workingItem.BilateralFactorID];
            }
            else if (workingItem.RatingID != null)
            {
                s = RatingCodeDictionary[workingItem.RatingID];
            }
            else if (workingItem.BilateralFactorID != null)
            {
                s = BilateralFactorDictionary[workingItem.BilateralFactorID];
            }

            return s;
        }
        public readonly IDictionary<string, string> BilateralFactorDictionary = new Dictionary<string, string>
        {
             { "1", "Bilateral Upper" }
            , {"2",  "Right Upper"}
            , {"3",  "Left Upper"}
            , {"4",  "Bilateral Lower"}
            , {"5",  "Right Lower"}
            , {"6",  "Left Lower"}
         };

        public readonly IDictionary<string, string> RatingCodeDictionary = new Dictionary<string, string>
        {
             {"1", "10"}
             ,{"2", "20"}
             ,{"3", "30"}
             ,{"4", "40"}
             ,{"5", "50"}
             ,{"6", "60"}
             ,{"7", "70"}
             ,{"8", "80"}
             ,{"9", "90"}
         };

    }

    public class CalculatorItem
    {
        public string RatingID { get; set; }
        public string BilateralFactorID { get; set; }
    }

    //public class BilateralFactorDictionary
    //{
    //    public static SelectList StateSelectList
    //    {
    //        get { return new SelectList(StateDictionary, "Value", "Key"); }
    //    }

    //}

}