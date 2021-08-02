using System;
using System.Collections.Generic;
using PlannerAPI.Models;
using System.Linq;
using PlannerAPI.Models.Domain;
using PlannerAPI.Models.Auth;
using PlannerAPI.Exceptions;

namespace PlannerAPI.Persistence
{
    public class EventInMemDao : IEventDao
    {

        List<Event> _eventList = new List<Event>();
        List<Attendee> _attendees = new List<Attendee>();
        List<Activity> _activityList = new List<Activity>();
        List<Organizer> _organizers = new List<Organizer>();


        public EventInMemDao()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            _organizers.Add(new Organizer("Jimmy", 1, _eventList, roles));
            _organizers.Add(new Organizer("Steve", 2, _eventList, roles));
            _organizers.Add(new Organizer("John", 3, _eventList, roles));
            _organizers.Add(new Organizer("Bob", 4, _eventList, roles));
            _organizers.Add(new Organizer("Mario", 5, _eventList, roles));

            _activityList.Add(new Activity(1, "Bowling", 50));
            _activityList.Add(new Activity(2, "Running", 20));
            _activityList.Add(new Activity(3, "Jogging", 40));
            _activityList.Add(new Activity(4, "Lifting", 90));
            _activityList.Add(new Activity(5, "Swimming", 50));

            _attendees.Add(new Attendee(1, "John"));
            _attendees.Add(new Attendee(2, "David"));
            _attendees.Add(new Attendee(3, "Jimmy"));
            _attendees.Add(new Attendee(4, "Stephen"));
            _attendees.Add(new Attendee(5, "Walter"));

            List<Attendee> toTest = new List<Attendee>();
            toTest.Add(_attendees[0]);
            toTest.Add(_attendees[1]);

            _eventList.Add(new Event(1, "Brendan's Party", 
                new DateTime(2021, 6, 28), toTest, 60, _activityList, _organizers[0].Id));
            _eventList.Add(new Event(2, "John's Party", 
                new DateTime(2020, 5, 06), _attendees, 30, _activityList, _organizers[1].Id));
            _eventList.Add(new Event(3, "Quinton's Party", 
                new DateTime(1997, 2, 19), _attendees, 85, _activityList, _organizers[2].Id));
            _eventList.Add(new Event(4, "Renee's Party", 
                new DateTime(1956, 8, 20), _attendees, 90, _activityList, _organizers[3].Id));
            _eventList.Add(new Event(5, "Jimmy's Party", 
                new DateTime(1996, 2, 20), _attendees, 80, _activityList, _organizers[3].Id));
        }

        public Event GetEventById(int id)
        {
            List<Event> events = GetAllEvents();

            Event toReturn = events.Where(e => e.Id == id).SingleOrDefault();

            if (toReturn == null)
                throw new NullReferenceException("Event does not exist");

            return toReturn;
        }

        public void RemoveEvent(Event toRemove)
        {
            _eventList = _eventList.Where(e => e.Id != toRemove.Id).ToList();
        }

        public List<Event> GetAllEvents()
        {
            return _eventList.Where(e => e != null).ToList();
        }

        public int AddEvent(Event toAdd)
        {
            if (toAdd == null)
                throw new NullReferenceException("Event is null");
            else
            {
                _eventList.Add(toAdd);
                return toAdd.Id;
            }
        }

        public void EditEvent(Event updated)
        {
            List<Event> allEvents = GetAllEvents();

            for(int i = 0; i < allEvents.Count; i++)
            {
                Event thisEvent = allEvents[i];
                if(thisEvent.Id == updated.Id)
                {
                    thisEvent.Id = updated.Id;
                    thisEvent.EventName = updated.EventName;
                    thisEvent.Date = updated.Date;
                    thisEvent.Activities = updated.Activities;
                    thisEvent.Attendees = updated.Attendees;
                    thisEvent.Category = updated.Category;
                    thisEvent.OrganizerId = updated.OrganizerId;
                    thisEvent.Location = updated.Location;
                    thisEvent.Time = updated.Time;
                    thisEvent.Duration = updated.Duration;
                    _eventList[i] = thisEvent;
                }
            }
        }

        public Event GetEventByName(string name)
        {
            List<Event> events = GetAllEvents();

            Event toReturn = events.Where(e => e.EventName.ToLower() == name.ToLower()).Single();

            if (toReturn == null)
                throw new InvalidEventException("This event does not exist");
            else
                return toReturn;
        }

        public List<Event> GetEventsByOrganizerId(int id)
        {
            List<Event> events = GetAllEvents();

            List<Event> toReturn = events.Where(e => e.OrganizerId == id).ToList();

            if (toReturn == null || toReturn.Count == 0)
                throw new NoEventsForGivenOrganizer("No events where found for this organizer id");
            else
                return toReturn;
        }

        public List<Activity> GetEventActivities(int id)
        {
            List<Event> events = GetAllEvents();

            Event thisEvent = events.Where(e => e.Id == id).Single();

            List<Activity> toReturn = thisEvent.Activities;

            if (toReturn == null || toReturn.Count == 0)
                throw new NoActivitiesForGivenEventException("No activities were found for this event");
            else
                return toReturn;
        }

        public List<Attendee> GetEventAttendees(int id)
        {
            List<Event> events = GetAllEvents();

            Event thisEvent = events.Where(e => e.Id == id).Single();

            List<Attendee> toReturn = thisEvent.Attendees;

            if (toReturn == null || toReturn.Count == 0)
                throw new NoAttendeesForGivenEventException("No attendees were found for this event");
            else
                return toReturn;
        }

        public Organizer GetEventOrganizer(int? id)
        {
            List<Event> events = GetAllEvents();

            Event thisEvent = events.Where(e => e.Id == id).Single();

            Organizer toReturn = _organizers.Where(o => o.Id == thisEvent.OrganizerId).Single();

            if (toReturn == null)
                throw new NoOrganizerForGivenEventException("No activities were found for this event");
            else
                return toReturn;
        }
    }
}
