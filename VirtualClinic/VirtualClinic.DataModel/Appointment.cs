using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public int? VitalsId { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Vital Vitals { get; set; }
    }
}
