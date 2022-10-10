using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Payment
    {
        public decimal Payid { get; set; }
        public decimal? Cardnumber { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Userid { get; set; }
        public DateTime? Datee { get; set; }

        public virtual Useraccount User { get; set; }
    }
}
