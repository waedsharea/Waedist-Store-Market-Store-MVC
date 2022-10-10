using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waedist.Models
{
    public class PSCJoincs
    {

        public Store store { get; set; }
        public Product product { get; set; }
        public Category category { get; set; }
        public Productstore productstore { get; set; }
    }
}
