using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        public int CutOfsalary { get; set; }
        public int ExtraForEachMonth { get; set; }
        public int EmployeeId { get; set; }
        public int Salary1 { get; set; }
        public DateTime? MonthOfSalary { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
