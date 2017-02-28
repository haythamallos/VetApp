using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Classes
{
    public class RatingProjections
    {
        public static Dictionary<int, RatingProjection> RatingTable_1 = new Dictionary<int, RatingProjection>()
                {
                    { 0, new RatingProjection {Rating = 0, TotalPerMonth = 0, IncreasePerMonthFromPreviousRating = 0, DeltaFromPrevious = 0}},
                    { 10, new RatingProjection {Rating = 10, TotalPerMonth = 133, IncreasePerMonthFromPreviousRating = 133, DeltaFromPrevious = 133}},
                    { 20, new RatingProjection {Rating = 20, TotalPerMonth = 263, IncreasePerMonthFromPreviousRating = 130, DeltaFromPrevious = 130}},
                    { 30, new RatingProjection {Rating = 30, TotalPerMonth = 455, IncreasePerMonthFromPreviousRating = 322, DeltaFromPrevious = 192}},
                    { 40, new RatingProjection {Rating = 40, TotalPerMonth = 651, IncreasePerMonthFromPreviousRating = 518, DeltaFromPrevious = 196}},
                    { 50, new RatingProjection {Rating = 50, TotalPerMonth = 917, IncreasePerMonthFromPreviousRating = 703, DeltaFromPrevious = 185}},
                    { 60, new RatingProjection {Rating = 60, TotalPerMonth = 1156, IncreasePerMonthFromPreviousRating = 1023, DeltaFromPrevious = 320}},
                    { 70, new RatingProjection {Rating = 70, TotalPerMonth = 1447, IncreasePerMonthFromPreviousRating = 1314, DeltaFromPrevious = 291}},
                    { 80, new RatingProjection {Rating = 80, TotalPerMonth = 1680, IncreasePerMonthFromPreviousRating = 1547, DeltaFromPrevious = 233}},
                    { 90, new RatingProjection {Rating = 90, TotalPerMonth = 1888, IncreasePerMonthFromPreviousRating = 1755, DeltaFromPrevious = 208}},
                    { 100, new RatingProjection {Rating = 100, TotalPerMonth = 3068, IncreasePerMonthFromPreviousRating = 2935, DeltaFromPrevious = 1180}}
                };
    }

    public class RatingProjection
    {
        public int Rating { get; set; }
        public int TotalPerMonth { get; set; }
        public int IncreasePerMonthFromPreviousRating { get; set; }
        public int DeltaFromPrevious { get; set; }
    }
}


