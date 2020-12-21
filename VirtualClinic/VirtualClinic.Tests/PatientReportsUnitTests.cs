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
        public void GetPatientReportsByID_Database_test1(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patientReport = repo.GetPatientReportByID(id);

            var patientReportActual = context.PatientReports.Find(id);
            var vitalsReport = context.Vitals.Find(patientReportActual.VitalsId);


            Assert.Equal(patientReport.Id, patientReportActual.Id);
            Assert.Equal(patientReport.Info, patientReportActual.Information);
            Assert.Equal(patientReport.Time, patientReportActual.ReportTime);
            Assert.Equal(patientReport.Vitals?.Id, vitalsReport?.Id);
            Assert.Equal(patientReport.Vitals?.Systolic, vitalsReport?.Systolic);
            Assert.Equal(patientReport.Vitals?.Diastolic, vitalsReport?.Diastolic);
            Assert.Equal(patientReport.Vitals?.HeartRate, vitalsReport?.HeartRate);
            Assert.Equal(patientReport.Vitals?.PainLevel, vitalsReport?.Pain);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        public void GetPatientReportsByID_Database_test2(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            Assert.Throws<ArgumentException>(() => repo.GetPatientReportByID(id));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetPatientReportsByIDAsync_Database_test1(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patientReport = await repo.GetPatientReportByIDAsync(id);

            var patientReportActual = context.PatientReports.Find(id);
            var vitalsReport = context.Vitals.Find(patientReportActual.VitalsId);


            Assert.Equal(patientReport.Id, patientReportActual.Id);
            Assert.Equal(patientReport.Info, patientReportActual.Information);
            Assert.Equal(patientReport.Time, patientReportActual.ReportTime);
            Assert.Equal(patientReport.Vitals?.Id, vitalsReport?.Id);
            Assert.Equal(patientReport.Vitals?.Systolic, vitalsReport?.Systolic);
            Assert.Equal(patientReport.Vitals?.Diastolic, vitalsReport?.Diastolic);
            Assert.Equal(patientReport.Vitals?.HeartRate, vitalsReport?.HeartRate);
            Assert.Equal(patientReport.Vitals?.PainLevel, vitalsReport?.Pain);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        public async Task GetPatientReportsByIDAsync_Database_test2(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            await Assert.ThrowsAsync<ArgumentException>(() => repo.GetPatientReportByIDAsync(id));
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
        public void GetPatientReports_Database_test1(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patientReport = repo.GetPatientReports(id);

            var patientReportActual = context.PatientReports.Where(x => x.PatientId == id);
            var vitalsActual = context.Vitals.Where(x => patientReportActual.Select(y => y.VitalsId).Contains(x.Id));

            foreach (var report in patientReport)
            {
                Assert.Contains(report.Id, patientReportActual.Select(x => x.Id));
                if (report.Vitals is not null)
                {
                    Assert.Contains(report.Vitals.Id, vitalsActual.Select(x => x.Id));
                    Assert.Contains(report.Vitals.Systolic, vitalsActual.Select(x => x.Systolic));
                    Assert.Contains(report.Vitals.Diastolic, vitalsActual.Select(x => x.Diastolic));
                    Assert.Contains(report.Vitals.HeartRate, vitalsActual.Select(x => x.HeartRate));
                    Assert.Contains(report.Vitals.PainLevel, vitalsActual.Select(x => x.Pain));
                }
                Assert.Contains(report.Info, patientReportActual.Select(x => x.Information));
                Assert.Contains(report.Time, patientReportActual.Select(x => x.ReportTime));
                Assert.Contains(report.PatientId, patientReportActual.Select(x => x.PatientId));
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
        public async Task GetPatientReportsAsync_Database_test1(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var patientReport = await repo.GetPatientReportsAsync(id);

            var patientReportActual = context.PatientReports.Where(x => x.PatientId == id);
            var vitalsActual = context.Vitals.Where(x => patientReportActual.Select(y => y.VitalsId).Contains(x.Id));

            foreach (var report in patientReport)
            {
                Assert.Contains(report.Id, patientReportActual.Select(x => x.Id));
                if (report.Vitals is not null)
                {
                    Assert.Contains(report.Vitals.Id, vitalsActual.Select(x => x.Id));
                    Assert.Contains(report.Vitals.Systolic, vitalsActual.Select(x => x.Systolic));
                    Assert.Contains(report.Vitals.Diastolic, vitalsActual.Select(x => x.Diastolic));
                    Assert.Contains(report.Vitals.HeartRate, vitalsActual.Select(x => x.HeartRate));
                    Assert.Contains(report.Vitals.PainLevel, vitalsActual.Select(x => x.Pain));
                }
                Assert.Contains(report.Info, patientReportActual.Select(x => x.Information));
                Assert.Contains(report.Time, patientReportActual.Select(x => x.ReportTime));
                Assert.Contains(report.PatientId, patientReportActual.Select(x => x.PatientId));
            }
        }

        [Fact]
        public void AddPatientReport_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var report = new Domain.Models.PatientReport(patientId: 7, info: "I have a stomach ache", time: DateTime.Now);

            repo.AddPatientReport(report);

            var reportActual = context.PatientReports.Find(7);

            Assert.Equal(report.PatientId, reportActual.PatientId);
            Assert.Equal(report.Info, reportActual.Information);
            Assert.Equal(report.Time, reportActual.ReportTime);
        }
        [Fact]
        public async Task AddPatientReportAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var report = new Domain.Models.PatientReport(patientId: 7, info: "I have a stomach ache", time: DateTime.Now);

            await repo.AddPatientReportAsync(report);

            var reportActual = context.PatientReports.Find(7);

            Assert.Equal(report.PatientId, reportActual.PatientId);
            Assert.Equal(report.Info, reportActual.Information);
            Assert.Equal(report.Time, reportActual.ReportTime);
        }
    }
}
