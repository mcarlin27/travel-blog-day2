using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
        [Table("Locations")]
    public class Location
    {
        public Location()
        {
            this.Experiences = new HashSet<Experience>();
            //this.People = new HashSet<People>();
        }
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Experience> Experiences { get; set; }
        //public virtual ICollection<People> People { get; set; }

    }
}