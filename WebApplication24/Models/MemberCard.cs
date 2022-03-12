using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class MemberCard
    {
        public MemberCard()
        {
            Payment = new HashSet<Payment>();
        }

        public int CardId { get; set; }
        public int CustomerId { get; set; }
        public int MemberShipId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int EmployeeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual MemberShip MemberShip { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
