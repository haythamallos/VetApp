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

        public void AddItem()
        {
            if (!string.IsNullOrEmpty(workingItem.RatingID))
            {
                CalculatorItem item = new CalculatorItem() {RatingID = workingItem.RatingID, BilateralFactorID = workingItem.BilateralFactorID };
                lstCalculatorItem.Add(item);
                workingItem.Clear();
            }
        }
        public void RemoveItem(int index)
        {
            if (index < lstCalculatorItem.Count)
            {
                lstCalculatorItem.RemoveAt(index);
            }
        }
        public void Clear()
        {
            lstCalculatorItem.Clear();
            workingItem.Clear();
        }
        public void CalcCombinedRating()
        {

        }
        public static readonly IDictionary<string, string> BilateralFactorDictionary = new Dictionary<string, string>
        {
             { "1", "Bilateral Upper" }
            , {"2",  "Right Upper"}
            , {"3",  "Left Upper"}
            , {"4",  "Bilateral Lower"}
            , {"5",  "Right Lower"}
            , {"6",  "Left Lower"}
         };
     
    }

    public class CalculatorItem
    {
        public string RatingID { get; set; }
        public string BilateralFactorID { get; set; }
        public override string ToString()
        {
            string s = RatingID;
            if (!string.IsNullOrEmpty(BilateralFactorID))
            {
                s = s + " " + CalculatorViewModel.BilateralFactorDictionary[BilateralFactorID];
            }
            return s;
        }
        public void Clear()
        {
            BilateralFactorID = null;
            RatingID = null;
        }
    }

    //public class BilateralFactorDictionary
    //{
    //    public static SelectList StateSelectList
    //    {
    //        get { return new SelectList(StateDictionary, "Value", "Key"); }
    //    }

    //}

}