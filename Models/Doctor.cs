using System;
using System.Collections.Generic;

namespace OnlineHealthManagement.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int DoctorId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string Specification { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string IsAvailable { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
