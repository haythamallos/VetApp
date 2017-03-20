using System.Drawing;

namespace MainSite.Models
{
    public class ProductModel
    {
        public long CartItemID { get; set; }
        public long ContentTypeID { get; set; }
        public long ContentID { get; set; }
        public long UserID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string ImagePath { get; set; }
        public int NumberOfPages { get; set; }
        public string ProductRefName { get; set; }
        public string ProductRefDescription { get; set; }
        public string AlertMessageTitle { get; set; }
        public string AlertMessageDescription { get; set; }
        //public byte[] ContentData { get; set; }

    }
}