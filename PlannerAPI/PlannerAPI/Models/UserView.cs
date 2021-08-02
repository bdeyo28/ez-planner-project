using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannerAPI.Models.Auth;
using PlannerAPI.Models.Domain;

namespace PlannerAPI.Models
{
    public class UserView
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public List<Event> OrganizedEvents { get; set; }
        public string Name { get; set; }
        public UserView(Organizer org)
        {
            Email = org.Email;
            Id = org.Id;
            OrganizedEvents = org.OrganizedEvents;
            Name = org.Name;
        }

    }
}
