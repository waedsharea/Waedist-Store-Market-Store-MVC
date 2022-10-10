using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Product
    {
        public Product()
        {
            Orderproducts = new HashSet<Orderproduct>();
            Productstores = new HashSet<Productstore>();
        }

        public decimal Productid { get; set; }
        public string Productname { get; set; }
        public decimal? Price { get; set; }
        public decimal? Productvalue { get; set; }
        public string Pic { get; set; }
        public string Productsize { get; set; }

        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
        public virtual ICollection<Productstore> Productstores { get; set; }
    }
}
