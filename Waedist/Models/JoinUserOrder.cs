using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waedist.Models
{
    public class JoinUserOrder
    {

        public Orderproduct orderproduct { get; set; }
        public Useraccount useraccount { get; set; }
        public Orderr order { get; set; }
        public Product product { get; set; }
        public Productstore productshop { get; set; }
        public Store store { get; set; }


    }
}
