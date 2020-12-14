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
        public double? Temperature { get; set; }
        public int PainLevel { get; set; }

        /// <summary>
        /// Creates a new Vitals with the given information.
        /// </summary>
        /// <param name="id">A unique identifier.</param>
        /// <param name="heartRate">The heartrate of the patient.</param>
        /// <param name="bloodPressure">The patient's blood pressure.</param>
        /// <param name="temperature">The patient's temperature.</param>
        /// <param name="painLevel">The patient's pain level.</param>
        public Vitals(int id, int heartRate, string bloodPressure, double? temperature, int painLevel)
        {
            Id = id;
            HeartRate = heartRate;
            BloodPressure = bloodPressure;
            Temperature = temperature;
            PainLevel = painLevel;
        }

        /// <summary>
        /// create a default vitals with just an id
        /// </summary>
        /// <param name="id">A unique id</param>
        public Vitals(int id)
        {
            this.Id = id;
        }
    }
}
