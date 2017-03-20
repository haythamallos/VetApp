using System.Collections.Generic;

namespace MainSite.Models
{
    public class ProductCartModel
    {
        public List<ProductModel> lstProductModel { get; set; }
        public double TotalPrice { get; set; }
        public ProductCartModel()
        {
            lstProductModel = new List<ProductModel>();
        }

    }
}