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
    public class AttendeeRepoTests
    {
        EFAttendeeRepo _attendeeRepo;
        EFEventRepo _eventRepo;

        Organizer _onlyOrganizer;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<PlannerDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            PlannerDbContext newContext = new PlannerDbContext(builder.Options);

            _attendeeRepo = new EFAttendeeRepo(newContext);
            _eventRepo = new EFEventRepo(newContext);

            newContext.Attendees.RemoveRange(newContext.Attendees);
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
        public void TestAddAttendee()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee1",
                Email = "attendee1@gmail.com"
            };

            Attendee attendee2 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee2",
                Email = "attendee2@gmail.com"
            };

            _attendeeRepo.AddAttendee(attendee1);
            _attendeeRepo.AddAttendee(attendee2);

            List<Attendee> testAttendees = _attendeeRepo.GetAllAttendees();

            Assert.AreEqual(2, testAttendees.Count);

            Assert.AreEqual("Attendee1", testAttendees[0].Name);
            Assert.AreEqual("attendee1@gmail.com", testAttendees[0].Email);
            Assert.AreEqual("Attendee2", testAttendees[1].Name);
            Assert.AreEqual("attendee2@gmail.com", testAttendees[1].Email);
        }

        [Test]
        public void TestEditAttendee()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee1",
                Email = "attendee1@gmail.com"
            };

            _attendeeRepo.AddAttendee(attendee1);

            List<Attendee> testAttendees = _attendeeRepo.GetAllAttendees();

            Assert.AreEqual("Attendee1", testAttendees[0].Name);
            Assert.AreEqual("attendee1@gmail.com", testAttendees[0].Email);

            attendee1.Email = "updatedattendee1@gmail.com";
            attendee1.Name = "EDITED NAME";

            _attendeeRepo.EditAttendee(attendee1);

            testAttendees = _attendeeRepo.GetAllAttendees();

            Assert.AreEqual("EDITED NAME", testAttendees[0].Name);
            Assert.AreEqual("updatedattendee1@gmail.com", testAttendees[0].Email);
        }

        [Test]
        public void TestGetAttendeeById()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee1",
                Email = "attendee1@gmail.com"
            };

            _attendeeRepo.AddAttendee(attendee1);

            Attendee toTest = _attendeeRepo.GetAttendeeById(attendee1.Id);

            Assert.AreEqual("Attendee1", toTest.Name);
            Assert.AreEqual("attendee1@gmail.com", toTest.Email);
        }

        [Test]
        public void TestRemoveAttendee()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee1",
                Email = "attendee1@gmail.com"
            };

            Attendee attendee2 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee2",
                Email = "attendee2@gmail.com"
            };

            _attendeeRepo.AddAttendee(attendee1);
            _attendeeRepo.AddAttendee(attendee2);

            List<Attendee> testAttendees = _attendeeRepo.GetAllAttendees();

            Assert.AreEqual(2, testAttendees.Count);

            _attendeeRepo.RemoveAttendee(attendee2);

            testAttendees = _attendeeRepo.GetAllAttendees();

            Assert.AreEqual(1, testAttendees.Count);
        }

        [Test]
        public void TestAddInvalidAttendeeToContext()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = null,
                Email = "attendee1@gmail.com"
            };

            Assert.Throws<DbUpdateException>(() => _attendeeRepo.AddAttendee(attendee1));
        }

        [Test]

        public void TestEditInvalidAttendeeToContext()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Attendee 1",
                Email = "attendee1@gmail.com"
            };

            attendee1.Email = null;

            Assert.Throws<System.InvalidOperationException>(() => _attendeeRepo.EditAttendee(attendee1));
        }

        [Test]
        public void TestRemoveNullAttendeeFromContext()
        {
            Attendee toDelete = null;

            Assert.Throws<System.ArgumentNullException>(() => _attendeeRepo.RemoveAttendee(toDelete));
        }

        [Test]
        public void TestGetAttendeeByInvalidId()
        {
            Assert.That(_attendeeRepo.GetAttendeeById(100000) == null);
        }

        [Test]
        public void AssertThrowsForeignKeyException()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = 1000,
                Name = "Attendee 1",
                Email = "attendee1@gmail.com"
            };


            Assert.Throws<DbUpdateException>(() => _attendeeRepo.AddAttendee(attendee1));
        }

        [Test]
        public void AssertThrowsForeignKeyExceptionEditAttendee()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd);

            Attendee attendee1 = new Attendee
            {
                EventId = toAdd.Id,
                Name = "Brendan",
                Email = "this@gmail.com"
            };

            _attendeeRepo.AddAttendee(attendee1);

            Attendee thisAttendee = _attendeeRepo.GetAttendeeById(attendee1.Id);

            Assert.IsNotNull(thisAttendee);

            thisAttendee.EventId = 1000;

            Assert.Throws<DbUpdateException>(() => _attendeeRepo.EditAttendee(thisAttendee));

        }

        [Test]

        public void TestRemoveNonExistentAttendee()
        {
            Attendee doesNotExistInContext = new Attendee();

            Assert.Throws<System.InvalidOperationException>(() => _attendeeRepo.RemoveAttendee(doesNotExistInContext));
        }
    }
}