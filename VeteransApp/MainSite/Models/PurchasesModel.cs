using System.Collections.Generic;

namespace MainSite.Models
{
    public class PurchasesModel
    {
        public List<PurchasesModelItem> lstPurchasesModelItem { get; set; }
        public PurchasesModel()
        {
            lstPurchasesModelItem = new List<PurchasesModelItem>();
        }
    }

    public class PurchasesModelItem
    {
        public long ContentID { get; set; }
        public string FormDescription { get; set; }
        public string DatePurchased { get; set; }
    }
}