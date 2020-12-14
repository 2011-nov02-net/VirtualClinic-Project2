using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualClinic.Domain.Interfaces;
using VirtualClinic.Domain.Models;

namespace VirtualClinic.Domain.Repositories
{
    class ClinicRepository : IClinicRepository
    {
        public void AddDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task AddDoctorAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void AddPatientReport(PatientReport report)
        {
            throw new NotImplementedException();
        }

        public Task AddPatientReportAsync(PatientReport report)
        {
            throw new NotImplementedException();
        }

        public void AddPrescription(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public Task AddPrescriptionAsync(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public void AddTimeslot(Timeslot timeslot)
        {
            throw new NotImplementedException();
        }

        public Task AddTimeslotAsync(Timeslot timeslot)
        {
            throw new NotImplementedException();
        }

        public Doctor GetDoctorByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetDoctorByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetDoctorPatients(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Patient>> GetDoctorPatientsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Timeslot> GetDoctorTimeslots(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Timeslot>> GetDoctorTimeslotsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Prescription> GetPatientPrescriptions(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public PatientReport GetPatientReportByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PatientReport> GetPatientReportByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PatientReport> GetPatientReports(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Prescription>> GetPatientReportsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetPatients()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Timeslot> GetPatientTimeslots(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Timeslot>> GetPatientTimeslotsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
