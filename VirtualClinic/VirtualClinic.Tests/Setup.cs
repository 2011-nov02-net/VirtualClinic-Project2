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
            context.SaveChanges();

            return connection;
        }
    }
}
