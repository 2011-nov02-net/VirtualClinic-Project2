using System;
using System.Collections.Generic;

namespace VirtualClinic.Domain.Models
{
    public class Patient : User
    {
        public Doctor PrimaryDoctor { get; set; }
        public List<PatientReport> PatientReports { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; }
        public string InsuranceProvider { get; set; }

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="id">A unique id for the paitent.</param>
        /// <param name="name">The patient's name</param>
        /// <param name="dob">The patient's Date of birth.</param>
        /// <param name="doctor">Optional, defualts to null. The primary doctor of the patient</param>
        public Patient(int id, string name, DateTime dob, Doctor doctor = null)
        {
            base.Id = id;
            base.Name = name;
            this.DateOfBirth = dob;
            this.PrimaryDoctor = doctor;

            this.PatientReports = new List<PatientReport>();
            this.Prescriptions = new List<Prescription>();
        }

    }
}
