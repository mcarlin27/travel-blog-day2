using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table("People")]
    public class People
    {
        [Key]
        public int PeopleId { get; set; }
        public string Description { get; set; }
        public int ExperienceId { get; set; }
        public virtual Experience Experiences { get; set; }
    }
}