using SpaceparkAPI.Objects;
using System.Collections.Generic;

namespace SpaceparkAPI.Swapi
{
    public class APIResponseTraveller
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Person> Results { get; set; }
    }
}
