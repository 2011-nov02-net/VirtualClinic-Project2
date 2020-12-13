using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Doctor : User
    {
        public List<Patient> Patients { get; set; }
        public string Title { get; set; }
    }
}