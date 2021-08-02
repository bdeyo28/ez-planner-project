using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlannerAPI.Models.Domain;

namespace PlannerAPI.Models.Auth
{
    [Table("Organizer")]
    public class Organizer
    {
        public Organizer()
        {
        }

        [Required]
        public string Name { get; set; }
        [Column("Id")]
        public int Id { get; set; }
        [Column("OrganizedEvents")]
        public List<Event> OrganizedEvents { get; set; } = new List<Event>();

        [Required]
        [MaxLength(75)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public List<OrganizerRole> Roles { get; set; } = new List<OrganizerRole>();

        public Organizer(Organizer that)
        {
            Name = that.Name;
            Id = that.Id;
            OrganizedEvents = that.OrganizedEvents;
            Roles = that.Roles;
        }

        public Organizer(string name, int id, List<Event> theseEvents, List<OrganizerRole> roles)
        {
            Name = name;
            Id = id;
            OrganizedEvents = theseEvents;
            Roles = roles;
        }
    }
}
