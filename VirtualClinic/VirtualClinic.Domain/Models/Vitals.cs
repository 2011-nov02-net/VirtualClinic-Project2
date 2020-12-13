using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Vitals
    {
        public int Id { get; set; }
        public int HeartRate { get; set; }
        public string BloodPressure { get; set; }
        public double Temperature { get; set; }
        public int PainLevel { get; set; }
    }
}
