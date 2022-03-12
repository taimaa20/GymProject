using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class Task
    {
        public Task()
        {
            EmplyeeJob = new HashSet<EmplyeeJob>();
        }

        public int TaskId { get; set; }
        public string NameOfTask { get; set; }

        public virtual ICollection<EmplyeeJob> EmplyeeJob { get; set; }
    }
}
