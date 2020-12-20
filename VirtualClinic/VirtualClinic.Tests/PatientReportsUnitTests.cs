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
        public void GetPatientReports_Database_test1(int id)
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
        public void GetPatientReports_Database_test2(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            Assert.Throws<ArgumentException>(() => repo.GetPatientReportByID(id));

        }
    }
}
