using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Appointment
    {
        public Appointment()
        {
            Timeslots = new HashSet<Timeslot>();
        }

        public int Id { get; set; }
        public string Notes { get; set; }
        public int? VitalsId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Vital Vitals { get; set; }
        public virtual ICollection<Timeslot> Timeslots { get; set; }
    }
}
