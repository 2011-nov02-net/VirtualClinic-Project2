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
    public class ClinicRepository : IClinicRepository
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

        public async Task<IEnumerable<Models.Patient>> GetPatientsAsync()
        {
            var DBPatients = await _context.Patients
                .Include(thing => thing.PatientReports)
                .ToListAsync();

            List<Models.Patient> ModelPatients = new List<Models.Patient>();

            foreach (var dbpatient in DBPatients)
            {
                ModelPatients.Add(DB_DomainMapper.MapPatient(dbpatient));
            }

            return ModelPatients;
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

        public async Task<Models.Patient> GetPatientByIDAsync(int patientId)
        {
            var DBPatient = await _context.Patients.FindAsync(patientId);

            if (DBPatient is not null)
            {
                return DB_DomainMapper.MapPatient(DBPatient);
            }
            else
            {
                throw new ArgumentException("Patient Not found in DB.");
            }
        }


        /// <summary>
        /// Add a patient to the database 
        /// </summary>
        /// <param name="patient">A patient to be added to the database</param>
        public Models.Patient AddPatient(Models.Patient patient)
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
            patient.Id = newPatient.Id;
            return patient;
        }

        /// <summary>
        /// Add a patient to the database 
        /// </summary>
        /// <param name="patient">A patient to be added to the database</param>
        public async Task<Models.Patient> AddPatientAsync(Models.Patient patient)
        {

            if(patient.PrimaryDoctor is null)
            {
                var dr = _context.Doctors.First();
                var patientdr = new Domain.Models.Doctor(dr.Id, dr.Name);
                patient.PrimaryDoctor = patientdr;
            }
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
            patient.Id = newPatient.Id;
            return patient;
        }

        /// <summary>
        /// Update a patient in the database
        /// </summary>
        /// <param name="id">The id of teh patient to be updated</param>
        /// <param name="patient">The update </param>
        /// <returns>True if the patient is in the datase and has been updated </returns>
        public async Task<bool> UpdatePatientAsync(int id, Models.Patient patient)
        {
            var updatedPatient = await _context.Patients.Where(p => p.Id == id).FirstAsync();
            if (updatedPatient == null)
            {
                return false;
            }else
            {

            updatedPatient.Id = patient.Id;
            updatedPatient.Name = patient.Name;
            updatedPatient.DoctorId = patient.PrimaryDoctor.Id;
            updatedPatient.Ssn = patient.SSN;
            updatedPatient.Dob = patient.DateOfBirth;

                await _context.SaveChangesAsync();
                return true;
            }

        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception(" This patient doesn't exist");
            }
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
            var DBDoctor = _context.Doctors.Find(doctorId);

            var doctor = new Models.Doctor(DBDoctor.Id, DBDoctor.Name, DBDoctor.Title);

            return doctor;
        }

        /// <summary>
        /// Get a doctor with a specific Id
        /// </summary>
        /// <param name="id">The id of the doctor to be returned</param>
        /// <returns>A doctor with a list of its patient</returns>
        public async Task<Models.Doctor> GetDoctorByIDAsync(int doctorId)
        {
            var DBDoctor = await _context.Doctors.FindAsync(doctorId);

            var doctor = new Models.Doctor(DBDoctor.Id, DBDoctor.Name, DBDoctor.Title);

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
            var doctor = GetDoctorByID(doctorId);

            List<Models.Patient> patients = new List<Models.Patient>();

            foreach (var patient in DBPatients)
            {
                Models.Patient next = new Models.Patient(patient.Id, patient.Name, patient.Dob, patient.Ssn, patient.Insurance);
                next.PrimaryDoctor = doctor;
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
            var doctor = await GetDoctorByIDAsync(doctorId);

            List<Models.Patient> patients = new List<Models.Patient>();

            foreach (var patient in DBPatients)
            {
                Models.Patient next = new Models.Patient(patient.Id, patient.Name, patient.Dob, patient.Ssn, patient.Insurance);
                next.PrimaryDoctor = doctor;
                patients.Add(next);
            }

            return patients;
        }


        /// <summary>
        /// Adds a doctor to the database
        /// </summary>
        /// <param name="doctor">The doctor to be added to the database</param>
        public Models.Doctor AddDoctor(Models.Doctor doctor)
        {
            var newDoctor = new DataModel.Doctor
            {
                Name = doctor.Name,
                Title = doctor.Title
            };
            _context.Doctors.Add(newDoctor);
            _context.SaveChanges();
            doctor.Id = newDoctor.Id;
            return doctor;
        }

        /// <summary>
        /// Adds a doctor to the database
        /// </summary>
        /// <param name="doctor">The doctor to be added to the database</param>
        public async Task<Models.Doctor> AddDoctorAsync(Models.Doctor doctor)
        {
            var newDoctor = new DataModel.Doctor
            {
                Name = doctor.Name,
                Title = doctor.Title
            };
            await _context.Doctors.AddAsync(newDoctor);
            await _context.SaveChangesAsync();
            doctor.Id = newDoctor.Id;
            return doctor;
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
        public Models.Timeslot AddTimeslot(Models.Timeslot timeslot)
        {
            DataModel.Timeslot DBTimeslot = new DataModel.Timeslot();

            //DBTimeslot.Id = timeslot.Id;
            //todo: check if Id is valid in DB, and if not, throw some argument exception.

            DBTimeslot.DoctorId = timeslot.DoctorId;
            DBTimeslot.Start = timeslot.Start;
            DBTimeslot.End = timeslot.End;
            DBTimeslot.AppointmentId = timeslot.Appointment?.Id;

            //DBTimeslot.DoctorId = timeslot.dr.id;      

            _context.Timeslots.Attach(DBTimeslot);
            if (timeslot.Appointment is not null)
            {
                // TODO: construct appointment record and insert into table

                DataModel.Appointment appointment = new DataModel.Appointment
                {
                    Notes = timeslot.Appointment.Notes,
                    PatientId = timeslot.Appointment.PatientId,
                    DoctorId = timeslot.Appointment.DoctorId,
                    Start = timeslot.Start,
                    End = timeslot.End
                };
                _context.Appointments.Add(appointment);

            }

            _context.Timeslots.Add(DBTimeslot);
            _context.SaveChanges();
            timeslot.Id = DBTimeslot.Id;
            return timeslot;
        }

        public async Task<Models.Timeslot> AddTimeslotAsync(Models.Timeslot timeslot)
        {
            DataModel.Timeslot DBTimeslot = new DataModel.Timeslot();

            //DBTimeslot.Id = timeslot.Id;
            //todo: check if Id is valid in DB, and if not, throw some argument exception.

            DBTimeslot.DoctorId = timeslot.DoctorId;
            DBTimeslot.Start = timeslot.Start;
            DBTimeslot.End = timeslot.End;
            DBTimeslot.AppointmentId = timeslot.Appointment?.Id;

            //DBTimeslot.DoctorId = timeslot.dr.id;

            if (timeslot.Appointment is not null)
            {
                // TODO: construct appointment record and insert into table

                DataModel.Appointment appointment = new DataModel.Appointment
                {
                    Notes = timeslot.Appointment.Notes,
                    PatientId = timeslot.Appointment.PatientId,
                    DoctorId = timeslot.Appointment.DoctorId,
                    Start = timeslot.Start,
                    End = timeslot.End
                };
                await _context.Appointments.AddAsync(appointment);

            }

            await _context.Timeslots.AddAsync(DBTimeslot);
            _context.SaveChanges();
            timeslot.Id = DBTimeslot.Id;
            return timeslot;
        }



        public Models.Appointment AddAppointmentToTimeslot(Models.Appointment appointment, int TimeslotId)
        {
            var timeslot = _context.Timeslots.Find(TimeslotId);

            if (timeslot == null)
            {
                throw new ArgumentException($"Timeslot id:{TimeslotId} can't be modified because it doesn't exist");
            }
            if (timeslot.AppointmentId is not null)
            {
                throw new ArgumentException($"Timeslot id:{TimeslotId} already has an appointment");
            }

            var new_appointment = new DataModel.Appointment
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Start = timeslot.Start,
                End = timeslot.End
            };

            _context.Appointments.Add(new_appointment);
            _context.SaveChanges();
            timeslot.AppointmentId = new_appointment.Id;
            appointment.Id = new_appointment.Id;
            _context.SaveChanges();
            return appointment;
        }
        public async Task<Models.Appointment> AddAppointmentToTimeslotAsync(Models.Appointment appointment, int TimeslotId)
        {
            var timeslot = await _context.Timeslots.FindAsync(TimeslotId);

            if (timeslot == null)
            {
                throw new ArgumentException($"Timeslot id:{TimeslotId} can't be modified because it doesn't exist");
            }
            if (timeslot.AppointmentId is not null)
            {
                throw new ArgumentException($"Timeslot id:{TimeslotId} already has an appointment");
            }

            var new_appointment = new DataModel.Appointment
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Start = timeslot.Start,
                End = timeslot.End
            };

            await _context.Appointments.AddAsync(new_appointment);
            await _context.SaveChangesAsync();
            timeslot.AppointmentId = new_appointment.Id;
            appointment.Id = new_appointment.Id;
            await _context.SaveChangesAsync();

            return appointment;
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
                throw new ArgumentException($"Patient Report ID {ReportID} Not Found in DB.");
            }

            var modelreport = DB_DomainMapper.MapReport(report);

            //technically could be null, but shouldn't be because this ID comes from DB information.

            if (report.VitalsId is not null)
            {
                modelreport.Vitals = DB_DomainMapper.MapVitals(_context.Vitals.Find(report.VitalsId));
            }

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
                throw new ArgumentException($"Patient Report ID {ReportId} Not Found in DB.");
            }

            var modelreport = DB_DomainMapper.MapReport(report);

            //technically could be null, but shouldn't be because this ID comes from DB information.

            if (report.VitalsId is not null)
            {
                modelreport.Vitals = DB_DomainMapper.MapVitals(_context.Vitals.Find(report.VitalsId));
            }

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
                r.PatientId = PatientId;
                modelreports.Add(DB_DomainMapper.MapReport(r));
            }

            return modelreports;
        }

        public async Task<IEnumerable<Models.PatientReport>> GetPatientReportsAsync(int PatientId)
        {
            List<DataModel.PatientReport> reports = await  _context.PatientReports
               .Where(report => report.PatientId == PatientId)
               .ToListAsync();

            List<Models.PatientReport> modelreports = new List<Models.PatientReport>();

            foreach (var r in reports)
            {
                modelreports.Add(DB_DomainMapper.MapReport(r));
            }

            return modelreports;
        }

        /// <summary>
        /// Add a patient report to the database
        /// </summary>
        /// <param name="report">The report to be added tp the database</param>
        public Models.PatientReport AddPatientReport(Models.PatientReport report)
        {
            var newPatientReport = new DataModel.PatientReport
            {

                PatientId = report.PatientId,
                ReportTime = report.Time,
                Information = report.Info
                //add vitals id for datamodel??

            };

            _context.Add(newPatientReport);
            _context.SaveChanges();
            report.Id = newPatientReport.Id;
            return report;
        }

        /// <summary>
        /// Add a patient report to the database
        /// </summary>
        /// <param name="report">The report to be added tp the database</param>
        public async Task<Models.PatientReport> AddPatientReportAsync(Models.PatientReport report)
        {
            var newPatientReport = new DataModel.PatientReport
            {

                PatientId = report.PatientId,
                ReportTime = report.Time,
                Information = report.Info

            };

            await _context.AddAsync(newPatientReport);
            await _context.SaveChangesAsync();
            report.Id = newPatientReport.Id;
            return report;
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
        public Models.Prescription GetPrescription(int PrescriptionId)
        {
            var script = _context.Prescriptions.Find(PrescriptionId);

            if (script is not null)
            {
                return DB_DomainMapper.MapPrescription(script);
            }
            else
            {
                throw new ArgumentException($"Prescription, {PrescriptionId}, not found.");
            }
        }

        public async Task<Models.Prescription> GetPrescriptionAsync(int PrescriptionId)
        {
            var script = await _context.Prescriptions.FindAsync(PrescriptionId);

            if (script is not null)
            {
                return DB_DomainMapper.MapPrescription(script);
            }
            else
            {
                throw new ArgumentException($"Prescription, {PrescriptionId}, not found.");
            }
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

        public async Task<IEnumerable<Models.Prescription>> GetPatientPrescriptionsAsync(int patientID)
        {
            var DbPerscriptions = await _context.Prescriptions
                .Where(p => p.PatientId == patientID)
                .ToListAsync();

            List<Models.Prescription> modelPresciptions = new List<Models.Prescription>();

            foreach (var script in DbPerscriptions)
            {
                modelPresciptions.Add(DB_DomainMapper.MapPrescription(script));
            }

            return modelPresciptions;
        }

        /// <summary>
        /// Add a prescription to the database
        /// </summary>
        /// <param name="prescription">The prescition to be added to the databse</param>
        public Models.Prescription AddPrescription(Models.Prescription prescription)
        {
            var newPrescription = new DataModel.Prescription
            {
                Information = prescription.Info,
                Drug = prescription.DrugName,
                PatientId = prescription.PatientId,
                DoctorId = prescription.DoctorId
            };

            _context.Add(newPrescription);
            _context.SaveChanges();
            prescription.Id = newPrescription.Id;
            return prescription;
        }

        /// <summary>
        /// Add a prescription to the database
        /// </summary>
        /// <param name="prescription">The prescition to be added to the databse</param>
        public async Task<Models.Prescription> AddPrescriptionAsync(Models.Prescription prescription)
        {
            var newPrescription = new DataModel.Prescription
            {
                Information = prescription.Info,
                Drug = prescription.DrugName,
                PatientId = prescription.PatientId,
                DoctorId = prescription.DoctorId,
            };

            await _context.AddAsync(newPrescription);
            await _context.SaveChangesAsync();
            prescription.Id = newPrescription.Id;
            return prescription;
        }
        #endregion

        public string GetAuthType(string userEmail)
        {
            var type = _context.Users.Find(userEmail);
            if (type is null)
            {
                throw new ArgumentException($"Email {userEmail} does not exist");
            }
            return type.UserType;
        }

        public async Task<string> GetAuthTypeAsync(string userEmail)
        {
            var type = await _context.Users.FindAsync(userEmail);
            if (type is null)
            {
                throw new ArgumentException($"Email {userEmail} does not exist");
            }
            return type.UserType;
        }


        /// <summary>
        /// Currently, this creates a new patient with placeholder values, inserts it into the db, and then creates a new 
        /// patient type user and inserts that into the db, and returns information on the new user.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <returns>the created user object from the db</returns>
        public async Task<UserModel> AddAuthorizedPatientAsync(string Email)
        {
            DataModel.User newuser = new DataModel.User();

            newuser.Email = Email;
            newuser.UserType = "patient";

            Models.Patient pat = new Models.Patient(-1, "Dr.", DateTime.Now, "1234-56-678","Unknown Insurance.");

            //should be removed after AddPatientAsynch returns the patient created 
            //with real id, then update id bellow in commented out code
            pat.Id = _context.Patients.Max(patient => patient.Id) + 1;

            var newDbPat = AddPatientAsync(pat);

            //uncomment when add patients asynch is added
            //pat.Id = newDbPat.id;
            newuser.Id = pat.Id;

            var user = await _context.Users.AddAsync(newuser);

            return new UserModel(newuser.Id, Email);
        }

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
