using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmploeeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Emploee { get; set; }
        public virtual LoginTypes Type { get; set; }
    }
}
