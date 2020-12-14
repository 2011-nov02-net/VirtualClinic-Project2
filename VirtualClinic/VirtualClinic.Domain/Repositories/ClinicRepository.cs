using Microsoft.Extensions.Logging;
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Doctor>> GetDoctorsAsync()
        {
            throw new NotImplementedException();
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

                //todo: fill in perscriptions
                next.Prescriptions = new List<Models.Prescription>();

                //could get dr
                next.PrimaryDoctor = this.GetDoctorByID(dbpatient.DoctorId);

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

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Patient>> GetPatientsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Timeslot> GetPatientTimeslots(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Timeslot>> GetPatientTimeslotsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
