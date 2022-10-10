using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Testmoninal
    {
        public decimal Testmoninalid { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Testimage { get; set; }
        public decimal? Userid { get; set; }
        public string Status { get; set; }

        public virtual Useraccount User { get; set; }
    }
}
