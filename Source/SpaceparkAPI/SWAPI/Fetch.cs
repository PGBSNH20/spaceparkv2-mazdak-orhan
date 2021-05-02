using RestSharp;
using SpaceparkAPI.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkAPI.SWAPI
{
    public class Fetch
    {
        private const string baseURL = "http://swapi.dev/api/";

        //Fetch people from API
        public static async Task<List<Person>> People(string input)
        {
            var client = new RestClient(baseURL);
            string requestUrl = $"http://swapi.dev/api/people/?search={input}";
            APIResponseTraveller response;
            List<Person> persons = new List<Person>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponseTraveller>(request);

                persons.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return persons;
        }

        //Fetch Starships from API
        public static async Task<List<Starship>> Starships()
        {
            var client = new RestClient(baseURL);
            string requestUrl = "http://swapi.dev/api/starships/";
            APIResponseStarships response;
            List<Starship> starships = new List<Starship>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponseStarships>(request);

                starships.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return starships;
        }
    }
}
