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
        public Task<bool> UpdatePatientAsync(int id, Patient patient);
        public Task<bool> DeletePatientAsync(int id);
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
        public Task<bool> AddDoctorAsync(Doctor doctor);
        #endregion

        /// <summary>
        /// Timeslot related queries
        /// </summary>
        #region TimeSlot
        public IEnumerable<Timeslot> GetPatientTimeslots(int id);
        public Task<IEnumerable<Timeslot>> GetPatientTimeslotsAsync(int id);
        public IEnumerable<Timeslot> GetDoctorTimeslots(int id);
        public Task<IEnumerable<Timeslot>> GetDoctorTimeslotsAsync(int id);
        public void AddTimeslot(Timeslot timeslot);
        public Task AddTimeslotAsync(Timeslot timeslot);
        public void AddAppointmentToTimeslot(Appointment appointment, int TimeslotId);
        public Task<TimeSlot> AddAppointmentToTimeslotAsync(Appointment appointment, int TimeslotId);
        #endregion

        /// <summary>
        /// Patient report related queries
        /// </summary>
        #region Reports
        public PatientReport GetPatientReportByID(int id);
        public Task<PatientReport> GetPatientReportByIDAsync(int id);
        public IEnumerable<PatientReport> GetPatientReports(int id);
        public Task<IEnumerable<PatientReport>> GetPatientReportsAsync(int id);
        public void AddPatientReport(PatientReport report);
        public Task<bool> AddPatientReportAsync(PatientReport report);
        #endregion

        /// <summary>
        /// Prescription related queries
        /// </summary>
        #region Perscriptions
        public Prescription GetPrescription(int PerscriptionId);

        public Task<Prescription> GetPrescriptionAsync(int PerscriptionId);

        public IEnumerable<Prescription> GetPatientPrescriptions(int id);
        public Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int id);

        public void AddPrescription(Prescription prescription);
        public Task<bool> AddPrescriptionAsync(Prescription prescription);
        #endregion
    }
}
