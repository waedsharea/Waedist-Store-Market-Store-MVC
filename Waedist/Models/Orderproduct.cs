using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Orderproduct
    {
        public decimal Id { get; set; }
        public decimal? Orderid { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Totalamount { get; set; }
        public string Status { get; set; }
        public decimal? Productid { get; set; }
        public decimal? Stoid { get; set; }
        public string Storename { get; set; }

        public virtual Orderr Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
