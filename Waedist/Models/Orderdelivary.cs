using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Orderdelivary
    {
        public decimal Odlivaryid { get; set; }
        public decimal? Orderid { get; set; }
        public decimal? Userid { get; set; }
        public string Username { get; set; }
        public string Useraddress { get; set; }
        public string Useremail { get; set; }
        public string Userphone { get; set; }
        public string Shippingmethod { get; set; }
        public decimal? Postnumber { get; set; }
    }
}
