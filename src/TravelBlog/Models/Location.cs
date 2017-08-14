using System.Collections.Generic;
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
        }
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Experience> Experiences { get; set; }

        public override bool Equals(System.Object otherLocation)
        {
            if (!(otherLocation is Location))
            {
                return false;
            }
            else
            {
                Location newLocation = (Location)otherLocation;
                return this.LocationId.Equals(newLocation.LocationId);
            }
        }

        public override int GetHashCode()
        {
            return this.LocationId.GetHashCode();
        }

    }
}
