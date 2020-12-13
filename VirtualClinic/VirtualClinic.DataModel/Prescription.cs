using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Drug { get; set; }
        public string Information { get; set; }
        public DateTime Date { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
