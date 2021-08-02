using PlannerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannerAPI.Models.Auth;

namespace PlannerAPI.Persistence.Repos
{
    public class EFOrganizerRepo : IOrganizerDao
    {
        PlannerDbContext _context;

        public EFOrganizerRepo(PlannerDbContext context)
        {
            _context = context;
        }

        public Role GetOrganizerRole(string name)
        {
            return _context.Roles.SingleOrDefault(r => r.Name.ToLower() == name.ToLower());
        }
        public int AddOrganizer(Organizer toAdd)
        {
            _context.Organizers.Add(toAdd);
            _context.SaveChanges();

            return toAdd.Id;
        }

        public void EditOrganizer(Organizer updated)
        {
            _context.Organizers.Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Organizer> GetAllOrganizers()
        {
            return _context.Organizers.ToList();
        }

        public Organizer GetOrganizerById(int id)
        {
            return _context.Organizers.Find(id);
        }

        public void RemoveOrganizer(Organizer toRemove)
        {
            _context.Organizers.Remove(toRemove);
            _context.SaveChanges();
        }

        public Organizer GetOrganizerByName(string username)
        {
            return _context.Organizers.Include("Roles.SelectedRole")
                .SingleOrDefault(o => o.Name.ToLower() == username.ToLower());
        }

        public Organizer GetUserAsOrganizer(int id)
        {
            return _context.Organizers.SingleOrDefault(org => org.Id == id);
        }
    }
}
