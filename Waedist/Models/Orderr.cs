using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Orderr
    {
        public Orderr()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public decimal Orderid { get; set; }
        public decimal? Userid { get; set; }
        public DateTime? Dte { get; set; }
        public string Status { get; set; }
        public string Display { get; set; }

        public virtual Useraccount User { get; set; }
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
