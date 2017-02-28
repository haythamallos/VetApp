using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace MainSite.Classes
{
    public class CookieManager
    {
        public static readonly string COOKIENAME = @"veteranspp.com";
        public static readonly string COOKIE_FIELD_USER_GUID = @"UserGuid";
        public static readonly string COOKIE_FIELD_VISIT_COUNT = @"VisitCount";
        public static readonly string COOKIE_FIELD_IS_FIRST_TIME_FILING = @"IsFirstTimeFiling";
        public static readonly string COOKIE_FIELD_HAS_A_CLAIM = @"HasAClaim";
        public static readonly string COOKIE_FIELD_HAS_ACTIVE_APPEAL = @"HasActiveAppeal";
        public static readonly string COOKIE_FIELD_CURRENT_RATING = @"CurrentRating";
        public static readonly string COOKIE_FIELD_ISNEW_EVAL = @"IsNewEval";
    }
}