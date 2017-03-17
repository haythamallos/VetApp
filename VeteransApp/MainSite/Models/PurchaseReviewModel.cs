using System.Drawing;

namespace MainSite.Models
{
    public class PurchaseReviewModel
    {
        public long ContentTypeID { get; set; }
        public long ContentID { get; set; }
        public long UserID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public byte[] ContentData { get; set; }

    }
}