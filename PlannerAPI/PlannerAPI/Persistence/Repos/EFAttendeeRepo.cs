using PlannerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannerAPI.Models.Domain;

namespace PlannerAPI.Persistence.Repos
{
    public class EFAttendeeRepo : IAttendeeDao
    {
        PlannerDbContext _context;

        public EFAttendeeRepo(PlannerDbContext context)
        {
            _context = context;
        }

        public int AddAttendee(Attendee toAdd)
        {
            _context.Attendees.Add(toAdd);
            _context.SaveChanges();

            return toAdd.Id;
        }

        public void EditAttendee(Attendee updated)
        {
            _context.Attendees.Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Attendee> GetAllAttendees()
        {
            return _context.Attendees.ToList();
        }

        public Attendee GetAttendeeById(int id)
        {
            return _context.Attendees.Find(id);
        }

        public void RemoveAttendee(Attendee toRemove)
        {
            _context.Attendees.Remove(toRemove);
            _context.SaveChanges();
        }
    }
}
