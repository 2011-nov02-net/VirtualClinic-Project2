using System;
using System.Collections.Generic;

namespace VirtualClinic.Domain.Models
{
    public class Patient : User
    {

        public Patient(int id, string name, DateTime dob)
        {
            base.Id = id;
            base.Name = name;
            this.DateOfBirth = dob;
        }

        public Doctor PrimaryDoctor { get; set; }
        public List<PatientReport> PatientReports { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; }
        public string InsuranceProvider { get; set; }

    }
}
