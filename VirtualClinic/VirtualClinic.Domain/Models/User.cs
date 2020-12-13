using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Timeslot> Timeslots { get; set; }
    }
}
