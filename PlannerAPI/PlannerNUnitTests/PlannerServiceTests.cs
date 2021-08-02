using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PlannerAPI.Persistence;
using PlannerAPI.Persistence.Repos;
using Microsoft.Extensions.Configuration;
using PlannerAPI.Models.Domain;
using PlannerAPI.Models.Auth;
using System.Collections.Generic;
using PlannerAPI.Services;
using PlannerAPI.Exceptions;

namespace PlannerNUnitTests
{
    public class PlannerServiceTests
    {
        IPlannerService _service;

        ActivityInMemDao _activityDao;
        AttendeeInMemDao _attendeeDao;
        EventInMemDao _eventDao;
        OrganizerInMemDao _organizerDao;

        [SetUp]
        public void Setup()
        {
            _activityDao = new ActivityInMemDao();
            _attendeeDao = new AttendeeInMemDao();
            _eventDao = new EventInMemDao();
            _organizerDao = new OrganizerInMemDao();

            _service = new PlannerService(_eventDao, _activityDao, _attendeeDao, _organizerDao);
        }

        [Test]
        public void TestGetAllActivities()
        {
            List<Activity> activities = _service.GetAllActivities();

            Assert.AreEqual(5, activities.Count);

            Assert.AreEqual("Bowling", activities[0].Name);
            Assert.AreEqual("Running", activities[1].Name);
            Assert.AreEqual("Jogging", activities[2].Name);
            Assert.AreEqual("Lifting", activities[3].Name);
        }

        [Test]
        public void TestGetAllEvents()
        {
            List<Event> events = _service.GetAllEvents();

            Assert.AreEqual(5, events.Count);

            Assert.AreEqual("Brendan's Party", events[0].EventName);
            Assert.AreEqual("John's Party", events[1].EventName);
            Assert.AreEqual("Quinton's Party", events[2].EventName);
            Assert.AreEqual("Renee's Party", events[3].EventName);
        }

        [Test]
        public void TestGetAllAttendees()
        {
            List<Attendee> attendees = _service.GetAllAttendees();

            Assert.AreEqual(5, attendees.Count);

            Assert.AreEqual("John", attendees[0].Name);
            Assert.AreEqual("David", attendees[1].Name);
            Assert.AreEqual("Jimmy", attendees[2].Name);
            Assert.AreEqual("Stephen", attendees[3].Name);
        }

        [Test]
        public void TestGetEventsByOrganizerId()
        {
            List<Event> events = _service.GetEventsByOrganizerId(4);

            Assert.AreEqual(2, events.Count);

            Assert.AreEqual("Renee's Party", events[0].EventName);
            Assert.AreEqual("Jimmy's Party", events[1].EventName);
        }

        [Test]
        public void TestGetOrganizerEventsThrowsNoEvents()
        {
            Assert.Throws<NoEventsForGivenOrganizer>(() => _service.GetEventsByOrganizerId(-1000));
        }

        [Test]
        public void TestGetEventActivities()
        {
            List<Activity> activities = _service.GetEventActivities(1);

            Assert.AreEqual(5, activities.Count);

            Assert.AreEqual("Bowling", activities[0].Name);
            Assert.AreEqual("Running", activities[1].Name);
            Assert.AreEqual("Jogging", activities[2].Name);
            Assert.AreEqual("Lifting", activities[3].Name);
        }

        [Test]
        public void TestGetEventActivitiesThrowsNoActivities()
        {
            Assert.Throws<System.InvalidOperationException>(() => _service.GetEventActivities(-11));
        }

        [Test]
        public void TestGetEventActivitiesInvalidId()
        {
            Assert.Throws<System.InvalidOperationException>(() => _service.GetEventActivities(-10));
        }

        [Test]
        public void TestGetEventAttendees()
        {
            List<Attendee> attendees = _service.GetEventAttendees(1);

            Assert.AreEqual(2, attendees.Count);

            Assert.AreEqual("John", attendees[0].Name);
            Assert.AreEqual("David", attendees[1].Name);
        }

        [Test]
        public void TestGetEventAttendeeThrowsNoAttendees()
        {
            Assert.Throws<System.InvalidOperationException>(() => _service.GetEventActivities(-11));
        }

        [Test]
        public void TestGetEventAttendeesInvalidId()
        {
            Assert.Throws<System.InvalidOperationException>(() => _service.GetEventAttendees(-10));
        }

        [Test]
        public void TestGetEventOrganizer()
        {
            Organizer test1 = _service.GetEventOrganizer(1);
            Organizer test2 = _service.GetEventOrganizer(2);

            Assert.AreEqual("Jimmy", test1.Name);
            Assert.AreEqual("Steve", test2.Name);
        }

        [Test]
        public void TestGetEventOrganizerInvalidId()
        {
            Assert.Throws<System.InvalidOperationException>(() => _service.GetEventOrganizer(0));
        }

        [Test]
        public void TestAddEvent()
        {
            Organizer thisOrganizer = _service.GetOrganizerById(1);

            Event toAdd = new Event
            {
                EventName = "TEST EVENT",
                Location = "Maryland",
                Time = "3 PM EST",
                OrganizerId = thisOrganizer.Id,
                Category = "Work",
                Date = new System.DateTime(21, 07, 21),
                Duration = 10
            };

            _service.AddEvent(toAdd);

            Assert.AreEqual(6, _service.GetAllEvents().Count);
        }

        [Test]
        public void TestAddEventNonNullableProperty()
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

            _service.AddEvent(toAdd);


            Assert.Throws<InvalidIdException>(() => _service.GetEventById(toAdd.Id));
        }

        [Test]
        public void TestAddActivity()
        {
            Activity activity1 = new Activity
            {
                EventId = 1,
                Name = "Activity 1",
                Duration = 10
            };

            _service.AddActivity(activity1);

            Assert.AreEqual(6, _service.GetAllActivities().Count);
        }

        [Test]
        public void TestAddActivityNonNullableProperty()
        {
            Activity activity1 = new Activity
            {
                EventId = 1,
                Name = null,
                Duration = 10
            };

            _service.AddActivity(activity1);

            Assert.Throws<InvalidIdException>(() => _service.GetActivityById(activity1.Id));
        }

        [Test]
        public void TestAddAttendee()
        {
            Attendee attendee1 = new Attendee
            {
                EventId = 1,
                Name = "Attendee1",
                Email = "attendee1@gmail.com"
            };

            _service.AddAttendee(attendee1);

            Assert.AreEqual(6, _service.GetAllAttendees().Count);
        }

        [Test]
        public void TestAddAttendeeNonNullableProperty()
        {
            Attendee attendee1 = new Attendee
            {
                EventId = 100,
                Name = null,
                Email = "attendee1@gmail.com"
            };

            _service.AddAttendee(attendee1);

            Assert.Throws<InvalidIdException>(() => _service.GetAttendeeById(attendee1.Id));
        }

        [Test]
        public void TestAddOrganizer()
        {
            Organizer thisOrganizer = _service.GetOrganizerById(1);
            thisOrganizer.Name = "New Organizer";

            _service.AddOrganizer(thisOrganizer);

            Assert.Pass();        
        }

        [Test]
        public void TestAddOrganizerNonNullableProperty()
        {
            Organizer thisOrganizer = _service.GetOrganizerById(1);
            thisOrganizer.Name = null;

            _service.AddOrganizer(thisOrganizer);

            Assert.Throws<System.InvalidOperationException>(() => _service.GetOrganizerById(thisOrganizer.Id));
        }

        [Test]
        public void TestRemoveEvent()
        {
            Assert.AreEqual(5, _service.GetAllEvents().Count);

            _service.RemoveEvent(_service.GetEventById(1).Id);

            Assert.AreEqual(4, _service.GetAllEvents().Count);
        }

        [Test]
        public void TestRemoveEventInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.RemoveEvent(-1));
        }

        [Test]
        public void TestRemoveAttendee()
        {
            Assert.AreEqual(5, _service.GetAllAttendees().Count);

            _service.RemoveAttendee(_service.GetAttendeeById(1).Id);

            Assert.AreEqual(4, _service.GetAllAttendees().Count);
        }

        [Test]
        public void TestRemoveAttendeeInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.RemoveAttendee(-1));
        }

        [Test]
        public void TestRemoveActivity()
        {
            Assert.AreEqual(5, _service.GetAllActivities().Count);

            _service.RemoveActivity(_service.GetActivityById(1).Id);

            Assert.AreEqual(4, _service.GetAllActivities().Count);
        }

        [Test]
        public void TestRemoveActivityInvalidId()
        {
            Assert.Throws <InvalidIdException>(() => _service.RemoveActivity(-1));
        }

        [Test]
        public void TestRemoveOrganizer()
        {
            _service.RemoveOrganizer(_service.GetOrganizerById(1).Id);

            Assert.Pass();
        }

        [Test]
        public void TestRemoveOrganizerInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.RemoveOrganizer(-1));
        }

        [Test]
        public void TestEditEvent()
        {
            Event thisEvent = _service.GetEventById(1);

            Assert.IsNotNull(thisEvent);

            thisEvent.EventName = "NEW EDITED NAME";
            thisEvent.Duration = 1000;
            thisEvent.Location = "Times Square";

            _service.EditEvent(thisEvent);

            thisEvent = _service.GetEventById(1);

            Assert.AreEqual("NEW EDITED NAME", thisEvent.EventName);
            Assert.AreEqual(1000, thisEvent.Duration);
            Assert.AreEqual("Times Square", thisEvent.Location);
        }

        [Test]
        public void TestEditInvalidEventThrowsNullObject()
        {
            Event thisEvent = _service.GetEventById(1);

            Assert.IsNotNull(thisEvent);

            thisEvent.EventName = null;
            thisEvent.Duration = 1000;
            thisEvent.Location = "Times Square";

            Assert.Throws<NullObjectException>(() => _service.EditEvent(thisEvent));

        }

        [Test]
        public void TestEditAttendee()
        {
            Attendee thisAttendee = _service.GetAttendeeById(1);

            Assert.IsNotNull(thisAttendee);

            thisAttendee.Name = "EDITED NAME";
            thisAttendee.Email = "EDITED@GMAIL.COM";
            thisAttendee.EventId = 2;

            _service.EditAttendee(thisAttendee);

            thisAttendee = _service.GetAttendeeById(1);

            Assert.AreEqual("EDITED NAME", thisAttendee.Name);
            Assert.AreEqual("EDITED@GMAIL.COM", thisAttendee.Email);

        }

        [Test]
        public void TestEditAttendeeThrowsNullException()
        {

            Attendee thisAttendee = _service.GetAttendeeById(1);

            Assert.IsNotNull(thisAttendee);

            thisAttendee.Name = "EDITED NAME";
            thisAttendee.Email = "EDITED@GMAIL.COM";
            thisAttendee.EventId = -1;

            Assert.Throws<NullObjectException>(() => _service.EditAttendee(thisAttendee));
        }

        [Test]
        public void TestEditActivity()
        {
            Activity thisActivity = _service.GetActivityById(1);

            Assert.IsNotNull(thisActivity);

            thisActivity.Name = "EDITED NAME";
            thisActivity.Duration = 1000;
            thisActivity.EventId = 2;

            _service.EditActivity(thisActivity);

            Assert.AreEqual("EDITED NAME", thisActivity.Name);
            Assert.AreEqual(1000, thisActivity.Duration);
        }

        [Test]
        public void TestEditActivityThrowsNullException()
        {
            Activity thisActivity = _service.GetActivityById(1);

            Assert.IsNotNull(thisActivity);

            thisActivity.Name = null;
            thisActivity.Duration = 1000;
            thisActivity.EventId = 2;

            Assert.Throws<NullObjectException>(() => _service.EditActivity(thisActivity));
        }

        [Test]
        public void TestEditOrganizer()
        {
            Organizer thisOrganizer = _service.GetOrganizerById(1);

            Assert.IsNotNull(thisOrganizer);

            thisOrganizer.Name = "EDITED NAME";
            thisOrganizer.Email = "EDITED@GMAIL.COM";

            _service.EditOrganizer(thisOrganizer);

            Assert.AreEqual("EDITED NAME", thisOrganizer.Name);
            Assert.AreEqual("EDITED@GMAIL.COM", thisOrganizer.Email);
        }

        [Test]
        public void TestEditOrganizerThrowsNullException()
        {
            Organizer thisOrganizer = _service.GetOrganizerById(1);

            Assert.IsNotNull(thisOrganizer);

            thisOrganizer.Name = "EDITED NAME";
            thisOrganizer.Email = null;

            Assert.Throws<NullObjectException>(() => _service.EditOrganizer(thisOrganizer));
        }

        [Test]
        public void TestGetOrganizerAsUser()
        {
            Assert.IsNotNull(_service.GetOrganizerById(1));

            Organizer toTest = _service.GetOrganizerById(2);

            Assert.AreEqual("Steve", toTest.Name);
            Assert.AreEqual(5, toTest.OrganizedEvents.Count);
        }

        [Test]
        public void TestGetActivityById()
        {
            Activity toTest = _service.GetActivityById(1);

            Assert.IsNotNull(toTest);

            Assert.AreEqual("Bowling", toTest.Name);
            Assert.AreEqual(50, toTest.Duration);
        } 

        [Test]
        public void TestGetActivityByInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetActivityById(-1));

            Assert.Throws<InvalidIdException>(() => _service.GetActivityById(0));
        }
        
        [Test]
        public void TestGetActivityWithNonExistentIdThrowsNullReference()
        {
            Assert.Throws<System.NullReferenceException>(() => _service.GetActivityById(1100));

            Assert.Throws<System.NullReferenceException>(() => _service.GetActivityById(110));

            Assert.Throws<System.NullReferenceException>(() => _service.GetActivityById(92));
        }

        [Test] 
        public void TestGetEventById()
        {
            Event toTest = _service.GetEventById(1);

            Assert.IsNotNull(toTest);

            Assert.AreEqual("Brendan's Party", toTest.EventName);
            Assert.AreEqual(5, toTest.Activities.Count);
            Assert.AreEqual(2, toTest.Attendees.Count);
            Assert.AreEqual(new System.DateTime(2021, 6, 28), toTest.Date);
        }

        [Test]
        public void TestGetEventByInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetEventById(-1));

            Assert.Throws<InvalidIdException>(() => _service.GetEventById(0));
        }

        [Test]
        public void TestGetEventWithNonExistentIdThrowsNullReference()
        {
            Assert.Throws<System.NullReferenceException>(() => _service.GetEventById(1100));

            Assert.Throws<System.NullReferenceException>(() => _service.GetEventById(110));

            Assert.Throws<System.NullReferenceException>(() => _service.GetEventById(92));
        }

        [Test]
        public void TestGetAttendeeById()
        {
            Attendee toTest = _service.GetAttendeeById(1);

            Assert.IsNotNull(toTest);

            Assert.AreEqual("John", toTest.Name);

            toTest = _service.GetAttendeeById(2);

            Assert.IsNotNull(toTest);

            Assert.AreEqual("David", toTest.Name);
        }

        [Test]
        public void TestGetAttendeeByInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetAttendeeById(-1));

            Assert.Throws<InvalidIdException>(() => _service.GetAttendeeById(0));
        }

        [Test]
        public void TestGetAttendeeWithNonExistentIdThrowsNullReference()
        {
            Assert.Throws<System.NullReferenceException>(() => _service.GetAttendeeById(1100));

            Assert.Throws<System.NullReferenceException>(() => _service.GetAttendeeById(110));

            Assert.Throws<System.NullReferenceException>(() => _service.GetAttendeeById(92));
        }

        [Test]

        public void TestGetOrganizerById()
        {
            Organizer toTest = _service.GetOrganizerById(1);

            Assert.IsNotNull(toTest);

            Assert.AreEqual("Jimmy", toTest.Name);

            toTest = _service.GetOrganizerById(2);

            Assert.IsNotNull(toTest);

            Assert.AreEqual("Steve", toTest.Name);
        }

        [Test]
        public void TestGetOrganizerByInvalidId()
        {
            Assert.Throws<InvalidIdException>(() => _service.GetOrganizerById(-1));

            Assert.Throws<InvalidIdException>(() => _service.GetOrganizerById(0));
        }

        [Test]
        public void TestGetOrganizerWithNonExistentIdThrowsNullReference()
        {
            Assert.Throws<System.NullReferenceException>(() => _service.GetOrganizerById(1100));

            Assert.Throws<System.NullReferenceException>(() => _service.GetOrganizerById(110));

            Assert.Throws<System.NullReferenceException>(() => _service.GetOrganizerById(92));
        }

        [Test]
        public void TestGetEventByName()
        {
            Event toTest = _service.GetEventByName("Brendan's Party");

            Assert.IsNotNull(toTest);

            Assert.AreEqual(5, toTest.Activities.Count);
            Assert.AreEqual(2, toTest.Attendees.Count);
            Assert.AreEqual(new System.DateTime(2021, 6, 28), toTest.Date);

            toTest = _service.GetEventByName("Jimmy's Party");

            Assert.AreEqual(5, toTest.Activities.Count);
            Assert.AreEqual(5, toTest.Attendees.Count);
            Assert.AreEqual(new System.DateTime(1996, 2, 20), toTest.Date);
        }

        [Test]
        public void TestGetEventByNameNullName()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetEventByName(null));

        }

        [Test]
        public void TestGetOrganizerByName()
        {
            Organizer toTest = _service.GetOrganizerByName("Steve");

            Assert.IsNotNull(toTest);

            Assert.AreEqual(2, toTest.Id);

            toTest = _service.GetOrganizerByName("Mario");

            Assert.IsNotNull(toTest);

            Assert.AreEqual(5, toTest.Id);
        }

        [Test]
        public void TestGetOrganizerByNameNullName()
        {
            Assert.Throws<InvalidNameException>(() => _service.GetOrganizerByName(null));

        }
    }
}