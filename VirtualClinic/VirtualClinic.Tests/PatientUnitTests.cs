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
        public async Task AddPatientAsync_Database_Test()
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
        public async Task GetPatientsAsync_Database_test()
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
        public async Task GetPatientbyIDAsync_Database_test()
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
        [Fact]
        public async Task GetDoctorPatientsAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patients = await repo.GetDoctorPatientsAsync(3);

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

    }
}
