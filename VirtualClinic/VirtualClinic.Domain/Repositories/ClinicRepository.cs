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


        //Made with https://patorjk.com/software/taag/#p=display&f=Big
        /*    _____      _   _            _       
         *   |  __ \    | | (_)          | |      
         *   | |__) |_ _| |_ _  ___ _ __ | |_ ___ 
         *   |  ___/ _` | __| |/ _ \ '_ \| __/ __|
         *   | |  | (_| | |_| |  __/ | | | |_\__ \
         *   |_|   \__,_|\__|_|\___|_| |_|\__|___/
         */
        #region Patients

        /// <summary>
        /// Get's all the patients from the DB.
        /// </summary>
        /// <returns>A list of paitents</returns>
        public IEnumerable<Models.Patient> GetPatients()
        {
            var DBPatients = _context.Patients
                    .Include(thing => thing.PatientReports)
                    .ToList();

            List<Models.Patient> ModelPatients = new List<Models.Patient>();

            foreach (var dbpatient in DBPatients)
            {
                ModelPatients.Add(DB_DomainMapper.MapPatient(dbpatient));
            }

            return ModelPatients;
        }

        public Task<IEnumerable<Models.Patient>> GetPatientsAsync()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get's a specific patient based on their ID
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throws an argument exception if there is no patient with that ID in the DB.
        /// </exception>
        /// <param name="patientId">The patient's ID in the database.</param>
        /// <returns>A model representation of that patient.</returns>
        public Models.Patient GetPatientByID(int patientId)
        {
            var DBPatient = _context.Patients.Find(patientId);

            if (DBPatient is not null)
            {
                return DB_DomainMapper.MapPatient(DBPatient);
            }
            else
            {
                throw new ArgumentException("Patient Not found in DB.");
            }
        }

        public Task<Models.Patient> GetPatientByIDAsync(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Add a patient to the database 
        /// </summary>
        /// <param name="patient">A patient to be added to the database</param>
        public void AddPatient(Models.Patient patient)
        {
            //todo: replace with mapper
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


        /// <summary>
        /// Add a patient to the database 
        /// </summary>
        /// <param name="patient">A patient to be added to the database</param>
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
        #endregion



        /*     _____             _                 
         *    |  __ \           | |                
         *    | |  | | ___   ___| |_ ___  _ __ ___ 
         *    | |  | |/ _ \ / __| __/ _ \| '__/ __|
         *    | |__| | (_) | (__| || (_) | |  \__ \
         *    |_____/ \___/ \___|\__\___/|_|  |___/                                
         */
        #region Doctors

        /// <summary>
        /// Get's all the doctors.
        /// </summary>
        /// <returns>A list of all the doctors.</returns>
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

        /// <summary>
        /// Get a list of doctors asynchornusly.
        /// </summary>
        /// <returns>A task that can be awaited with the list of doctors.</returns>
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



        /// <summary>
        /// Get a doctor with a specific Id
        /// </summary>
        /// <param name="id">The id of the doctor to be returned</param>
        /// <returns>A doctor with a list of its patient</returns>
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

            foreach (var patient in DBPatients)
            {
                Models.Patient next = new Models.Patient(patient.Id, patient.Name, patient.Dob);

                patients.Add(next);
            }

            return patients;
        }


        /// <summary>
        /// Get patients of a specific doctor
        /// </summary>
        /// <param name="id">The id of the doctor whose patients are being requested</param>
        /// <returns>A list of patients of a doctor</returns>
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

        /// <summary>
        /// Adds a doctor to the database
        /// </summary>
        /// <param name="doctor">The doctor to be added to the database</param>
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

        #endregion



        /*    _______ _                     _       _       
         *   |__   __(_)                   | |     | |      
         *      | |   _ _ __ ___   ___  ___| | ___ | |_ ___ 
         *      | |  | | '_ ` _ \ / _ \/ __| |/ _ \| __/ __|
         *      | |  | | | | | | |  __/\__ \ | (_) | |_\__ \
         *      |_|  |_|_| |_| |_|\___||___/_|\___/ \__|___/ 
         */
        #region Timeslot
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


        /// <summary>
        /// Get timeslot and apointment information for ones related to a specific patient.
        /// </summary>
        /// <remarks>
        /// NOTE: because this matches to a patient id which is stored on apointments,
        /// every item returned should have an apointment attached to the timeslot.
        /// </remarks>
        /// <param name="PatientId">The id of the desired patient in the DB.</param>
        /// <returns>List of timeslots with any apointment informtion filled in.</returns>
        public async Task<IEnumerable<Models.Timeslot>> GetPatientTimeslotsAsync(int PatientId)
        {
            List<DataModel.Timeslot> timeslots = await _context.Timeslots
                  .Include(ts => ts.Appointment)
                  .Where(ts => ts.Appointment.PatientId == PatientId)
                  .ToListAsync();

            return ConvertTimeslots(timeslots, true);
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

        /// <summary>
        /// Get timeslot and apointment information for ones related to a specific patient.
        /// </summary>
        /// <remarks>
        /// NOTE: because this matches to a patient id which is stored on apointments,
        /// every item returned should have an apointment attached to the timeslot.
        /// </remarks>
        /// <param name="PatientId">The id of the desired patient in the DB.</param>
        /// <returns>List of timeslots with any apointment informtion filled in.</returns>
        public async Task<IEnumerable<Models.Timeslot>> GetDoctorTimeslotsAsync(int doctorId)
        {
            List<DataModel.Timeslot> timeslots = await _context.Timeslots
                    .Include(ts => ts.Appointment)
                    .Where(ts => ts.DoctorId == doctorId)
                    .ToListAsync();

            return ConvertTimeslots(timeslots);
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

        #endregion



        /*    _____                       _       
         *   |  __ \                     | |      
         *   | |__) |___ _ __   ___  _ __| |_ ___ 
         *   |  _  // _ \ '_ \ / _ \| '__| __/ __|
         *   | | \ \  __/ |_) | (_) | |  | |_\__ \
         *   |_|  \_\___| .__/ \___/|_|   \__|___/
         *              | |                       
         *              |_|                      
         */
        #region Reports
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

            if (report is null)
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
        /// Given a report id, gets the report with the given id.
        /// </summary>
        /// <remarks>
        /// will not fill in the references to patient.
        /// </remarks>
        /// <param name="ReportID">The id value of the report in the DB</param>
        /// <returns>The patient report with the given ID</returns>
        public async Task<Models.PatientReport> GetPatientReportByIDAsync(int ReportId)
        {
            DataModel.PatientReport report = await _context.PatientReports.FindAsync(ReportId);

            if (report is null)
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
        /// Get's all of a single patient's reports.
        /// </summary>
        /// <param name="PatientId">The ID of the patient who's reports are desired.</param>
        /// <returns>All reports of the given patient.</returns>
        public IEnumerable<Models.PatientReport> GetPatientReports(int PatientId)
        {
            List<DataModel.PatientReport> reports = _context.PatientReports
                .Where(report => report.PatientId == PatientId)
                .ToList();

            List<Models.PatientReport> modelreports = new List<Models.PatientReport>();

            foreach (var r in reports)
            {
                modelreports.Add(DB_DomainMapper.MapReport(r));
            }

            return modelreports;
        }

        public Task<IEnumerable<Models.Prescription>> GetPatientReportsAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a patient report to the database
        /// </summary>
        /// <param name="report">The report to be added tp the database</param>
        public void AddPatientReport(Models.PatientReport report)
        {
            var newPatientReport = new DataModel.PatientReport
            {

                PatientId = report.Patient.Id,
                ReportTime = report.Time,
                Information = report.Info
                //add vitals id for datamodel??

            };

            _context.Add(newPatientReport);
            _context.SaveChanges();

        }

        /// <summary>
        /// Add a patient report to the database
        /// </summary>
        /// <param name="report">The report to be added tp the database</param>
        public async Task AddPatientReportAsync(Models.PatientReport report)
        {
            var newPatientReport = new DataModel.PatientReport
            {

                PatientId = report.Patient.Id,
                ReportTime = report.Time,
                Information = report.Info

            };

            await _context.AddAsync(newPatientReport);
            await _context.SaveChangesAsync();
        }
        #endregion






        /*    _____                         _       _   _                 
         *   |  __ \                       (_)     | | (_)                
         *   | |__) | __ ___  ___  ___ _ __ _ _ __ | |_ _  ___  _ __  ___ 
         *   |  ___/ '__/ _ \/ __|/ __| '__| | '_ \| __| |/ _ \| '_ \/ __|
         *   | |   | | |  __/\__ \ (__| |  | | |_) | |_| | (_) | | | \__ \
         *   |_|   |_|  \___||___/\___|_|  |_| .__/ \__|_|\___/|_| |_|___/
         *                                   | |                          
         *                                   |_|                         
         */
        #region Prescriptions
        /// <summary>
        /// Get's a specific prescription by it's ID
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Throws an argument exception if there is no perscription with that ID in the DB.
        /// </exception>
        /// <param name="PerscriptionId">The ID of the desired prescription</param>
        /// <returns>The prescription with the given ID.</returns>
        public Models.Prescription GetPrescription(int PerscriptionId)
        {
            var script = _context.Prescriptions.Find(PerscriptionId);

            if (script is not null)
            {
                return DB_DomainMapper.MapPrescription(script);
            }
            else
            {
                throw new ArgumentException("Prescription, {id}, not found.");
            }
        }

        public Task<bool> GetPrescriptionAsync(int PerscriptionId)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Gets a patient's perscriptions.
        /// </summary>
        /// <param name="patientID">The patient's ID</param>
        /// <returns>The patient's prescriptions.</returns>
        public IEnumerable<Models.Prescription> GetPatientPrescriptions(int patientID)
        {
            var DbPerscriptions = _context.Prescriptions
                .Where(p => p.PatientId == patientID)
                .ToList();

            List<Models.Prescription> modelPresciptions = new List<Models.Prescription>();

            foreach (var script in DbPerscriptions)
            {
                modelPresciptions.Add(DB_DomainMapper.MapPrescription(script));
            }

            return modelPresciptions;
        }

        public Task<IEnumerable<Models.Prescription>> GetPatientPrescriptionsAsync(int id)
        {
            throw new NotImplementedException();
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
                DoctorId = prescription.Doctor.Id,
            };

            _context.Add(newPrescription);
            _context.SaveChanges();

        }


        /// <summary>
        /// Add a prescription to the database
        /// </summary>
        /// <param name="prescription">The prescition to be added to the databse</param>
        public async Task<bool> AddPrescriptionAsync(Models.Prescription prescription)
        {
            var newPrescription = new DataModel.Prescription
            {
                Information = prescription.Info,
                Drug = prescription.DrugName,
                PatientId = prescription.Patient.Id,
                DoctorId = prescription.Doctor.Id,
            };

            await _context.AddAsync(newPrescription);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion



        /*    _    _      _                     
         *   | |  | |    | |                    
         *   | |__| | ___| |_ __   ___ _ __ ___ 
         *   |  __  |/ _ \ | '_ \ / _ \ '__/ __|
         *   | |  | |  __/ | |_) |  __/ |  \__ \
         *   |_|  |_|\___|_| .__/ \___|_|  |___/
         *                 | |                  
         *                 |_|                  
         */
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
                if (DBTimeSlot.Appointment is not null)
                {
                    modelts.Appointment = DB_DomainMapper.MapApointment(DBTimeSlot.Appointment);
                }
                else if (AllowNullApointmentsFlag)
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
