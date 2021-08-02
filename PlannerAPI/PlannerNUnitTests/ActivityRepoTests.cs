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
    public class ActivityRepoTests
    {
        EFActivityRepo _activityRepo;
        EFEventRepo _eventRepo;

        Organizer _onlyOrganizer;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var builder = new DbContextOptionsBuilder<PlannerDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            PlannerDbContext newContext = new PlannerDbContext(builder.Options);

            _activityRepo = new EFActivityRepo(newContext);
            _eventRepo = new EFEventRepo(newContext);

            newContext.Activities.RemoveRange(newContext.Activities);
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
        public void TestAddActivity()
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
                Name = "Activity 1",
                Duration = 10
            };

            _activityRepo.AddActivity(activity1);

            List<Activity> testActivities = _activityRepo.GetAllActivities();

            Assert.AreEqual(1, testActivities.Count);

            Assert.AreEqual("Activity 1", testActivities[0].Name);
            Assert.AreEqual(10, testActivities[0].Duration);
        }

        [Test]
        public void TestEditActivity()
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
                Name = "Activity 1",
                Duration = 10
            };

            _activityRepo.AddActivity(activity1);

            List<Activity> testActivities = _activityRepo.GetAllActivities();

            Assert.AreEqual(1, testActivities.Count);

            Assert.AreEqual("Activity 1", testActivities[0].Name);
            Assert.AreEqual(10, testActivities[0].Duration);

            activity1.Duration = 7;
            activity1.Name = "EDITED";

            _activityRepo.EditActivity(activity1);

            testActivities = _activityRepo.GetAllActivities();

            Assert.AreEqual(1, testActivities.Count);

            Assert.AreEqual("EDITED", testActivities[0].Name);
            Assert.AreEqual(7, testActivities[0].Duration);
        }

        [Test]
        public void TestRemoveActivityFromContext()
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
                Name = "Activity 1",
                Duration = 10
            };

            _activityRepo.AddActivity(activity1);

            List<Activity> testActivities = _activityRepo.GetAllActivities();

            Assert.AreEqual(1, testActivities.Count);

            _activityRepo.RemoveActivity(activity1);

            testActivities = _activityRepo.GetAllActivities();

            Assert.AreEqual(0, testActivities.Count);
        }

        [Test]
        public void TestGetActivityById()
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
                Name = "Activity 1",
                Duration = 10
            };

            _activityRepo.AddActivity(activity1);

            List<Activity> testActivities = _activityRepo.GetAllActivities();

            Assert.AreEqual(1, testActivities.Count);

            Activity toTest = _activityRepo.GetActivityById(activity1.Id);

            Assert.AreEqual("Activity 1", testActivities[0].Name);
            Assert.AreEqual(10, testActivities[0].Duration);
        }

        [Test]
        public void TestAddInvalidActivityToContext()
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
                Name = null,
                Duration = 10
            };

            Assert.Throws<DbUpdateException>(() => _activityRepo.AddActivity(activity1));
        }

        [Test]

        public void TestEditInvalidActivityToContext()
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
                Name = "Activity 1",
                Duration = 10
            };

            activity1.Name = null;

            Assert.Throws<System.InvalidOperationException>(() => _activityRepo.EditActivity(activity1));
        }

        [Test]
        public void TestRemoveNullActivityFromContext()
        {
            Activity toDelete = null;

            Assert.Throws<System.ArgumentNullException>(() => _activityRepo.RemoveActivity(toDelete));
        }

        [Test]
        public void TestGetActivityByInvalidId()
        {
            Assert.That(_activityRepo.GetActivityById(100000) == null);
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

            Activity activity1 = new Activity
            {
                EventId = 1000,
                Name = "Activity 1",
                Duration = 10
            };

            Assert.Throws<DbUpdateException>(() => _activityRepo.AddActivity(activity1));
        }

        [Test]
        public void AssertThrowsForeignKeyExceptionEditActivity()
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
                Name = "Activity 1",
                Duration = 10
            };

            _activityRepo.AddActivity(activity1);

            Activity thisActivity = _activityRepo.GetActivityById(activity1.Id);

            Assert.IsNotNull(thisActivity);

            thisActivity.EventId = 1000;

            Assert.Throws<DbUpdateException>(() => _activityRepo.EditActivity(thisActivity));

        }

        [Test]
        public void TestRemoveNonExistentActivity()
        {
            Activity doesntExistInContext = new Activity();

            Assert.Throws<System.InvalidOperationException>(() => _activityRepo.RemoveActivity(doesntExistInContext));
        }
    }
}
