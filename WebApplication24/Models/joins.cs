using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication24.Models
{
    public class joins
    {
        public MemberCard memberCard {get;set;}
        public MemberShip memberShip { get; set; }
        public Employee employee { get; set; }
        public Salary salary { get; set; }
        public Payment payment { get; set; }
        public LoginTypes LoginTypes { get; set; }
        public Customer customer { get; set; }
        public Offers offer { get; set; }
        public Login logins { get; set; }
        public Task tasks1 { get; set; }
       public EmplyeeJob emplyeeJob { get; set; }

    }
}
