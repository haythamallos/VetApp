using System.Collections.Generic;

namespace MainSite.Models
{
    public class SearchResultModel
    {
        public List<UserModel> lstUserModel { get; set; }
        public string searchText { get; set; }
        public int numresults { get; set; }
        public SearchResultModel()
        {
            lstUserModel = new List<UserModel>();
        }
    }
}