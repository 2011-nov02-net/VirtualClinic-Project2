using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace VirtualClinic.Domain.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Timeslot> Timeslots { get; set; }

        protected User(int id, string name, List<Timeslot> timeslots)
        {
            Id = id;
            Name = name;
            
            if(timeslots is null)
            {
                timeslots = new List<Timeslot>();
            }
        }
        protected User(string name, List<Timeslot> timeslots)
        {
            Name = name;

            if (timeslots is null)
            {
                timeslots = new List<Timeslot>();
            }
        }

        protected User()
        {
            Id = -1;
            Name = null;
            Timeslots = new List<Timeslot>();
        }
    }
}