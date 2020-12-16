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
    }
}
