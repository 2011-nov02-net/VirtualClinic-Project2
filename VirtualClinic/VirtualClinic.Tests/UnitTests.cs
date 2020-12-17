using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using VirtualClinic.DataModel;
using VirtualClinic.Domain.Repositories;
using VirtualClinic.Domain;
using Xunit;
using System.Linq;

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
        public void AddPatient_Database_Test()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            var patient = new VirtualClinic.Domain.Models.Patient(1, "Jerry Smith", DateTime.Now)
            {
                SSN = "293-38-0071",
                InsuranceProvider = "Umbrella Corp"
            };

            patient.PrimaryDoctor = new Domain.Models.Doctor(100, "John Smith", "diagnostician");

            using (var context = new ClinicDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

                repo.AddPatient(patient);
            }

            using var context2 = new ClinicDbContext(options);
            DataModel.Patient patientActual = context2.Patients
                .Single(l => l.Id == 1);

            Assert.Equal(patient.Name, patientActual.Name);
            Assert.Equal(patient.SSN, patientActual.Ssn);
            Assert.Equal(patient.Id, patientActual.Id);
            Assert.Equal(patient.InsuranceProvider, patientActual.Insurance);
        }

        public void GetPatients_Database_test()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            var patient = new VirtualClinic.Domain.Models.Patient(1, "Jerry Smith", DateTime.Now)
            {
                SSN = "293-38-0071",
                InsuranceProvider = "Umbrella Corp"
            };

            using (var context = new ClinicDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

                repo.AddPatient(patient);
            }

            using var context2 = new ClinicDbContext(options);
            DataModel.Patient patientActual = context2.Patients
                .Single(l => l.Id == 1);

            Assert.Equal(patient.Name, patientActual.Name);
            Assert.Equal(patient.SSN, patientActual.Ssn);
            Assert.Equal(patient.Id, patientActual.Id);
            Assert.Equal(patient.InsuranceProvider, patientActual.Insurance);
        }

        public ClinicDbContext Database_init()
        {
            throw new NotImplementedException();
        }
    }
}
