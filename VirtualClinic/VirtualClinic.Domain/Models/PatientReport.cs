using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class PatientReport
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }
        public Vitals Vitals { get; set; }
    }
}
