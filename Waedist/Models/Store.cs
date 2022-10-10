using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Store
    {
        public Store()
        {
            Productstores = new HashSet<Productstore>();
            Reviews = new HashSet<Review>();
        }

        public decimal Storeid { get; set; }
        public string Storename { get; set; }
        public decimal? Categoryid { get; set; }
        public string Smallpic { get; set; }
        public string Headpic { get; set; }
        public string Text { get; set; }
        public decimal? Totalsales { get; set; }
        public decimal? Monthlyrent { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Productstore> Productstores { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
