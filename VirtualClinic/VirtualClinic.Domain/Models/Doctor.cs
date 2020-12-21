using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualClinic.Domain.Models
{
    public class Doctor : User
    {
        private HashSet<Patient> _patients = new HashSet<Patient>();

        public List<Patient> Patients { 
            get
            {
                //equiv to making readonly.
                return _patients.ToList();
            }
            internal set
            {
                _patients = new HashSet<Patient>();

                foreach(var p in value)
                {
                    _patients.Add(p);
                }                
            }
        }


        public string Title { get; set; }

        /// <summary>
        /// Adds a new patient to the doctor's list of patients
        /// </summary>
        /// <param name="p">The patient to be added.</param>
        public void AddPatient(Patient p)
        {
            _patients.Add(p);
        }


        /// <summary>
        /// Creates a new doctor.
        /// </summary>
        /// <param name="id">A unique id for the doctor.</param>
        /// <param name="name">The doctor's name.</param>
        /// <param name="title">(Optional) The doctor's title</param>
        public Doctor(int id, string name, string title = null)
        {
            base.Id = id;
            base.Name = name;
            this.Title = title;
            _patients = new HashSet<Patient>();
        }
        public Doctor(string name, string title = null)
        {
            base.Name = name;
            this.Title = title;
            _patients = new HashSet<Patient>();
        }
        public Doctor()
        {
        }
    }
}