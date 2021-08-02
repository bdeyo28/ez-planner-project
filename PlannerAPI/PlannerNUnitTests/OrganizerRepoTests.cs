using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PlannerAPI.Persistence;
using PlannerAPI.Persistence.Repos;
using Microsoft.Extensions.Configuration;
using PlannerAPI.Models.Domain;
using PlannerAPI.Models.Auth;
using System.Collections.Generic;

namespace PlannerNUnitTests
{
    public class OrganizerRepoTests
    {
        EFOrganizerRepo _organizerRepo;
        EFEventRepo _eventRepo;

        Organizer _onlyOrganizer;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<PlannerDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            PlannerDbContext newContext = new PlannerDbContext(builder.Options);

            _organizerRepo = new EFOrganizerRepo(newContext);
            _eventRepo = new EFEventRepo(newContext);

            newContext.Events.RemoveRange(newContext.Events);
            newContext.Organizers.RemoveRange(newContext.Organizers);

            _onlyOrganizer = new Organizer
            {
                Name = "Brendan",
                Email = "bdeyo@gmail.com"
            };

            newContext.Organizers.Add(_onlyOrganizer);

            newContext.SaveChanges();
        }

        [Test]
        public void TestAddOrganizer()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer toAdd = new Organizer
            {
                Name = "New Organizer",
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            _organizerRepo.AddOrganizer(toAdd);

            List<Organizer> testOrganizers = _organizerRepo.GetAllOrganizers();

            Assert.AreEqual(2, testOrganizers.Count);

            Assert.AreEqual("New Organizer", testOrganizers[1].Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizers[1].Email);
        }

        [Test]
        public void TestEditOrganizer()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer toAdd = new Organizer
            {
                Name = "New Organizer",
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            _organizerRepo.AddOrganizer(toAdd);

            List<Organizer> testOrganizers = _organizerRepo.GetAllOrganizers();

            Assert.AreEqual(2, testOrganizers.Count);

            Assert.AreEqual("New Organizer", testOrganizers[1].Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizers[1].Email);

            toAdd.Name = "EDITED";
            toAdd.Email = "edited@gmail.com";

            _organizerRepo.EditOrganizer(toAdd);

            testOrganizers = _organizerRepo.GetAllOrganizers();

            Assert.AreEqual(2, testOrganizers.Count);

            Assert.AreEqual("EDITED", testOrganizers[1].Name);
            Assert.AreEqual("edited@gmail.com", testOrganizers[1].Email);
        }

        [Test]
        public void TestGetOrganizerById()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer toAdd = new Organizer
            {
                Name = "New Organizer",
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            _organizerRepo.AddOrganizer(toAdd);

            List<Organizer> testOrganizers = _organizerRepo.GetAllOrganizers();

            Assert.AreEqual(2, testOrganizers.Count);

            Assert.AreEqual("New Organizer", testOrganizers[1].Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizers[1].Email);

            Organizer testOrganizer = _organizerRepo.GetOrganizerById(toAdd.Id);

            Assert.AreEqual("New Organizer", testOrganizer.Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizer.Email);
        }

        [Test]
        public void TestGetOrganizerByName()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer toAdd = new Organizer
            {
                Name = "New Organizer",
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            _organizerRepo.AddOrganizer(toAdd);

            List<Organizer> testOrganizers = _organizerRepo.GetAllOrganizers();

            Assert.AreEqual(2, testOrganizers.Count);

            Assert.AreEqual("New Organizer", testOrganizers[1].Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizers[1].Email);

            Organizer testOrganizer = _organizerRepo.GetOrganizerByName(toAdd.Name);

            Assert.AreEqual("New Organizer", testOrganizer.Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizer.Email);

        }

        [Test]
        public void TestRemoveOrganizer()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer toAdd = new Organizer
            {
                Name = "New Organizer",
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            _organizerRepo.AddOrganizer(toAdd);

            List<Organizer> testOrganizers = _organizerRepo.GetAllOrganizers();

            Assert.AreEqual(2, testOrganizers.Count);

            Assert.AreEqual("New Organizer", testOrganizers[1].Name);
            Assert.AreEqual("organizer@gmail.com", testOrganizers[1].Email);

            _organizerRepo.RemoveOrganizer(toAdd);

            Assert.AreEqual(1, _organizerRepo.GetAllOrganizers().Count);
        }

        [Test]
        public void TestAddInvalidOrganizer()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer invalidAdd = new Organizer
            {
                Name = null,
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            Assert.Throws<DbUpdateException>(() => _organizerRepo.AddOrganizer(invalidAdd));
        }

        [Test]
        public void TestEditInvalidOrganizer()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            Organizer invalidEdit = new Organizer
            {
                Name = "Not Invalid",
                Email = "organizer@gmail.com",
                Roles = roles,
                OrganizedEvents = null
            };

            _organizerRepo.AddOrganizer(invalidEdit);

            Assert.That(_organizerRepo.GetAllOrganizers() != null);

            invalidEdit.Name = null;

            Assert.Throws<DbUpdateException>(() => _organizerRepo.EditOrganizer(invalidEdit));
        }

        [Test]
        public void TestGetOrganizerByIdFail()
        {
            Assert.That(_organizerRepo.GetOrganizerById(1000) == null);
        }

        [Test]
        public void TestGetOrganizerByNameFail()
        {
            Assert.That(_organizerRepo.GetOrganizerByName("Howard Stern") == null);
        }

        [Test]

        public void TestGetOrganizerByNullName()
        {
            Assert.Throws<System.InvalidOperationException>(() => _organizerRepo.GetOrganizerByName(null));
        }

        [Test]
        public void TestRemoveInvalidOrganizer()
        {
            Organizer toDelete = null;

            Assert.Throws<System.ArgumentNullException>(() => _organizerRepo.RemoveOrganizer(toDelete));
        }

        [Test]
        public void TestRemoveNonExistentOrganizer()
        {
            Organizer toRemove = new Organizer();

            Assert.Throws<System.InvalidOperationException>(() => _organizerRepo.RemoveOrganizer(toRemove));
        }
    }
}
