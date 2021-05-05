using SpaceparkAPI.Objects;
using System.Collections.Generic;

namespace SpaceparkAPI.Swapi
{
    public class APIResponseStarships
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Starship> Results { get; set; }
    }
}
