using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class MemberShip
    {
        public MemberShip()
        {
            MemberCard = new HashSet<MemberCard>();
            Payment = new HashSet<Payment>();
        }

        public int MemberShipId { get; set; }
        public string MemberShipName { get; set; }
        public int TimePerMonth { get; set; }
        public string TypeOfEvnet { get; set; }
        public int Cost { get; set; }
        public int OfferId { get; set; }
        public int? Capacity { get; set; }

        public virtual Offers Offer { get; set; }
        public virtual ICollection<MemberCard> MemberCard { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
