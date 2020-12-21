using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using VirtualClinic.DataModel;
using VirtualClinic.Domain.Repositories;
using VirtualClinic.Domain;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Tests
{
    public partial class UnitTests
    {
        public SqliteConnection Database_init()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;

            DataModel.Doctor[] doctors =
            {
                new DataModel.Doctor { Name = "Gregory House", Title = "Diagnostician", Id = 1 },
                new DataModel.Doctor { Name = "Critina Yang", Title = "Cardiologist", Id = 2 },
                new DataModel.Doctor { Name = "Perry Cox", Title = "Oncologist", Id = 3 },
                new DataModel.Doctor { Name = "Dana Scully", Title = "Radiologist", Id = 4 }
            };

            DataModel.Patient[] patients =
            {
                new DataModel.Patient {Id = 1, Name = "Jerry Smith", Dob = new DateTime(1977, 5, 13), DoctorId = 1, Insurance = "Medicare", Ssn = "230-98-9882" },
                new DataModel.Patient {Id = 2, Name = "Bird Person", Dob = new DateTime(1991, 2, 21), DoctorId = 2, Insurance = "Insuricare", Ssn = "273-88-3476" },
                new DataModel.Patient {Id = 3, Name = "Jotaro Kujo", Dob = new DateTime(1988, 5, 1), DoctorId = 1, Insurance = "Real Health", Ssn = "097-98-1127" },
                new DataModel.Patient {Id = 4, Name = "Walter White", Dob = new DateTime(1956, 3, 20), DoctorId = 3, Insurance = "Meth Health", Ssn = "435-88-5634" },
                new DataModel.Patient {Id = 5, Name = "Garfield", Dob = new DateTime(2001, 9, 30), DoctorId = 3, Insurance = "NixonCare", Ssn = "346-37-4858" },
                new DataModel.Patient {Id = 6, Name = "Johnny Silverhand", Dob = new DateTime(1990, 3, 2), DoctorId = 4, Insurance = "Insuricare", Ssn = "358-01-9678" },
                new DataModel.Patient {Id = 7, Name = "Richard Nixon", Dob = new DateTime(1999, 8, 17), DoctorId = 4, Insurance = "Medicare", Ssn = "426-33-1348" },
                new DataModel.Patient {Id = 8, Name = "Beth Smith", Dob = new DateTime(1976, 7, 21), DoctorId = 4, Insurance = "Medicare", Ssn = "456-67-8523" }
            };

            DataModel.PatientReport[] patientReports =
            {
                new DataModel.PatientReport {Id = 1, VitalsId = 1, Information = "I am dying to death", PatientId = 1, ReportTime = new DateTime(2018, 7, 11, 12, 31, 32)},
                new DataModel.PatientReport {Id = 2, VitalsId = 2, Information = "I am dying", PatientId = 3, ReportTime = new DateTime(2020, 9, 22, 9, 1, 7)},
                new DataModel.PatientReport {Id = 3, VitalsId = 3, Information = "My hands are cold", PatientId = 4, ReportTime = new DateTime(2020, 3, 16, 4, 55, 22)},
                new DataModel.PatientReport {Id = 4, VitalsId = 4, Information = "I am dying to death again", PatientId = 1, ReportTime = new DateTime(2019, 1, 21, 6, 29, 23)},
                new DataModel.PatientReport {Id = 5, VitalsId = null, Information = "I saw a pretty bird by the window", PatientId = 2, ReportTime = new DateTime(2020, 7, 19, 20, 45, 11)},
                new DataModel.PatientReport {Id = 6, VitalsId = null, Information = "My goldfish has alzheimers", PatientId = 5, ReportTime = new DateTime(2019, 3, 25, 11, 19, 6)}
            };

            DataModel.Prescription[] prescriptions =
            {
                new DataModel.Prescription {Id = 1, DoctorId = 1, PatientId = 1, Drug = "Mycoxafloppin", Information = "Take orally every 6 hours", Date = new DateTime(2020, 12, 6, 8, 12, 00)},
                new DataModel.Prescription {Id = 2, DoctorId = 1, PatientId = 1, Drug = "DuderSoluzone", Information = "Radically take", Date = new DateTime(2019, 5, 23, 12, 11, 00)},
                new DataModel.Prescription {Id = 3, DoctorId = 1, PatientId = 2, Drug = "Mafializone", Information = "Take it every day or you will regret it", Date = new DateTime(2020, 5, 29, 15, 46, 00)},
                new DataModel.Prescription {Id = 4, DoctorId = 3, PatientId = 4, Drug = "Ambien", Information = "Take orally every night", Date = new DateTime(2020, 7, 15, 13, 32, 00)},
                new DataModel.Prescription {Id = 5, DoctorId = 3, PatientId = 5, Drug = "gibberish", Information = "literally useless", Date = new DateTime(2018, 9, 3, 9, 17, 00)},
                new DataModel.Prescription {Id = 6, DoctorId = 4, PatientId = 6, Drug = "Mycoxafloppin", Information = "Take orally every 6 hours", Date = new DateTime(2019, 1, 6, 11, 15, 00)},
                new DataModel.Prescription {Id = 7, DoctorId = 4, PatientId = 7, Drug = "Mycoxafloppin", Information = "Take orally every 6 hours", Date = new DateTime(2020, 2, 29, 10, 9, 00)},
            };

            DataModel.Vital[] vitals =
            {
                new DataModel.Vital {Id = 1, Systolic = 120, Diastolic = 76, HeartRate = 78},
                new DataModel.Vital {Id = 2, Systolic = 140, Diastolic = 57, HeartRate = 76},
                new DataModel.Vital {Id = 3, Systolic = 142, Diastolic = 123, HeartRate = 81},
                new DataModel.Vital {Id = 4, Systolic = 117, Diastolic = 98, HeartRate = 149},
                new DataModel.Vital {Id = 5, Systolic = 120, Diastolic = 79, HeartRate = 201},
                new DataModel.Vital {Id = 6, Systolic = 154, Diastolic = 99, HeartRate = 189},
                new DataModel.Vital {Id = 7, Systolic = 175, Diastolic = 134, HeartRate = 110},
                new DataModel.Vital {Id = 8, Systolic = 132, Diastolic = 144, HeartRate = 90},
            };

            DataModel.Timeslot[] timeslots =
            {
               new DataModel.Timeslot {Id = 1, AppointmentId = null, DoctorId = 1, Start = new DateTime(2020, 12, 20, 8, 0, 0), End = new DateTime(2020, 12, 20, 8, 30, 0)},
               new DataModel.Timeslot {Id = 2, AppointmentId = null, DoctorId = 2, Start = new DateTime(2020, 12, 20, 8, 0, 0), End = new DateTime(2020, 12, 20, 17, 0, 0)},
               new DataModel.Timeslot {Id = 3, AppointmentId = null, DoctorId = 3, Start = new DateTime(2020, 12, 20, 8, 0, 0), End = new DateTime(2020, 12, 20, 17, 0, 0)},
               new DataModel.Timeslot {Id = 4, AppointmentId = null, DoctorId = 4, Start = new DateTime(2020, 12, 20, 8, 0, 0), End = new DateTime(2020, 12, 20, 17, 0, 0)},
               new DataModel.Timeslot {Id = 5, AppointmentId = 1, DoctorId = 1, Start = new DateTime(2020, 12, 20, 8, 0, 0), End = new DateTime(2020, 12, 20, 8, 30, 0)},
               new DataModel.Timeslot {Id = 6, AppointmentId = 2, DoctorId = 1, Start = new DateTime(2020, 12, 20, 9, 0, 0), End =  new DateTime(2020, 12, 20, 9, 30, 0)},
               new DataModel.Timeslot {Id = 7, AppointmentId = 3, DoctorId = 3, Start = new DateTime(2020, 12, 20, 11, 0, 0), End = new DateTime(2020, 12, 20, 12, 0, 0)},
               new DataModel.Timeslot {Id = 8, AppointmentId = 4, DoctorId = 4, Start = new DateTime(2020, 12, 21, 8, 0, 0), End = new DateTime(2020, 12, 21, 8, 30, 0)}

            };

            DataModel.Appointment[] appointments =
            {
               new DataModel.Appointment { Id = 1, Notes = "first appointment of the day", DoctorId = 1, PatientId = 1, VitalsId = 5, Start = new DateTime(2020, 12, 20, 8, 0, 0), End = new DateTime(2020, 12, 20, 8, 30, 0) },
               new DataModel.Appointment { Id = 2, Notes = "second appointment of the day", DoctorId = 1, PatientId = 3, VitalsId = 6, Start = new DateTime(2020, 12, 20, 9, 0, 0), End = new DateTime(2020, 12, 20, 9, 30, 0) },
               new DataModel.Appointment { Id = 3, Notes = "", DoctorId = 3, PatientId = 5, VitalsId = 7, Start = new DateTime(2020, 12, 20, 11, 0, 0), End = new DateTime(2020, 12, 20, 12, 0, 0) },
               new DataModel.Appointment { Id = 4, Notes = "very rude", DoctorId = 4, PatientId = 8, VitalsId = 8, Start = new DateTime(2020, 12, 21, 8, 0, 0), End = new DateTime(2020, 12, 21, 8, 30, 0) }
            };

            var context = new ClinicDbContext(options);

            context.Database.EnsureCreated();

            foreach (var doctor in doctors)
            {
                context.Doctors.Add(doctor);
            }
            foreach (var patient in patients)
            {
                context.Patients.Add(patient);
            }
            foreach (var report in patientReports)
            {
                context.PatientReports.Add(report);
            }
            foreach (var prescription in prescriptions)
            {
                context.Prescriptions.Add(prescription);
            }
            foreach (var vital in vitals)
            {
                context.Vitals.Add(vital);
            }
            foreach (var timeslot in timeslots)
            {
                context.Timeslots.Add(timeslot);
            }
            foreach (var appointment in appointments)
            {
                context.Appointments.Add(appointment);
            }

            context.SaveChanges();

            return connection;
        }
    }
}
