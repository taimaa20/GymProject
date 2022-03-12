using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class LoginTypes
    {
        public LoginTypes()
        {
            Customer = new HashSet<Customer>();
            Employee = new HashSet<Employee>();
            Login = new HashSet<Login>();
        }

        public int TypeId { get; set; }
        public string LoginType { get; set; }
        public int? TypeOfLoginType { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Login> Login { get; set; }
    }
}
