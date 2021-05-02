using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkAPI.SWAPI
{
    public class APIResponseStarships
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Starships> Results { get; set; }
    }
}
