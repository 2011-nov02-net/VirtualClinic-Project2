using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Doctor : User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<Patient> Patients { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }

}
