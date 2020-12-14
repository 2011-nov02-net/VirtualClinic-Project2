﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualClinic.DataModel;
using VirtualClinic.Domain.Interfaces;
using VirtualClinic.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VirtualClinic.Domain.Mapper;

namespace VirtualClinic.Domain.Repositories
{
    class ClinicRepository : IClinicRepository
    {
        private readonly ClinicDbContext _context;
        private readonly ILogger<ClinicRepository> _logger;
        public ClinicRepository(ClinicDbContext context, ILogger<ClinicRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }


        public void AddDoctor(Models.Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task AddDoctorAsync(Models.Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void AddPatient(Models.Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientAsync(Models.Patient patient)
        {
            throw new NotImplementedException();
        }

        public void AddPatientReport(Models.PatientReport report)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientReportAsync(Models.PatientReport report)
        {
            throw new NotImplementedException();
        }

        public void AddPrescription(Models.Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public Task AddPrescriptionAsync(Models.Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public void AddTimeslot(Models.Timeslot timeslot)
        {
            throw new NotImplementedException();
        }

        public Task AddTimeslotAsync(Models.Timeslot timeslot)
        {
            throw new NotImplementedException();
        }

        public Models.Doctor GetDoctorByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Doctor> GetDoctorByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Patient> GetDoctorPatients(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Patient>> GetDoctorPatientsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Doctor> GetDoctors()
        {
            var DBDoctors = _context.Doctors
               .Include(o => o.Patients)
               .ToList();

            List<Models.Doctor> ModelDoctors = new List<Models.Doctor>();

            foreach (var dbdoctor in DBDoctors)
            {
                Models.Doctor next = new Models.Doctor(dbdoctor.Id, dbdoctor.Name, dbdoctor.Title)
                {
                    Patients = (List<Models.Patient>)GetDoctorPatients(dbdoctor.Id)
                };

                ModelDoctors.Add(next);
            }

            return ModelDoctors;

        }

        public async Task<IEnumerable<Models.Doctor>> GetDoctorsAsync()
        {
            var DBDoctors = await _context.Doctors
              .Include(o => o.Patients)
              .ToListAsync();

            List<Models.Doctor> ModelDoctors = new List<Models.Doctor>();

            foreach (var dbdoctor in DBDoctors)
            {
                Models.Doctor next = new Models.Doctor(dbdoctor.Id, dbdoctor.Name, dbdoctor.Title)
                {
                    Patients = (List<Models.Patient>)GetDoctorPatients(dbdoctor.Id)
                };

                ModelDoctors.Add(next);
            }

            return ModelDoctors;

        }

        public IEnumerable<Models.Timeslot> GetDoctorTimeslots(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Timeslot>> GetDoctorTimeslotsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Models.Patient GetPatientByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Patient> GetPatientByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Prescription> GetPatientPrescriptions(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Prescription>> GetPatientPrescriptionsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Models.PatientReport GetPatientReportByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.PatientReport> GetPatientReportByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.PatientReport> GetPatientReports(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Prescription>> GetPatientReportsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Patient> GetPatients()
        {
            var DBPatients = _context.Patients
                    .Include(thing => thing.PatientReports)
                    .ToList();

            List<Models.Patient> ModelPatients = new List<Models.Patient>();

            foreach(var dbpatient in DBPatients)
            {
                Models.Patient next = new Models.Patient(dbpatient.Id, dbpatient.Name,dbpatient.Dob);

                next.InsuranceProvider = dbpatient.Insurance;

                next.SSN = dbpatient.Ssn;
            
                //could get dr
                next.PrimaryDoctor = GetDoctorByID(dbpatient.DoctorId);

                //todo, fill in timeslots
                next.Timeslots = new List<Models.Timeslot>();

                //todo: fill in perscriptions
                next.Prescriptions = new List<Models.Prescription>();

                next.PatientReports = new List<Models.PatientReport>();
                foreach(var dbPatientReport in dbpatient.PatientReports)
                {
                    var report = DB_DomainMapper.MapReport(dbPatientReport);
                    report.Patient = next;
                    //TODO: note vitals still not filled in.

                    next.PatientReports.Add(report);
                }
                

                ModelPatients.Add(next);
            }

            return ModelPatients;
        }

        public Task<IEnumerable<Models.Patient>> GetPatientsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get timeslot and apointment information for ones related to a specific patient.
        /// </summary>
        /// <remarks>
        /// NOTE: because this matches to a patient id which is stored on apointments,
        /// every item returned should have an apointment attached to the timeslot.
        /// </remarks>
        /// <param name="PatientId">The id of the desired patient in the DB.</param>
        /// <returns>List of timeslots with any apointment informtion filled in.</returns>
        public IEnumerable<Models.Timeslot> GetPatientTimeslots(int PatientId)
        {
            List<DataModel.Timeslot> timeslots = _context.Timeslots
                    .Include(ts => ts.Appointment)
                    .Where(ts => ts.Appointment.PatientId == PatientId)
                    .ToList();

            List<Models.Timeslot> modelTimeslots = new List<Models.Timeslot>();

            foreach(var DBTimeSlot in timeslots)
            {
                Models.Timeslot modelts = DB_DomainMapper.MapTimeslot(DBTimeSlot);

                //does not fill in dr or patient
                modelts.Appointment = DB_DomainMapper.MapApointment(DBTimeSlot.Appointment);

                modelTimeslots.Add(modelts);
            }

            return modelTimeslots;        
        }

        public Task<IEnumerable<Models.Timeslot>> GetPatientTimeslotsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
