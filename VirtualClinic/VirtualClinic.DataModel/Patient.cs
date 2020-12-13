using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            PatientReports = new HashSet<PatientReport>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DoctorId { get; set; }
        public DateTime Dob { get; set; }
        public string Ssn { get; set; }
        public string Insurance { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PatientReport> PatientReports { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
