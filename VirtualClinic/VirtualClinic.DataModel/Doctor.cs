using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            Patients = new HashSet<Patient>();
            Prescriptions = new HashSet<Prescription>();
            Timeslots = new HashSet<Timeslot>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Timeslot> Timeslots { get; set; }
    }
}
