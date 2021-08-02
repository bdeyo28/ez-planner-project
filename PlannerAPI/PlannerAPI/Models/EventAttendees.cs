using PlannerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Models
{
    [Table("EventAttendees")]
    public class EventAttendees
    {
        public Event SelectedEvent { get; set; }
        public Attendee SelectedAttendee { get; set; }

        [ForeignKey("SelectedEvent")]
        public int? EventId { get; set; }
        [ForeignKey("SelectedAttendee")]
        public int? AttendeeId { get; set; }
        
    }
}
