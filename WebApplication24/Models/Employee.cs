using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendance1 = new HashSet<Attendance1>();
            EmplyeeJob = new HashSet<EmplyeeJob>();
            Login = new HashSet<Login>();
            MemberCard = new HashSet<MemberCard>();
            Salary = new HashSet<Salary>();
        }

        public int EmployeeId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Phone { get; set; }
        public int TypeId { get; set; }
        public string Evaluation { get; set; }

        public virtual LoginTypes Type { get; set; }
        public virtual ICollection<Attendance1> Attendance1 { get; set; }
        public virtual ICollection<EmplyeeJob> EmplyeeJob { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<MemberCard> MemberCard { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
    }
}
