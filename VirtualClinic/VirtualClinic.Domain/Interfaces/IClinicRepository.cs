using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualClinic.Domain.Models;

namespace VirtualClinic.Domain.Interfaces
{
    public interface IClinicRepository
    {
        /// <summary>
        /// Patient related queries
        /// </summary>
        #region Patients
        public IEnumerable<Patient> GetPatients();
        public Task<IEnumerable<Patient>> GetPatientsAsync();
        public Patient GetPatientByID(int id);
        public Task<Patient> GetPatientByIDAsync(int id);
        public void AddPatient(Patient patient);
        public Task<bool> AddPatientAsync(Patient patient);
        #endregion

        /// <summary>
        /// Doctor related queries
        /// </summary>
        #region Doctors
        public IEnumerable<Doctor> GetDoctors();
        public Task<IEnumerable<Doctor>> GetDoctorsAsync();
        public Doctor GetDoctorByID(int id);
        public Task<Doctor> GetDoctorByIDAsync(int id);
        public IEnumerable<Patient> GetDoctorPatients(int id);
        public Task<IEnumerable<Patient>> GetDoctorPatientsAsync(int id);
        public void AddDoctor(Doctor doctor);
        public Task AddDoctorAsync(Doctor doctor);
        #endregion

        /// <summary>
        /// Timeslot related queries
        /// </summary>
        #region TimeSlot
        public void AddTimeslot(Timeslot timeslot);
        public Task AddTimeslotAsync(Timeslot timeslot);
        public IEnumerable<Timeslot> GetPatientTimeslots(int id);
        public Task<IEnumerable<Timeslot>> GetPatientTimeslotsAsync(int id);
        public IEnumerable<Timeslot> GetDoctorTimeslots(int id);
        public Task<IEnumerable<Timeslot>> GetDoctorTimeslotsAsync(int id);
        #endregion

        /// <summary>
        /// Patient report related queries
        /// </summary>
        #region Reports
        public IEnumerable<PatientReport> GetPatientReports(int id);
        public Task<IEnumerable<Prescription>> GetPatientReportsAsync(int id);
        public PatientReport GetPatientReportByID(int id);
        public Task<PatientReport> GetPatientReportByIDAsync(int id);
        public void AddPatientReport(PatientReport report);
        public Task AddPatientReportAsync(PatientReport report);
        #endregion

        /// <summary>
        /// Prescription related queries
        /// </summary>
        #region Perscriptions
        public void AddPrescription(Prescription prescription);
        public Task<bool> AddPrescriptionAsync(Prescription prescription);
        public IEnumerable<Prescription> GetPatientPrescriptions(int id);
        public Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int id);
        #endregion
    }
}
