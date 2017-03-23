using System;
using System.Collections.Generic;
using System.Linq;

namespace MainSite.ViewModels
{
    public class CalculatorViewModel
    {
        public List<CalculatorItem> lstCalculatorItem { get; set; }
        public CalculatorItem workingItem { get; set; }
        public int combinedRating { get; set; }
        public CalculatorItem bilateralWorkingItem { get; set; }
        public bool hasBilateral { get; set; }
        public bool isLowerBilateral = false;
        public bool isUpperBilateral = false;

        public CalculatorViewModel()
        {
            lstCalculatorItem = new List<CalculatorItem>();
            workingItem = new CalculatorItem();
            bilateralWorkingItem = new CalculatorItem() { isBilateralWorkingItem = true };
            combinedRating = 0;
            hasBilateral = false;
        }

        public void AddItem()
        {
            if (workingItem.RatingID > 0)
            {
                CalculatorItem item = new CalculatorItem() { RatingID = workingItem.RatingID, BilateralFactorID = workingItem.BilateralFactorID };
                lstCalculatorItem.Add(item);
                workingItem.Clear();
                lstCalculatorItem = lstCalculatorItem.OrderByDescending(x => x.RatingID).ToList();
                combinedRating = CalcCombinedRating();
            }
        }
        public void RemoveItem(int index)
        {
            if (index < lstCalculatorItem.Count)
            {
                lstCalculatorItem.RemoveAt(index);
                lstCalculatorItem = lstCalculatorItem.OrderByDescending(x => x.RatingID).ToList();
                combinedRating = CalcCombinedRating();
            }
        }
        public void Clear()
        {
            lstCalculatorItem.Clear();
            workingItem.Clear();
            combinedRating = 0;
            bilateralWorkingItem.Clear();
            bilateralWorkingItem.isBilateralWorkingItem = true;
            hasBilateral = false;
        }
        public int CalcCombinedRating()
        {
            int result = 0;
            CalculatorItem item = null;
            CalculatorItem itemTmp = null;
            double curRating = 0;
            double tmpRating = 0;
            double curEfficiency = 100.0;
            double roundedRating = 0;
            int bilaterialRatingRaw = 0;

            // preprocess to check if bilateral is in effect
            // Bilateral must have a corresponding lower
            // or upper item
            isUpperBilateral = false;
            isLowerBilateral = false;
            for (int i = 0; i < lstCalculatorItem.Count; i++)
            {
                item = lstCalculatorItem[i];
                if (!string.IsNullOrEmpty(item.BilateralFactorID))
                {
                    if ((item.BilateralFactorID == "2") || (item.BilateralFactorID == "3"))
                    {
                        isUpperBilateral = true;
                    }

                    if ((item.BilateralFactorID == "4") || (item.BilateralFactorID == "5"))
                    {
                        isLowerBilateral = true;
                    }

                    if ((item.BilateralFactorID == "1") || (item.BilateralFactorID == "6"))
                    {
                        item.skipLateralCalc = true;
                    }
                }
            }

            if (isUpperBilateral)
            {
                // set the skips for items 1
                for (int i = 0; i < lstCalculatorItem.Count; i++)
                {
                    item = lstCalculatorItem[i];
                    if ( (!string.IsNullOrEmpty(item.BilateralFactorID)) && (item.BilateralFactorID == "1"))
                    {
                        item.skipLateralCalc = false;
                    }
                }
            }

            if (isLowerBilateral)
            {
                // set the skips for items 1
                for (int i = 0; i < lstCalculatorItem.Count; i++)
                {
                    item = lstCalculatorItem[i];
                    if ((!string.IsNullOrEmpty(item.BilateralFactorID)) && (item.BilateralFactorID == "6"))
                    {
                        item.skipLateralCalc = false;
                    }
                }
            }

            for (int i = 0; i < lstCalculatorItem.Count; i++)
            {
                item = lstCalculatorItem[i];
                if ((!string.IsNullOrEmpty(item.BilateralFactorID)) && (!item.skipLateralCalc))
                {
                    tmpRating = (item.RatingID / 100.0) * curEfficiency;
                    curEfficiency = curEfficiency - tmpRating;
                    curRating = Math.Round((curRating + tmpRating), MidpointRounding.AwayFromZero);
                }
            }

            bilaterialRatingRaw = Convert.ToInt32(1.1 * curRating);
            bilateralWorkingItem.RatingID = bilaterialRatingRaw;

            if (bilateralWorkingItem.RatingID > 0)
            {
                lstCalculatorItem.Add(bilateralWorkingItem);
                lstCalculatorItem = lstCalculatorItem.OrderByDescending(x => x.RatingID).ToList();
                hasBilateral = true;
            }
            else
            {
                hasBilateral = false;
            }

            curRating = 0; tmpRating = 0; curEfficiency = 100.0; roundedRating = 0;
            int biIndex = -1;
            for (int i = 0; i < lstCalculatorItem.Count; i++)
            {
                item = lstCalculatorItem[i];
                if ((hasBilateral) && (!item.isBilateralWorkingItem) && (!string.IsNullOrEmpty(item.BilateralFactorID)) && (!item.skipLateralCalc))
                {
                    continue;
                }
                tmpRating = (item.RatingID / 100.0) * curEfficiency;
                curEfficiency = curEfficiency - tmpRating;
                curRating = Math.Round((curRating + tmpRating), MidpointRounding.AwayFromZero);
                if (item.isBilateralWorkingItem)
                {
                    biIndex = i;
                }
            }
            roundedRating = RoundToTens(curRating);
            result = Convert.ToInt32(roundedRating);

            if (biIndex >= 0)
            {
                lstCalculatorItem.RemoveAt(biIndex);
            }
            return result;
        }
        private double RoundToTens(double D)
        {
            return 10 * Math.Floor(Math.Round((D / 10), MidpointRounding.AwayFromZero));
        }
        public static readonly IDictionary<string, string> BilateralFactorDictionary = new Dictionary<string, string>
        {
             { "1", "Bilateral Upper Arms" }
            , {"2",  "Right Upper Arm"}
            , {"3",  "Left Upper Arm"}
            , {"4",  "Left Lower Leg"}
            , {"5",  "Right Lower Leg"}
            , {"6",  "Bilateral Lower Leg"}
         };

        public string getBilateralFactorItem(string key)
        {
            string item = null;
            if (!string.IsNullOrEmpty(key))
            {
                item = BilateralFactorDictionary[key];
            }

            return item;
        }
    }

    public class CalculatorItem
    {
        public int RatingID { get; set; }
        public string BilateralFactorID { get; set; }
        public bool isBilateralWorkingItem { get; set; }
        public bool skipLateralCalc { get; set; }
        public override string ToString()
        {
            string s = string.Empty;
            if (RatingID > 0)
            {
                s = Convert.ToString(RatingID) + " ";
            }
            if (!string.IsNullOrEmpty(BilateralFactorID))
            {
                s = s + CalculatorViewModel.BilateralFactorDictionary[BilateralFactorID];
            }
            return s;
        }
        public void Clear()
        {
            BilateralFactorID = null;
            RatingID = 0;
            isBilateralWorkingItem = false;
            skipLateralCalc = false;
        }
    }
}