using PlannerAPI.Models;
using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Domain;
using System.Collections.Generic;

namespace PlannerAPI.Persistence
{
    public interface IEventDao
    {
        int AddEvent(Event toAdd);
        List<Event> GetAllEvents();
        Event GetEventById(int id);

        Event GetEventByName(string name);

        void RemoveEvent(Event toRemove);
        void EditEvent(Event updated);

        List<Event> GetEventsByOrganizerId(int id);
        List<Activity> GetEventActivities(int id);
        List<Attendee> GetEventAttendees(int id);
        Organizer GetEventOrganizer(int? id);
    }
}