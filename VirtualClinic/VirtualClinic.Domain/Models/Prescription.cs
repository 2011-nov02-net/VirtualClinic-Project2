using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public string Info { get; set; }
        public string DrugName { get; set; }


        /// <summary>
        /// Creates a new Prescription. 
        /// </summary>
        /// <param name="id">A unique id for the Prescription</param>
        /// <param name="info">Misc info about the Prescription.</param>
        /// <param name="drugName">The name of the drug.</param>
        /// <param name="patient">(Optional)The patient who the Prescription is for.</param>
        /// <param name="doctor">(Optional) The doctor issuing the Prescription.</param>
        public Prescription(int id, string info, string drugName, Patient patient = null, Doctor doctor = null)
        {
            Id = id;
            Info = info;
            DrugName = drugName;
            Patient = patient;
            Doctor = doctor;
        }
        public Prescription(string info, string drugName, Patient patient = null, Doctor doctor = null)
        {
            Info = info;
            DrugName = drugName;
            Patient = patient;
            Doctor = doctor;
        }
    }
}
