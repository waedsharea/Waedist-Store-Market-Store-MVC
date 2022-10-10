using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Productstore
    {
        public decimal Id { get; set; }
        public decimal? Productid { get; set; }
        public decimal? Storeid { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
