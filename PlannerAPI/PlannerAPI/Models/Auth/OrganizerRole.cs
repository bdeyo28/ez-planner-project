using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerAPI.Models.Auth
{
    public class OrganizerRole
    {
        public Role SelectedRole { get; set; }
        public Organizer EnrolledOrganizer { get; set; }
        
        [ForeignKey("SelectedRole")]
        public int RoleId { get; set; }
        [ForeignKey("EnrolledOrganizer")]
        public int OrganizerId { get; set; }
    }
}
