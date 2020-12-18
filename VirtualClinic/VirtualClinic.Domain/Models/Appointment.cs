﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// Creates a new apointment.
        /// </summary>
        /// <param name="id">A unique id for the apointment.</param>
        /// <param name="notes">Any notes about the apointment.</param>
        /// <param name="doctor">(Optional) The doctor who this apointment is with.</param>
        /// <param name="patient">(Optional) The patient whom the apointment is for.</param>
        public Appointment(int id, string notes, Doctor doctor = null, Patient patient = null)
        {
            Id = id;
            Notes = notes;
            Doctor = doctor;
            Patient = patient;        
        }
        public Appointment(string notes, Doctor doctor = null, Patient patient = null)
        {
            Notes = notes;
            Doctor = doctor;
            Patient = patient;
        }
    }
}
