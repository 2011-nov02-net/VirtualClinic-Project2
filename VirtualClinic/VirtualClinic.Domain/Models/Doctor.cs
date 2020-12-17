using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Doctor : User
    {
        public Doctor(string name, string title)
        {
            base.Name = name;
            this.Title = title;
        }
        public Doctor(int id, string name, string title)
        {
            base.Id = id;
            base.Name = name;
            this.Title = title;
        }

        public List<Patient> Patients { get; set; }
        public string Title { get; set; }
    }
}