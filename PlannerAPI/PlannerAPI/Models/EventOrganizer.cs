using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Models
{
    [Table("EventOrganizer")]
    public class EventOrganizer
    {
        public Event SelectedEvent { get; set; }
        public Organizer SelectedOrganizer { get; set; }

        [ForeignKey("SelectedEvent")]
        public int? EventId { get; set; }

        [ForeignKey("SelectedOrganizer")]
        public int? OrganizerId { get; set; }
    }
}
