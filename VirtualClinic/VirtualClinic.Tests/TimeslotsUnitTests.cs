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
        [Fact]
        public void AddTimeslot_Database_test1()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslot = new Domain.Models.Timeslot(new DateTime(2020, 12, 20, 8, 0, 0))
            {
                
                Appointment = new Domain.Models.Appointment(5, "idiot can't find his stethoscope")
                {
                    PatientId = 7, DoctorId = 4
                },
                Id = 9,
                DoctorId = 4
            };

            repo.AddTimeslot(timeslot);

            var timeslotActual = context.Timeslots.Find(9);

            Assert.Equal(timeslotActual.Id, timeslot.Id);
            Assert.Equal(timeslotActual.DoctorId, timeslot.DoctorId);
            Assert.Equal(timeslotActual.AppointmentId, timeslot.Appointment?.Id);
            Assert.Equal(timeslotActual.Start, timeslot.Start);
            Assert.Equal(timeslotActual.End, timeslot.End);
        }
        [Fact]
        public void AddTimeslot_Database_test2()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslot = new Domain.Models.Timeslot(new DateTime(2020, 12, 20, 8, 0, 0))
            {
                Id = 9,
                DoctorId = 4
            };

            repo.AddTimeslot(timeslot);

            var timeslotActual = context.Timeslots.Find(9);

            Assert.Equal(timeslotActual.Id, timeslot.Id);
            Assert.Equal(timeslotActual.DoctorId, timeslot.DoctorId);
            Assert.Equal(timeslotActual.Start, timeslot.Start);
            Assert.Equal(timeslotActual.End, timeslot.End);
            Assert.Null(timeslotActual.Appointment);

        }
        [Fact]
        public async Task AddTimeslotAsync_Database_test1()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslot = new Domain.Models.Timeslot(new DateTime(2020, 12, 20, 8, 0, 0))
            {

                Appointment = new Domain.Models.Appointment(5, "idiot can't find his stethoscope")
                {
                    PatientId = 7,
                    DoctorId = 4
                },
                Id = 9,
                DoctorId = 4
            };

            await repo.AddTimeslotAsync(timeslot);

            var timeslotActual = context.Timeslots.Find(9);

            Assert.Equal(timeslotActual.Id, timeslot.Id);
            Assert.Equal(timeslotActual.DoctorId, timeslot.DoctorId);
            Assert.Equal(timeslotActual.AppointmentId, timeslot.Appointment?.Id);
            Assert.Equal(timeslotActual.Start, timeslot.Start);
            Assert.Equal(timeslotActual.End, timeslot.End);
        }
        [Fact]
        public async Task AddTimeslotAsync_Database_test2()
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var timeslot = new Domain.Models.Timeslot(new DateTime(2020, 12, 20, 8, 0, 0))
            {
                Id = 9,
                DoctorId = 4
            };

            await repo.AddTimeslotAsync(timeslot);

            var timeslotActual = context.Timeslots.Find(9);

            Assert.Equal(timeslotActual.Id, timeslot.Id);
            Assert.Equal(timeslotActual.DoctorId, timeslot.DoctorId);
            Assert.Equal(timeslotActual.Start, timeslot.Start);
            Assert.Equal(timeslotActual.End, timeslot.End);
            Assert.Null(timeslotActual.Appointment);
        }
    }
}
