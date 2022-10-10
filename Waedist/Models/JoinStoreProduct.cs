using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waedist.Models
{
    public class JoinStoreProduct
    {
        public Productstore productstore { get; set; }
        public Orderproduct orderproduct { get; set; }

        public Store store { get; set; }
        public Product product { get; set; }
    }
}
