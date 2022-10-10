using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Category
    {
        public Category()
        {
            Stores = new HashSet<Store>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Pic { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
