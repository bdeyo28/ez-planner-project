using System;
using System.Linq;
using System.Collections.Generic;
using PlannerAPI.Models;
using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Domain;

namespace PlannerAPI.Persistence
{
    public class OrganizerInMemDao : IOrganizerDao
    {
        List<Organizer> _organizers = new List<Organizer>();
        List<Event> _eventList = new List<Event>();
        List<Attendee> _attendees = new List<Attendee>();
        List<Activity> _activityList = new List<Activity>();

        public OrganizerInMemDao()
        {
            List<OrganizerRole> roles = new List<OrganizerRole>();

            _attendees.Add(new Attendee(1, "John"));
            _attendees.Add(new Attendee(2, "David"));
            _attendees.Add(new Attendee(3, "Jimmy"));
            _attendees.Add(new Attendee(4, "Stephen"));
            _attendees.Add(new Attendee(5, "Walter"));

            _organizers.Add(new Organizer("Jimmy", 1, _eventList, roles));
            _organizers.Add(new Organizer("Steve", 2, _eventList, roles));
            _organizers.Add(new Organizer("John", 3, _eventList, roles));
            _organizers.Add(new Organizer("Bob", 4, _eventList, roles));
            _organizers.Add(new Organizer("Mario", 5, _eventList, roles));

            _eventList.Add(new Event(1, "Brendan's Party",
               new DateTime(2021, 6, 28), _attendees, 60, _activityList, _organizers[0].Id));
            _eventList.Add(new Event(2, "John's Party",
                new DateTime(2020, 5, 06), _attendees, 30, _activityList, _organizers[1].Id));
            _eventList.Add(new Event(3, "Quinton's Party",
                new DateTime(1997, 2, 19), _attendees, 85, _activityList, _organizers[2].Id));
            _eventList.Add(new Event(4, "Renee's Party",
                new DateTime(1956, 8, 20), _attendees, 90, _activityList, _organizers[3].Id));
            _eventList.Add(new Event(5, "Jimmy's Party",
                new DateTime(1996, 2, 20), _attendees, 80, _activityList, _organizers[3].Id));
        }

        public List<Organizer> GetAllOrganizers()
        {
            return _organizers.Where(o => o != null).ToList();
        }

        public void RemoveOrganizer(Organizer toRemove)
        {
            _organizers = _organizers.Where(o => o.Id != toRemove.Id).ToList();
        }

        public int AddOrganizer(Organizer toAdd)
        {
            if (toAdd != null)
            {
                _organizers.Add(toAdd);
                return toAdd.Id;
            }

            else
                throw new NullReferenceException("Organizer is null");
        }

        public void EditOrganizer(Organizer updated)
        {
            List<Organizer> organizers = GetAllOrganizers();

            for (int i = 0; i < organizers.Count; i++)
            {
                Organizer organizer = organizers[i];
                if (organizer.Id == updated.Id)
                {
                    organizer.Id = updated.Id;
                    organizer.Name = updated.Name;
                    organizer.OrganizedEvents = updated.OrganizedEvents;
                    _organizers[i] = organizer;
                }
            }
        }

        public Organizer GetOrganizerById(int id)
        {
            List<Organizer> organizers = GetAllOrganizers();

            Organizer toReturn = organizers.Where(o => o.Id == id).SingleOrDefault();

            if (toReturn == null)
                throw new NullReferenceException("Organizer is null");
            else
                return toReturn;
        }

        public Role GetOrganizerRole(string name)
        {
            throw new NotImplementedException();
        }

        public Organizer GetOrganizerByName(string username)
        {
            return _organizers.Where(o => o.Name.ToLower() == username.ToLower()).Single();
        }

        public Organizer GetUserAsOrganizer(int id)
        {
            throw new NotImplementedException();
        }
    }
}

