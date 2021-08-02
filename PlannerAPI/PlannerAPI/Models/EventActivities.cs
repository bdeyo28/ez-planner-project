using PlannerAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Models
{
    [Table("EventActivities")]
    public class EventActivities
    {
        public Event SelectedEvent { get; set; }
        public Activity SelectedActivity { get; set; }

        [ForeignKey("SelectedEvent")]
        public int? EventId { get; set; }

        [ForeignKey("SelectedActivity")]
        public int? ActivityId { get; set; }
    }
}
