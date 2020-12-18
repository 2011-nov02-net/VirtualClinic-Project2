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
    public class UnitTests
    {
        [Fact]
        public void AddDoctor_Database_Test()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            var doctor = new VirtualClinic.Domain.Models.Doctor(101, "Jerry Smith", "MD");

            using (var context = new ClinicDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

                repo.AddDoctor(doctor);
            }

            using var context2 = new ClinicDbContext(options);
            DataModel.Doctor doctorActual = context2.Doctors
                .Single(l => l.Name == "Jerry Smith");

            Assert.Equal(doctor.Name, doctorActual.Name);
            Assert.Equal(doctor.Title, doctorActual.Title);
        }
        [Fact]
        public async void AddDoctorAsync_Database_Test()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            var doctor = new VirtualClinic.Domain.Models.Doctor(101, "Jerry Smith", "MD");

            using (var context = new ClinicDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

                await repo.AddDoctorAsync(doctor);
            }

            using var context2 = new ClinicDbContext(options);
            DataModel.Doctor doctorActual = context2.Doctors
                .Single(l => l.Name == "Jerry Smith");

            Assert.Equal(doctor.Name, doctorActual.Name);
            Assert.Equal(doctor.Title, doctorActual.Title);

        }
        [Fact]
        public void AddPatient_Database_Test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patient = new VirtualClinic.Domain.Models.Patient("Billy Mays", DateTime.Now)
            {
                SSN = "293-38-0071",
                InsuranceProvider = "Umbrella Corp",
                PrimaryDoctor = repo.GetDoctorByID(1)
            };

            repo.AddPatient(patient);

            using var context2 = new ClinicDbContext(options);
            DataModel.Patient patientActual = context2.Patients
                .Single(l => l.Name == "Billy Mays");

            Assert.Equal(patient.Name, patientActual.Name);
            Assert.Equal(patient.SSN, patientActual.Ssn);
            Assert.Equal(patient.InsuranceProvider, patientActual.Insurance);
        }
        [Fact]
        public async void AddPatientAsync_Database_Test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patient = new VirtualClinic.Domain.Models.Patient("Billy Mays", DateTime.Now)
            {
                SSN = "293-38-0071",
                InsuranceProvider = "Umbrella Corp",
                PrimaryDoctor = repo.GetDoctorByID(1)
            };

            await repo.AddPatientAsync(patient);

            using var context2 = new ClinicDbContext(options);
            DataModel.Patient patientActual = context2.Patients
                .Single(l => l.Name == "Billy Mays");

            Assert.Equal(patient.Name, patientActual.Name);
            Assert.Equal(patient.SSN, patientActual.Ssn);
            Assert.Equal(patient.InsuranceProvider, patientActual.Insurance);
        }
        [Fact]
        public void GetDoctors_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var doctors = repo.GetDoctors();
            var doctorsActual = context.Doctors.ToList();

            foreach (var doctor in doctors)
            {
                Assert.Contains(doctor.Name, doctorsActual.Select(x => x.Name));
                Assert.Contains(doctor.Id, doctorsActual.Select(x => x.Id));
                Assert.Contains(doctor.Title, doctorsActual.Select(x => x.Title));
            }
        }
        [Fact]
        public async void GetDoctorsAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var doctors = await repo.GetDoctorsAsync();
            var doctorsActual = context.Doctors.ToList();

            foreach (var doctor in doctors)
            {
                Assert.Contains(doctor.Name, doctorsActual.Select(x => x.Name));
                Assert.Contains(doctor.Id, doctorsActual.Select(x => x.Id));
                Assert.Contains(doctor.Title, doctorsActual.Select(x => x.Title));
            }
        }
        [Fact]
        public void GetPatients_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patients = repo.GetPatients();
            var patientsActual = context.Patients.ToList();

            foreach (var patient in patients)
            {
                Assert.Contains(patient.Name, patientsActual.Select(x => x.Name));
                Assert.Contains(patient.Id, patientsActual.Select(x => x.Id));
                Assert.Contains(patient.SSN, patientsActual.Select(x => x.Ssn));
                Assert.Contains(patient.InsuranceProvider, patientsActual.Select(x => x.Insurance));
                Assert.Contains(patient.DateOfBirth, patientsActual.Select(x => x.Dob));
            }
        }
        [Fact]
        public async void GetPatientsAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patients = await repo.GetPatientsAsync();
            var patientsActual = context.Patients.ToList();

            foreach (var patient in patients)
            {
                Assert.Contains(patient.Name, patientsActual.Select(x => x.Name));
                Assert.Contains(patient.Id, patientsActual.Select(x => x.Id));
                Assert.Contains(patient.SSN, patientsActual.Select(x => x.Ssn));
                Assert.Contains(patient.InsuranceProvider, patientsActual.Select(x => x.Insurance));
                Assert.Contains(patient.DateOfBirth, patientsActual.Select(x => x.Dob));
            }
        }
        [Fact]
        public void GetPatientbyID_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patient = repo.GetPatientByID(2);

            var patientsActual = context.Patients.Where(x => x.Id == 2).Single();

            Assert.Equal(patient.Id, patientsActual.Id);
            Assert.Equal(patient.Name, patientsActual.Name);
            Assert.Equal(patient.SSN, patientsActual.Ssn);
        }
        [Fact]
        public async void GetPatientbyIDAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patient = await repo.GetPatientByIDAsync(2);

            var patientsActual = context.Patients.Where(x => x.Id == 2).Single();

            Assert.Equal(patient.Id, patientsActual.Id);
            Assert.Equal(patient.Name, patientsActual.Name);
            Assert.Equal(patient.SSN, patientsActual.Ssn);
        }
        [Fact]
        public void GetDoctorbyID_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var doctor = repo.GetDoctorByID(2);

            var doctorActual = context.Doctors.Where(x => x.Id == 2).Single();

            Assert.Equal(doctor.Id, doctorActual.Id);
            Assert.Equal(doctor.Name, doctorActual.Name);
        }
        [Fact]
        public async void GetDoctorbyIDAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var doctor = await repo.GetDoctorByIDAsync(2);

            var doctorActual = context.Doctors.Where(x => x.Id == 2).Single();

            Assert.Equal(doctor.Id, doctorActual.Id);
            Assert.Equal(doctor.Name, doctorActual.Name);
            Assert.Equal(doctor.Title, doctorActual.Title);
        }
        [Fact]
        public void GetDoctorPatients_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patients = repo.GetDoctorPatients(3);

            var patientsActual = context.Patients.Where(x => x.DoctorId == 3).ToList();

            foreach (var patient in patients)
            {
                Assert.Contains(patient.Name, patientsActual.Select(x => x.Name));
                Assert.Contains(patient.Id, patientsActual.Select(x => x.Id));
                Assert.Contains(patient.SSN, patientsActual.Select(x => x.Ssn));
                Assert.Contains(patient.InsuranceProvider, patientsActual.Select(x => x.Insurance));
                Assert.Contains(patient.DateOfBirth, patientsActual.Select(x => x.Dob));
                Assert.Contains(patient.PrimaryDoctor.Id, patientsActual.Select(x => x.DoctorId));
            }
        }
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
                new DataModel.Patient {Name = "Jerry Smith", Dob = new DateTime(1977, 5, 13), DoctorId = 1, Insurance = "Medicare", Ssn = "230-98-9882" },
                new DataModel.Patient {Name = "Bird Person", Dob = new DateTime(1991, 2, 21), DoctorId = 2, Insurance = "Insuricare", Ssn = "273-88-3476" },
                new DataModel.Patient {Name = "Jotaro Kujo", Dob = new DateTime(1988, 5, 1), DoctorId = 1, Insurance = "Real Health", Ssn = "097-98-1127" },
                new DataModel.Patient {Name = "Walter White", Dob = new DateTime(1956, 3, 20), DoctorId = 3, Insurance = "Meth Health", Ssn = "435-88-5634" },
                new DataModel.Patient {Name = "Garfield", Dob = new DateTime(2001, 9, 30), DoctorId = 3, Insurance = "NixonCare", Ssn = "346-37-4858" },
                new DataModel.Patient {Name = "Johnny Silverhand", Dob = new DateTime(1990, 3, 2), DoctorId = 4, Insurance = "Insuricare", Ssn = "358-01-9678" },
                new DataModel.Patient {Name = "Richard Nixon", Dob = new DateTime(1999, 8, 17), DoctorId = 4, Insurance = "Medicare", Ssn = "426-33-1348" },
                new DataModel.Patient {Name = "Beth Smith", Dob = new DateTime(1976, 7, 21), DoctorId = 4, Insurance = "Medicare", Ssn = "456-67-8523" }
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
            context.SaveChanges();

            return connection;
        }
    }
}
