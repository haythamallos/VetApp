using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vetapp.Engine.DataAccessLayer.Data;

namespace MainSite.Models
{
    public class PreliminaryModel
    {
        public ContentType contentType { get; set; }
        public long ContentTypeID { get; set; }
        public bool HasRating { get; set; }
        public int Rating { get; set; }
        public int RatingLeftSide { get; set; }
        public int RatingRightSide { get; set; }
        public string imageURL { get; set; }
        public long Side { get; set; }
        public bool AskSide { get; set; }
        public bool IsProfileFinished { get; set; }
        public string Message { get; set; }

        public PreliminaryModel()
        {
            contentType = new Vetapp.Engine.DataAccessLayer.Data.ContentType();
        }
    }
}