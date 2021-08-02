using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Domain;
using System.Collections.Generic;

namespace PlannerAPI.Services
{
    public interface IPlannerService
    {
        void AddActivity(Activity toAdd);
        void AddAttendee(Attendee toAdd);
        void AddEvent(Event toAdd);
        void AddOrganizer(Organizer toAdd);
        void EditActivity(Activity updated);
        void EditAttendee(Attendee updated);
        void EditEvent(Event updated);
        void EditOrganizer(Organizer updated);
        Activity GetActivityById(int id);
        List<Activity> GetAllActivities();
        List<Attendee> GetAllAttendees();
        List<Event> GetAllEvents();
        Attendee GetAttendeeById(int id);
        List<Activity> GetEventActivities(int id);
        List<Attendee> GetEventAttendees(int id);
        Event GetEventById(int id);
        Event GetEventByName(string name);
        Organizer GetEventOrganizer(int id);
        List<Event> GetEventsByOrganizerId(int id);
        Organizer GetOrganizerById(int id);
        Organizer GetOrganizerByName(string name);
        Organizer GetUserAsOrganizer(int id);
        void RemoveActivity(int id);
        void RemoveAttendee(int id);
        void RemoveEvent(int id);
        void RemoveOrganizer(int id);
    }
}