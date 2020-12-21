using System;
using System.Collections.Generic;
using System.Text;


namespace VirtualClinic.Domain.Models
{
    public class Timeslot
    {

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Appointment Appointment { get; set; }
        public int DoctorId { get; set; }

        /// <summary>
        /// Creates a 30 minute Appointment starting now.
        /// </summary>
        public Timeslot()
        {
            Start = DateTime.Now;
            End = DateTime.Now;
            End.AddMinutes(30);
        }

        /// <summary>
        /// Create a new timeslot between the specified times.
        /// </summary>
        /// <param name="start">The start time of the apointment.</param>
        /// <param name="end">when the apointment ends.</param>
        public Timeslot(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public Timeslot(int id, DateTime start, DateTime end)
        {
            Id = id;
            Start = start;
            End = end;
        }
        public Timeslot(DateTime start)
        {
            Start = start;
            End = start;
            End.AddMinutes(30);
        }
    }
}