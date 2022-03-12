using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Payment
    {
        public string TypeOfPayment { get; set; }
        public int NumberOfTimesToPay { get; set; }
        public int MemberShipId { get; set; }
        public int PaymentId { get; set; }
        public int? MountOfPayment { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public int? TheRestOfTheAmount { get; set; }
        public int? CardId { get; set; }
        public int? CutsomerId { get; set; }

        public virtual MemberCard Card { get; set; }
        public virtual Customer Cutsomer { get; set; }
        public virtual MemberShip MemberShip { get; set; }
    }
}
