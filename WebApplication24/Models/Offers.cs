using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Offers
    {
        public Offers()
        {
            MemberShip = new HashSet<MemberShip>();
        }

        public int OfferId { get; set; }
        public int PercantageOffer { get; set; }
        public int NumberOfMonthsInOffer { get; set; }

        public virtual ICollection<MemberShip> MemberShip { get; set; }
    }
}
