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
    public class EventRepoTests
    {
        EFEventRepo _eventRepo;
        EFAttendeeRepo _attendeeRepo;
        EFOrganizerRepo _organizerRepo;
        EFActivityRepo _activityRepo;

        IServiceCollection _services = new ServiceCollection();

        Organizer _onlyOrganizer;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<PlannerDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            PlannerDbContext newContext = new PlannerDbContext(builder.Options);

            _eventRepo = new EFEventRepo(newContext);
            _activityRepo = new EFActivityRepo(newContext);
            _attendeeRepo = new EFAttendeeRepo(newContext);
            _organizerRepo = new EFOrganizerRepo(newContext);

            newContext.Events.RemoveRange(newContext.Events);

            newContext.Organizers.RemoveRange(newContext.Organizers);

            _onlyOrganizer = new Organizer
            {
                Name = "Brendan",
                Email = "bdeyo@gmail.com"
            };

            newContext.Organizers.Add(_onlyOrganizer);
            newContext.SaveChanges();

            //_services.AddDbContext<PlannerDbContext>(options => options.UseSqlServer(config.GetConnectionString("TestDb")));
            //_services.AddScoped<IEventDao, EFEventRepo>();
        }

        [Test]
        public void TestAddEvent()
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

            List<Event> testAllEvents = _eventRepo.GetAllEvents();

            Assert.AreEqual(1, testAllEvents.Count);
            Assert.AreEqual("Maryland", testAllEvents[0].Location);
            Assert.AreEqual("Work", testAllEvents[0].Category);
            Assert.AreEqual("TEST EVENT", testAllEvents[0].EventName);
            Assert.AreEqual("3 PM EST", testAllEvents[0].Time);
            Assert.AreEqual(new System.DateTime(21, 07, 21), testAllEvents[0].Date);
            Assert.AreEqual(10, testAllEvents[0].Duration);
            Assert.AreEqual(_onlyOrganizer.Id, testAllEvents[0].OrganizerId);
        }

        [Test]
        public void TestEditEvent()
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

            List<Event> testAllEvents = _eventRepo.GetAllEvents();

            Event updated = toAdd;
            updated.Location = "Virginia";
            updated.Time = "5 PM EST";
            updated.EventName = "TEST EDITED EVENT";

            _eventRepo.EditEvent(updated);

            Assert.AreEqual(1, testAllEvents.Count);
            Assert.AreEqual("5 PM EST", testAllEvents[0].Time);
            Assert.AreEqual("TEST EDITED EVENT", testAllEvents[0].EventName);
            Assert.AreEqual("Virginia", testAllEvents[0].Location);

        }

        [Test]
        public void TestGetEventById()
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

            Event testSearchById = _eventRepo.GetEventById(toAdd.Id);

            Assert.AreEqual("Maryland", testSearchById.Location);
            Assert.AreEqual("Work", testSearchById.Category);
            Assert.AreEqual("TEST EVENT", testSearchById.EventName);
            Assert.AreEqual("3 PM EST", testSearchById.Time);
            Assert.AreEqual(new System.DateTime(21, 07, 21), testSearchById.Date);
            Assert.AreEqual(10, testSearchById.Duration);
            Assert.AreEqual(_onlyOrganizer.Id, testSearchById.OrganizerId);
        }

        [Test]
        public void TestGetEventByName()
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

            Event testSearchByName = _eventRepo.GetEventByName(toAdd.EventName);

            Assert.AreEqual("Maryland", testSearchByName.Location);
            Assert.AreEqual("Work", testSearchByName.Category);
            Assert.AreEqual("TEST EVENT", testSearchByName.EventName);
            Assert.AreEqual("3 PM EST", testSearchByName.Time);
            Assert.AreEqual(new System.DateTime(21, 07, 21), testSearchByName.Date);
            Assert.AreEqual(10, testSearchByName.Duration);
            Assert.AreEqual(_onlyOrganizer.Id, testSearchByName.OrganizerId);
        }

        [Test]
        public void TestDeleteEvent()
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

            List<Event> testAllEvents = _eventRepo.GetAllEvents();

            Assert.AreEqual(1, testAllEvents.Count);

            _eventRepo.RemoveEvent(toAdd);

            testAllEvents = _eventRepo.GetAllEvents();

            Assert.AreEqual(0, testAllEvents.Count);
        }

        [Test]
        public void TestEditInvalidEventObjectToContext()
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

            Assert.That(_eventRepo.GetAllEvents() != null);

            Event updated = toAdd;
            updated.EventName = null;

            Assert.Throws<System.InvalidOperationException>(() => _eventRepo.EditEvent(updated));

        }

        [Test]
        public void TestInvalidAddEvent()
        {
            Event toAdd = new Event
            {
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            Assert.Throws<DbUpdateException>(() => _eventRepo.AddEvent(toAdd));
        }

        [Test]
        public void TestDeleteEventWithInvalidId()
        {
            Event toDelete = null;

            Assert.Throws<System.ArgumentNullException>(() => _eventRepo.RemoveEvent(toDelete));
        }

        [Test]
        public void TestGetEventActivities()
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

            Activity activity1 = new Activity
            {
                EventId = toAdd.Id,
                Duration = 10,
                Name = "Activity 1"
            };

            _activityRepo.AddActivity(activity1);

            Activity activity2 = new Activity
            {
                EventId = toAdd.Id,
                Duration = 10,
                Name = "Activity 2"
            };

            _activityRepo.AddActivity(activity2);

            List<Activity> testGetActivities = _eventRepo.GetEventActivities(toAdd.Id);

            Assert.AreEqual(2, testGetActivities.Count);
            Assert.AreEqual("Activity 1", testGetActivities[0].Name);
            Assert.AreEqual(10, testGetActivities[0].Duration);
            Assert.AreEqual("Activity 2", testGetActivities[1].Name);
            Assert.AreEqual(10, testGetActivities[1].Duration);
        }

        [Test]

        public void TestGetEventActivitiesCountZero()
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

            List<Activity> testActivityList = _eventRepo.GetEventActivities(toAdd.Id);

            Assert.AreEqual(0, testActivityList.Count);
        }

        [Test]
        public void AssertThrowsNullExceptionInvalidEventIdForActivities()
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

            List<Activity> toTest = _eventRepo.GetEventActivities(1001);

            Assert.IsEmpty(toTest);

        }

        [Test]
        public void AssertThrowsNullExceptionInvalidEventIdForAttendees()
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

            Assert.IsEmpty(_eventRepo.GetEventAttendees(1001));

        }

        public void TestGetEventAttendeesCountZero()
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

            List<Attendee> testAttendeeList = _eventRepo.GetEventAttendees(toAdd.Id);

            Assert.AreEqual(0, testAttendeeList.Count);
        }

        [Test]
        public void TestGetEventAttendees()
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
                Email = "attendee1@gmail.com",
                Name = "Attendee 1"
            };

            _attendeeRepo.AddAttendee(attendee1);

            Attendee attendee2 = new Attendee
            {
                EventId = toAdd.Id,
                Email = "attendee2@gmail.com",
                Name = "Attendee 2"
            };

            _attendeeRepo.AddAttendee(attendee2);

            List<Attendee> testGetAttendees = _eventRepo.GetEventAttendees(toAdd.Id);

            Assert.AreEqual(2, testGetAttendees.Count);

            Assert.AreEqual("Attendee 1", testGetAttendees[0].Name);
            Assert.AreEqual("attendee1@gmail.com", testGetAttendees[0].Email);
            Assert.AreEqual("Attendee 2", testGetAttendees[1].Name);
            Assert.AreEqual("attendee2@gmail.com", testGetAttendees[1].Email);
        }

        [Test]
        public void TestGetEventOrganizer()
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

            Organizer toTest = _eventRepo.GetEventOrganizer(toAdd.Id);

            Assert.AreEqual("Brendan", toTest.Name);
            Assert.AreEqual("bdeyo@gmail.com", toTest.Email);
        }

        [Test]
        public void TestGetEventsByOrganizerId()
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

            Event toAdd2 = new Event
            {
                EventName = "TEST EVENT 2",
                Location = "Maryland 2",
                Time = "5 PM EST",
                OrganizerId = _onlyOrganizer.Id,
                Category = "Family",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _eventRepo.AddEvent(toAdd2);

            List<Event> testEvents = _eventRepo.GetEventsByOrganizerId(_onlyOrganizer.Id);

            Assert.AreEqual(2, testEvents.Count);

            Assert.AreEqual("Maryland", testEvents[0].Location);
            Assert.AreEqual("Work", testEvents[0].Category);
            Assert.AreEqual("TEST EVENT", testEvents[0].EventName);
            Assert.AreEqual("3 PM EST", testEvents[0].Time);
            Assert.AreEqual(new System.DateTime(21, 07, 21), testEvents[0].Date);
            Assert.AreEqual(10, testEvents[0].Duration);
            Assert.AreEqual(_onlyOrganizer.Id, testEvents[0].OrganizerId);

            Assert.AreEqual("Maryland 2", testEvents[1].Location);
            Assert.AreEqual("Family", testEvents[1].Category);
            Assert.AreEqual("TEST EVENT 2", testEvents[1].EventName);
            Assert.AreEqual("5 PM EST", testEvents[1].Time);
            Assert.AreEqual(new System.DateTime(21, 07, 21), testEvents[1].Date);
            Assert.AreEqual(10, testEvents[1].Duration);
            Assert.AreEqual(_onlyOrganizer.Id, testEvents[1].OrganizerId);
        }

        [Test]
        public void TestGetEventWithInvalidId()
        {
            Assert.That(_eventRepo.GetEventById(100000) == null);
        }

        [Test] 
        public void TestGetEventWithInvalidName()
        {
            Assert.That(_eventRepo.GetEventByName("Alfred Hitchcock") == null);
        }

        [Test]
        public void TestGetEventWithNullName()
        {
            Assert.Throws<System.InvalidOperationException>(() => _eventRepo.GetEventByName(null));
        }

        [Test]
        public void AssertThrowsForeignKeyException()
        {
            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = 100,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            Assert.Throws<DbUpdateException>(() => _eventRepo.AddEvent(toAdd));
        }

        [Test]
        public void AssertThrowsForeignKeyExceptionEditEvent()
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

            Event thisEvent = _eventRepo.GetEventById(toAdd.Id);

            Assert.IsNotNull(thisEvent);

            thisEvent.OrganizerId = 1000;

            Assert.Throws<DbUpdateException>(() => _eventRepo.EditEvent(thisEvent));

        }

        [Test]
        public void TestRemoveNonExistentEvent()
        {
            Event doesNotExistInContext = new Event();

            Assert.Throws<System.InvalidOperationException>(() => _eventRepo.RemoveEvent(doesNotExistInContext));
        }
    }
}