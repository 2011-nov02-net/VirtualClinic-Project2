using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Timeslot
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
