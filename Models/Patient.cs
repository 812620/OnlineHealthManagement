using System;
using System.Collections.Generic;

namespace OnlineHealthManagement.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age {get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
