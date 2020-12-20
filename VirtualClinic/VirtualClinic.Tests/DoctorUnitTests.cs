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
        public async Task AddDoctorAsync_Database_Test()
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
        public async Task GetDoctorsAsync_Database_test()
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
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetDoctorbyID_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var doctor = repo.GetDoctorByID(id);

            var doctorActual = context.Doctors.Where(x => x.Id == id).Single();

            Assert.Equal(doctor.Id, doctorActual.Id);
            Assert.Equal(doctor.Name, doctorActual.Name);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetDoctorbyIDAsync_Database_test(int id)
        {
            using var connection = Database_init();
            var options = new DbContextOptionsBuilder<ClinicDbContext>().UseSqlite(connection).Options;
            using var context = new ClinicDbContext(options);
            var repo = new ClinicRepository(context, new NullLogger<ClinicRepository>());

            var doctor = await repo.GetDoctorByIDAsync(id);

            var doctorActual = context.Doctors.Where(x => x.Id == id).Single();

            Assert.Equal(doctor.Id, doctorActual.Id);
            Assert.Equal(doctor.Name, doctorActual.Name);
            Assert.Equal(doctor.Title, doctorActual.Title);
        }
    }
}
