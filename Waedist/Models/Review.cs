using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Review
    {
        public decimal Reviewid { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public string Text { get; set; }
        public decimal? Storeid { get; set; }

        public virtual Store Store { get; set; }
    }
}
