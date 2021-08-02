using System;
using System.Linq;
using System.Collections.Generic;
using PlannerAPI.Models;
using PlannerAPI.Models.Domain;

namespace PlannerAPI.Persistence
{
    public class AttendeeInMemDao : IAttendeeDao
    {
        List<Attendee> _attendees = new List<Attendee>();

        public AttendeeInMemDao()
        {
            _attendees.Add(new Attendee(1, "John"));
            _attendees.Add(new Attendee(2, "David"));
            _attendees.Add(new Attendee(3, "Jimmy"));
            _attendees.Add(new Attendee(4, "Stephen"));
            _attendees.Add(new Attendee(5, "Walter"));
        }

        public List<Attendee> GetAllAttendees()
        {
            return _attendees.Where(a => a != null).ToList();
        }

        public Attendee GetAttendeeById(int id)
        {
            List<Attendee> attendees = GetAllAttendees();

            Attendee toReturn = attendees.Where(a => a.Id == id).SingleOrDefault();

            if (toReturn == null)
                throw new NullReferenceException("Attendee does not exist");

            return toReturn;
        }

        public void RemoveAttendee(Attendee toRemove)
        {
            _attendees = _attendees.Where(a => a.Id != toRemove.Id).ToList();
        }

        public int AddAttendee(Attendee toAdd)
        {
            if (toAdd != null)
            {
                _attendees.Add(toAdd);
                return toAdd.Id;
            }
            else
                throw new NullReferenceException("Attendee is null");
        }

        public void EditAttendee(Attendee updated)
        {
            List<Attendee> attendees = GetAllAttendees();

            for (int i = 0; i < attendees.Count; i++)
            {
                Attendee attendee = attendees[i];
                if (attendee.Id == updated.Id)
                {
                    attendee.Id = updated.Id;
                    attendee.Name = updated.Name;
                    _attendees[i] = attendee;
                }
            }
        }
    }
}
