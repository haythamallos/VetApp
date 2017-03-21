using System.Collections.Generic;

namespace MainSite.Models
{
    public class ProductCartModel
    {
        public List<ProductModel> lstProductModel { get; set; }
        public decimal TotalPrice { get; set; }
        public string TotalPriceText { get; set; }
        public string StripeApiKey { get; set; }
        public ProductCartModel()
        {
            lstProductModel = new List<ProductModel>();
        }

    }
}