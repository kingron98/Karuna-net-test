using System;
using PagedList;

namespace Karuna.Models
{
    public class ProductViewModels
    {
        public int? Page { get; set; }
        public int? SizePage { get; set; }
        public IPagedList<ProductView> SearchResults { get; set; }
        public Product ProductInformation { get; set; }

        #region search parameter
        public string Productname { get; set; }
        #endregion
    }

    public class ProductView
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Details { get; set; }
        public Nullable<bool> Publish { get; set; }
    }
}