using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Vital
    {
        public Vital()
        {
            Appointments = new HashSet<Appointment>();
            PatientReports = new HashSet<PatientReport>();
        }

        public int Id { get; set; }
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public int? HeartRate { get; set; }
        public decimal? Temperature { get; set; }
        public int? Pain { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PatientReport> PatientReports { get; set; }
    }
}
