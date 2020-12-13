using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    class Timeslot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Appointment Appointment { get; set; }

    }
}
