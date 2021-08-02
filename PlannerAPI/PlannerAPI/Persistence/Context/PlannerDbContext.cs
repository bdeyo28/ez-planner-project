using PlannerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlannerAPI.Models.Domain;
using PlannerAPI.Models.Auth;

namespace PlannerAPI.Persistence
{
    public class PlannerDbContext : DbContext
    {
        //private readonly ILoggerFactory _loggerFactory;

        public DbSet<Event> Events { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Organizer> Organizers { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<EventActivities> EventActivities { get; set; }

        public DbSet<EventAttendees> EventAttendees { get; set; }

        public DbSet<EventOrganizer> EventOrganizer { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<OrganizerRole> OrganizerRoles { get; set; }

        public PlannerDbContext(DbContextOptions<PlannerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EventActivities>().HasForeignKey(e => new
            //{
            //    e.EventId,
            //    e.ActivityId
            //});

            modelBuilder.Entity<EventActivities>().HasNoKey();

            modelBuilder.Entity<EventAttendees>().HasNoKey();

            modelBuilder.Entity<EventOrganizer>().HasNoKey();

            modelBuilder.Entity<OrganizerRole>().HasKey(ur => new { ur.OrganizerId, ur.RoleId });

            //modelBuilder.Entity<EventAttendees>().HasKey(e => new
            //{
            //    e.EventId,
            //    e.AttendeeId
            //});

            //modelBuilder.Entity<EventOrganizer>().HasKey(e => new
            //{
            //    e.EventId,
            //    e.OrganizerId
            //});
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLoggerFactory(_loggerFactory);
        //}

        //public PlannerDbContext() : base()
        //{

        //}
    }
}
