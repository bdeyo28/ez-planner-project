using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannerAPI.Models.Domain
{
    [Table("Schedule")]
    public class Schedule
    {
        public Schedule()
        { 
            
        }

        public Schedule(Schedule that)
        {
            Id = that.Id;
            StartDate = that.StartDate;
            EndDate = that.EndDate;
            Events = that.Events;
        }

        public Schedule(int id, DateTime start, DateTime end, List<Event> events)
        {
            Id = id;
            StartDate = start;
            EndDate = end;
            Events = events;
        }

        [Column("Id")]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Event> Events { get; set; }

    }
}
