using System.Collections.Generic;

namespace SpaceparkAPI.Models
{
    public class SpacePort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Parking> Parkings { get; set; }
    }
}
