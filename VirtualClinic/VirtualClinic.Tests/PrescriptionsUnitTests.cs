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
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void GetPrescription_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var prescription = repo.GetPrescription(id);

            var prescriptionActual = context.Prescriptions.Find(id);

            Assert.Equal(prescription.Id, prescriptionActual.Id);
            Assert.Equal(prescription.Info, prescriptionActual.Information);
            Assert.Equal(prescription.DrugName, prescriptionActual.Drug);
            Assert.Equal(prescription.DoctorId, prescriptionActual.DoctorId);
            Assert.Equal(prescription.PatientId, prescriptionActual.PatientId);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task GetPrescriptionAsync_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var prescription = await repo.GetPrescriptionAsync(id);

            var prescriptionActual = context.Prescriptions.Find(id);

            Assert.Equal(prescription.Id, prescriptionActual.Id);
            Assert.Equal(prescription.Info, prescriptionActual.Information);
            Assert.Equal(prescription.DrugName, prescriptionActual.Drug);
            Assert.Equal(prescription.DoctorId, prescriptionActual.DoctorId);
            Assert.Equal(prescription.PatientId, prescriptionActual.PatientId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void GetPatientPrescriptions_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var prescriptions = repo.GetPatientPrescriptions(id);

            var prescriptionsActual = context.Prescriptions.Where(x => x.PatientId == id).ToList();

            foreach (var prescription in prescriptions)
            {
                Assert.Contains(prescription.Id, prescriptionsActual.Select(x => x.Id));
                Assert.Contains(prescription.DoctorId, prescriptionsActual.Select(x => x.DoctorId));
                Assert.Contains(prescription.PatientId, prescriptionsActual.Select(x => x.PatientId));
                Assert.Contains(prescription.DrugName, prescriptionsActual.Select(x => x.Drug));
                Assert.Contains(prescription.Info, prescriptionsActual.Select(x => x.Information));
            }

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task GetPatientPrescriptionsAsync_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var prescriptions = await repo.GetPatientPrescriptionsAsync(id);

            var prescriptionsActual = context.Prescriptions.Where(x => x.PatientId == id).ToList();

            foreach (var prescription in prescriptions)
            {
                Assert.Contains(prescription.Id, prescriptionsActual.Select(x => x.Id));
                Assert.Contains(prescription.DoctorId, prescriptionsActual.Select(x => x.DoctorId));
                Assert.Contains(prescription.PatientId, prescriptionsActual.Select(x => x.PatientId));
                Assert.Contains(prescription.DrugName, prescriptionsActual.Select(x => x.Drug));
                Assert.Contains(prescription.Info, prescriptionsActual.Select(x => x.Information));
            }

        }

        [Fact]
        public void AddPrescription_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var prescription = new Domain.Models.Prescription("Take every 3 hours", "blarbazin")
            {
                Id = 8,
                PatientId = 1,
                DoctorId = 1
            };

            repo.AddPrescription(prescription);

            var prescriptionActual = context.Prescriptions.Find(8);

            Assert.Equal(prescription.Id, prescriptionActual.Id);
            Assert.Equal(prescription.Info, prescriptionActual.Information);
            Assert.Equal(prescription.DrugName, prescriptionActual.Drug);
            Assert.Equal(prescription.DoctorId, prescriptionActual.DoctorId);
            Assert.Equal(prescription.PatientId, prescriptionActual.PatientId);
        }
        [Fact]
        public async Task AddPrescriptionAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var prescription = new Domain.Models.Prescription("Take every 3 hours", "blarbazin")
            {
                Id = 8,
                PatientId = 1,
                DoctorId = 1
            };

            await repo.AddPrescriptionAsync(prescription);

            var prescriptionActual = context.Prescriptions.Find(8);

            Assert.Equal(prescription.Id, prescriptionActual.Id);
            Assert.Equal(prescription.Info, prescriptionActual.Information);
            Assert.Equal(prescription.DrugName, prescriptionActual.Drug);
            Assert.Equal(prescription.DoctorId, prescriptionActual.DoctorId);
            Assert.Equal(prescription.PatientId, prescriptionActual.PatientId);
        }
    }
}
