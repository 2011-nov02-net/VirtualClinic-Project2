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

        /// <summary>
        /// Adds a doctor to the database
        /// </summary>
        /// <param name="doctor">The doctor to be added to the database</param>
        public void AddDoctor(Models.Doctor doctor)
        {
           var newDoctor = new DataModel.Doctor
           {
               Name = doctor.Name,
               Title = doctor.Title
           };
           _context.Doctors.Add(newDoctor);
           _context.SaveChanges();
        }

        public async Task AddDoctorAsync(Models.Doctor doctor)
        {
            var newDoctor = new DataModel.Doctor
           {
               Name = doctor.Name,
               Title = doctor.Title
           };
           await _context.Doctors.AddAsync(newDoctor);
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add a patient to the database 
        /// </summary>
        /// <param name="patient">A patient to be added to the database</param>
        public void AddPatient(Models.Patient patient)
        {
            var newPatient = new DataModel.Patient
            {
                Name = patient.Name,
                Dob = patient.DateOfBirth,
                DoctorId = patient.PrimaryDoctor.Id,
                Ssn = patient.SSN,
                Insurance = patient.InsuranceProvider
            };
            _context.Patients.Add(newPatient);
            _context.SaveChanges();

        }

        public async Task<bool> AddPatientAsync(Models.Patient patient)
        {
            var newPatient = new DataModel.Patient
            {
                Name = patient.Name,
                Dob = patient.DateOfBirth,
                DoctorId = patient.PrimaryDoctor.Id,
                Ssn = patient.SSN,
                Insurance = patient.InsuranceProvider
            };
           await _context.Patients.AddAsync(newPatient);
           await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Add a patient report to the database
        /// </summary>
        /// <param name="report">The report to be added tp the database</param>
        public void AddPatientReport(Models.PatientReport report)
        {
            var newPatientReport = new DataModel.PatientReport
            {

                PatientId  = report.Patient.Id,
                ReportTime = report.Time,
                Information = report.Info
           //add vitals id for datamodel??

            };

            _context.Add(newPatientReport);
            _context.SaveChanges();

        }

        public async Task AddPatientReportAsync(Models.PatientReport report)
        {
            var newPatientReport = new DataModel.PatientReport
            {

                PatientId = report.Patient.Id,
                ReportTime = report.Time,
                Information = report.Info

            };

            await _context.AddAsync(newPatientReport);
           await  _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add a prescription to the database
        /// </summary>
        /// <param name="prescription">The prescition to be added to the databse</param>
        public void AddPrescription(Models.Prescription prescription)
        {
            var newPrescription = new DataModel.Prescription
            {
                Information = prescription.Info,
                Drug = prescription.DrugName,
                PatientId = prescription.Patient.Id,
                DoctorId  = prescription.Doctor.Id,
            };

            _context.Add(newPrescription);
            _context.SaveChanges();

        }

        public async Task<bool> AddPrescriptionAsync(Models.Prescription prescription)
        {
            var newPrescription = new DataModel.Prescription
            {
                Information = prescription.Info,
                Drug = prescription.DrugName,
                PatientId = prescription.Patient.Id,
                DoctorId = prescription.Doctor.Id,
            };

           await  _context.AddAsync(newPrescription);
           await  _context.SaveChangesAsync();
            return true;
        }

        
        /// <summary>
        /// Creates a DB timeslot based on a Model Timeslot, and sends it to the DB.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throws an argument exception if the id
        /// of the given timeslot is already in use on the DB
        /// </exception>
        /// <param name="timeslot">A Model Timeslot </param>
        public void AddTimeslot(Models.Timeslot timeslot)
        {
            DataModel.Timeslot DBTimeslot = new DataModel.Timeslot();

            //DBTimeslot.Id = timeslot.Id;
            //todo: check if Id is valid in DB, and if not, throw some argument exception.

            DBTimeslot.AppointmentId = timeslot.Appointment?.Id;
            //DBTimeslot.DoctorId = timeslot.dr.id;         

            throw new NotImplementedException();

            _context.Timeslots.Attach(DBTimeslot);
            _context.SaveChanges();
        }

        public Task AddTimeslotAsync(Models.Timeslot timeslot)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get a doctor with a specific Id
        /// </summary>
        /// <param name="id">The id of the doctor to be returned</param>
        /// <returns>A doctor with a list of its patient</returns>
        public Models.Doctor GetDoctorByID(int doctorId)
        {

            var DBDoctor = _context.Doctors.Where(o => o.Id == doctorId).First();

            var doctor = new Models.Doctor(DBDoctor.Id, DBDoctor.Name, DBDoctor.Title)
            {
                Patients = (List<Models.Patient>)GetDoctorPatients(DBDoctor.Id)
            };

            return doctor;
        }

        public async Task<Models.Doctor> GetDoctorByIDAsync(int doctorId)
        {
            var DBDoctor = await _context.Doctors.Where(o => o.Id == doctorId).FirstAsync();

            var doctor = new Models.Doctor(DBDoctor.Id, DBDoctor.Name, DBDoctor.Title)
            {
                Patients = (List<Models.Patient>)await GetDoctorPatientsAsync(DBDoctor.Id)
            };

            return doctor;
        }


        /// <summary>
        /// Get patients of a specific doctor
        /// </summary>
        /// <param name="id">The id of the doctor whose patients are being requested</param>
        /// <returns>A list of patients of a doctor</returns>

        public IEnumerable<Models.Patient> GetDoctorPatients(int doctorId)
        {
            var DBPatients = _context.Patients.Where(o => o.DoctorId == doctorId).ToList();

            List<Models.Patient> patients = new List<Models.Patient>();

            foreach( var patient in DBPatients)
            {
                Models.Patient next = new Models.Patient(patient.Id, patient.Name, patient.Dob);

                patients.Add(next);
            }

            return patients;
        }


        public async Task<IEnumerable<Models.Patient>> GetDoctorPatientsAsync(int doctorId)
        {
            var DBPatients = await _context.Patients.Where(o => o.DoctorId == doctorId).ToListAsync();

            List<Models.Patient> patients = new List<Models.Patient>();

            foreach (var patient in DBPatients)
            {
                Models.Patient next = new Models.Patient(patient.Id, patient.Name, patient.Dob);

                patients.Add(next);
            }

            return patients;
        }


        /// <summary>
        /// Get All doctors
        /// </summary>
        /// <returns>A list of all Doctors</returns>
        public IEnumerable<Models.Doctor> GetDoctors()
        {
            var DBDoctors = _context.Doctors
               .Include(o => o.Patients)
               .ToList();

            List<Models.Doctor> ModelDoctors = new List<Models.Doctor>();

            foreach (var dbdoctor in DBDoctors)
            {
                Models.Doctor next = new Models.Doctor(dbdoctor.Id, dbdoctor.Name, dbdoctor.Title);
          
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
                Models.Doctor next = new Models.Doctor(dbdoctor.Id, dbdoctor.Name, dbdoctor.Title);
                
                ModelDoctors.Add(next);
            }

            return ModelDoctors;

        }

        /// <summary>
        /// Get all timeslots related to a doctor.
        /// </summary>
        /// <param name="doctorId">The id of the doctor who's timeslots are to be retrieved.</param>
        /// <remarks>
        /// Note: references to the dr, and patient are left out inside these.
        /// </remarks>
        /// <returns>List of timeslots with any apointments filled in.</returns>
        public IEnumerable<Models.Timeslot> GetDoctorTimeslots(int doctorId)
        {
            List<DataModel.Timeslot> timeslots = _context.Timeslots
                    .Include(ts => ts.Appointment)
                    .Where(ts => ts.DoctorId == doctorId)
                    .ToList();

            return ConvertTimeslots(timeslots);
        }

        public async Task<IEnumerable<Models.Timeslot>> GetDoctorTimeslotsAsync(int doctorId)
        {
            List<DataModel.Timeslot> timeslots = await _context.Timeslots
                    .Include(ts => ts.Appointment)
                    .Where(ts => ts.DoctorId == doctorId)
                    .ToListAsync();

            return ConvertTimeslots(timeslots);
        }


        /// <summary>
        /// Get a patient by the patients Id
        /// </summary>
        /// <param name="patientId">The Id of the patient to be retrieved </param>
        /// <returns>A patient </returns>
        public Models.Patient GetPatientByID(int patientId)
        {
            var DBPatient = _context.Patients.Where(o => o.Id == patientId).First();

            var doctor = new Models.Doctor()
            {
                Id = DBPatient.Doctor.Id,
                Name = DBPatient.Doctor.Name,
                Title = DBPatient.Doctor.Title

            };

            var patient = new Models.Patient(DBPatient.Id, DBPatient.Name, DBPatient.Dob, doctor);
           

            return patient;
        }

        public async Task<Models.Patient> GetPatientByIDAsync(int patientId)
        {
            var DBPatient = await _context.Patients.Where(o => o.Id == patientId).FirstAsync();

            var doctor = new Models.Doctor()
            {
                Id = DBPatient.Doctor.Id,
                Name = DBPatient.Doctor.Name,
                Title = DBPatient.Doctor.Title

            };

            var patient = new Models.Patient(DBPatient.Id, DBPatient.Name, DBPatient.Dob, doctor);


            return patient;
        }

        public IEnumerable<Models.Prescription> GetPatientPrescriptions(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Prescription>> GetPatientPrescriptionsAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Given a report id, gets the report with the given id.
        /// </summary>
        /// <remarks>
        /// will not fill in the references to patient.
        /// </remarks>
        /// <param name="ReportID">The id value of the report in the DB</param>
        /// <returns>The patient report with the given ID</returns>
        public Models.PatientReport GetPatientReportByID(int ReportID)
        {
            DataModel.PatientReport report = _context.PatientReports.Find(ReportID);

            if(report is null)
            {
                //then no report with that id exists in the DB
                throw new ArgumentException("ID Not Found in DB.");
            }


            var modelreport = DB_DomainMapper.MapReport(report);
            //technically could be null, but shouldn't be because this ID comes from DB information.
            modelreport.Vitals = DB_DomainMapper.MapVitals(_context.Vitals.Find(report.VitalsId));
            return modelreport;
        }

        public async Task<Models.PatientReport> GetPatientReportByIDAsync(int ReportId)
        {
            DataModel.PatientReport report = await _context.PatientReports.FindAsync(ReportId);

            if(report is null)
            {
                //then no report with that id exists in the DB
                throw new ArgumentException("ID Not Found in DB.");
            }


            var modelreport = DB_DomainMapper.MapReport(report);
            //technically could be null, but shouldn't be because this ID comes from DB information.
            modelreport.Vitals = DB_DomainMapper.MapVitals(_context.Vitals.Find(report.VitalsId));
            return modelreport;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Models.PatientReport> GetPatientReports(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Prescription>> GetPatientReportsAsync(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
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

            return ConvertTimeslots(timeslots, true);        
        }

        public async Task<IEnumerable<Models.Timeslot>> GetPatientTimeslotsAsync(int PatientId)
        {
              List<DataModel.Timeslot> timeslots = await _context.Timeslots
                    .Include(ts => ts.Appointment)
                    .Where(ts => ts.Appointment.PatientId == PatientId)
                    .ToListAsync ();

            return ConvertTimeslots(timeslots, true); 
        }

        #region PrivateHelpers
        /// <summary>
        /// Takes a list of db timeslots and converts them to apointments
        /// </summary>
        /// <param name="timeslots">List of DB timeslots</param>
        /// <returns>List of model timeslots</returns>
        private static List<Models.Timeslot> ConvertTimeslots(List<DataModel.Timeslot> timeslots, bool AllowNullApointmentsFlag = false)
        {
            List<Models.Timeslot> modelTimeslots = new List<Models.Timeslot>();

            foreach (var DBTimeSlot in timeslots)
            {
                Models.Timeslot modelts = DB_DomainMapper.MapTimeslot(DBTimeSlot);

                //does not fill in dr or patient
                if(DBTimeSlot.Appointment is not null)
                {
                    modelts.Appointment = DB_DomainMapper.MapApointment(DBTimeSlot.Appointment);
                } else if (AllowNullApointmentsFlag)
                {
                    throw new NullReferenceException("A timeslot has a null apointment reference");
                }
                
                modelTimeslots.Add(modelts);
            }

            return modelTimeslots;
        }

        #endregion
    }
}
