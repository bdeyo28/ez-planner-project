using Microsoft.EntityFrameworkCore;
using PlannerAPI.Models;
using PlannerAPI.Models.Domain;
using PlannerAPI.Persistence;
using PlannerAPI.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Persistence.Repos
{
    public class EFScheduleRepo : IScheduleDao
    {
        PlannerDbContext _context;

        public EFScheduleRepo(PlannerDbContext context)
        {
            _context = context;
        }

        public int AddSchedule(Schedule toAdd)
        {
            _context.Schedules.Add(toAdd);
            _context.SaveChanges();

            return toAdd.Id;
        }

        public void RemoveSchedule(Schedule toRemove)
        {
            _context.Schedules.Remove(toRemove);
            _context.SaveChanges();
        }

        public void EditSchedule(Schedule updated)
        {
            _context.Schedules.Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Schedule> GetAllSchedules()
        {
            throw new NotImplementedException();
        }

        public Schedule GetScheduleById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
