using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Login = new HashSet<Login>();
            MemberCard = new HashSet<MemberCard>();
            Payment = new HashSet<Payment>();
        }

        public int CustomerId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime BirthDay { get; set; }
        public int? PaymentId { get; set; }
        public int TypeId { get; set; }
        public int Phone { get; set; }
        public string CustimerImg { get; set; }

        public virtual LoginTypes Type { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<MemberCard> MemberCard { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
