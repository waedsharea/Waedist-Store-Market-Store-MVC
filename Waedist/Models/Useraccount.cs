using System;
using System.Collections.Generic;

#nullable disable

namespace Waedist.Models
{
    public partial class Useraccount
    {
        public Useraccount()
        {
            Orderrs = new HashSet<Orderr>();
            Payments = new HashSet<Payment>();
            Testmoninals = new HashSet<Testmoninal>();
        }

        public decimal Userid { get; set; }
        public string Fullname { get; set; }
        public decimal? Phonenumber { get; set; }
        public string Pic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? Roleid { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Orderr> Orderrs { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Testmoninal> Testmoninals { get; set; }
    }
}
