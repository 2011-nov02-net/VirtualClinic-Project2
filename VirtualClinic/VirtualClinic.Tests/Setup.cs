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
                new DataModel.PatientReport {Id = 1, VitalsId = null, Information = "I am dying to death", PatientId = 1, ReportTime = new DateTime(2018, 7, 11, 12, 31, 32)},
                new DataModel.PatientReport {Id = 2, VitalsId = null, Information = "I am dying", PatientId = 3, ReportTime = new DateTime(2020, 9, 22, 9, 1, 7)},
                new DataModel.PatientReport {Id = 3, VitalsId = null, Information = "My hands are cold", PatientId = 4, ReportTime = new DateTime(2020, 3, 16, 4, 55, 22)},
                new DataModel.PatientReport {Id = 4, VitalsId = null, Information = "I am dying to death again", PatientId = 1, ReportTime = new DateTime(2019, 1, 21, 6, 29, 23)},
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
            context.SaveChanges();

            return connection;
        }
    }
}
