using System;
using System.Collections.Generic;

namespace OnlineHealthManagement.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int? PId { get; set; }
        public int? DId { get; set; }
        public string Status { get; set; }

        public virtual Doctor D { get; set; }
        public virtual Patient P { get; set; }
    }
}
