using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class  PatientReport
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }
        public Vitals Vitals { get; set; }

        /// <summary>
        /// Creates a new patient report
        /// </summary>
        /// <param name="id">A unique ID for the patient report</param>
        /// <param name="time">The time the report was recorded.</param>
        /// <param name="info">Any information or notes on the patient report.</param>
        /// <param name="patient">(Optional) The patient who this report is about.</param>
        public PatientReport(int id,  string info, DateTime time, Patient patient = null)
        {
            Id = id;
            Time = time;
            Info = info;
            Patient = patient;
        }
        public PatientReport(string info, DateTime time, Patient patient = null)
        {
            Time = time;
            Info = info;
            Patient = patient;
        }

        /// <summary>
        /// Creates a new report at date time now.
        /// </summary>
        /// <param name="id">A unique ID for the patient report</param>
        /// <param name="info">Any information or notes on the patient report.</param>
        /// <param name="patient">(Optional) The patient who this report is about.</param>
        public PatientReport(int id, string info, Patient patient = null)
        {
            Id = id;
            Time = DateTime.Now;
            Info = info;
            Patient = patient;
        }
    }
}
