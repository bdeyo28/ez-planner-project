using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannerAPI.Models.Domain
{
    [Table("Attendee")]
    public class Attendee
    {
        public Attendee()
        {

        }

        [Column("Id")]
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        [Required]
        [MaxLength(75)]
        public string Email { get; set; }

        public int EventId { get; set; }

        public Attendee(Attendee that)
        {
            Id = that.Id;
            Name = that.Name;
        }

        public Attendee(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
