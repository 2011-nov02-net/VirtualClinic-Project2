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
        public void GetPatientTimeslots_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslots = repo.GetPatientTimeslots(1);

            var timeslotsActual = context.Timeslots
                .Include(x => x.Appointment)
                .Where(x => x.Appointment.PatientId == 1).ToList();

            foreach (var timeslot in timeslots)
            {
                Assert.Contains(timeslot.Id, timeslotsActual.Select(x => x.Id));
                Assert.Contains(timeslot.Appointment.Notes, timeslotsActual.Select(x => x.Appointment.Notes));
                Assert.NotNull(timeslot.Appointment);
            }
        }
        [Fact]
        public async Task GetPatientTimeslotsAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslots = await repo.GetPatientTimeslotsAsync(1);

            var timeslotsActual = context.Timeslots
                .Include(x => x.Appointment)
                .Where(x => x.Appointment.PatientId == 1).ToList();

            foreach (var timeslot in timeslots)
            {
                Assert.Contains(timeslot.Id, timeslotsActual.Select(x => x.Id));
                Assert.Contains(timeslot.Appointment.Notes, timeslotsActual.Select(x => x.Appointment.Notes));
                Assert.NotNull(timeslot.Appointment);
            }
        }
        [Fact]
        public void GetDoctorTimeslots_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslots = repo.GetDoctorTimeslots(1);

            var timeslotsActual = context.Timeslots
                .Where(x => x.DoctorId == 1).ToList();

            foreach (var timeslot in timeslots)
            {
                Assert.Contains(timeslot.Id, timeslotsActual.Select(x => x.Id));
                Assert.Contains(timeslot.Start, timeslotsActual.Select(x => x.Start));
                Assert.Contains(timeslot.End, timeslotsActual.Select(x => x.End));
            }
        }
        [Fact]
        public async Task GetDoctorTimeslotsAsync_Database_test()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslots = await repo.GetDoctorTimeslotsAsync(1);

            var timeslotsActual = context.Timeslots
                .Where(x => x.DoctorId == 1).ToList();

            foreach (var timeslot in timeslots)
            {
                Assert.Contains(timeslot.Id, timeslotsActual.Select(x => x.Id));
                Assert.Contains(timeslot.Start, timeslotsActual.Select(x => x.Start));
                Assert.Contains(timeslot.End, timeslotsActual.Select(x => x.End));
            }
        }
    }
}
