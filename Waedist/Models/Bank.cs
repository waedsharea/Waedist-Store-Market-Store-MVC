﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Bank
    {
        public decimal Payid { get; set; }
        public string Ownername { get; set; }
        public decimal? Cardnumber { get; set; }
        public DateTime? Expiration { get; set; }
        public decimal? Cvv { get; set; }
        public decimal? Amount { get; set; }
    }
}
