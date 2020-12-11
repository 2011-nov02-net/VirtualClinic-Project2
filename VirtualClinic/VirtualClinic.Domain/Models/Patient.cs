using System;
using System.Collections.Generic;

namespace VirtualClinic.Domain.Models
{
    public class Patient
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Doctor PrimaryDoctor { get; set; }
        public List<PatientReport> PatientReports { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<Appointment> Appointments { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; }
        public string InsuranceProvider { get; set; }

    }
}
