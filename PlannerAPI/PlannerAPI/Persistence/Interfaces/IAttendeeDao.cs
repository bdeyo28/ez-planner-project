using PlannerAPI.Models;
using PlannerAPI.Models.Domain;
using System.Collections.Generic;

namespace PlannerAPI.Persistence
{
    public interface IAttendeeDao
    {
        int AddAttendee(Attendee toAdd);
        void EditAttendee(Attendee updated);
        List<Attendee> GetAllAttendees();
        Attendee GetAttendeeById(int id);
        void RemoveAttendee(Attendee toRemove);
    }
}