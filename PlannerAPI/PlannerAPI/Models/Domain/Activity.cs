using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlannerAPI.Models.Domain
{
    [Table("Activity")]
    public class Activity
    {
        public Activity()
        {
        }

        //[Column("Name")]
        //public Event Event { get; set; }

        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }

        public int EventId { get; set; }

        public Activity(Activity that)
        {
            Duration = that.Duration;
            Id = that.Id;
            Name = that.Name;
        }

        public Activity(int id, string name, int duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}
