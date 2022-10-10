using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waedist.Models
{
    public class PaymentUser
    {
        public Payment payment { get; set; }
        public Useraccount user { get; set; }
    }
}
