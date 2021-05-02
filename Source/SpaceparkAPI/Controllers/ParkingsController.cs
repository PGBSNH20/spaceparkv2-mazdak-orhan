using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceparkAPI.Models;
using SpaceParkAPI.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingsController : ControllerBase
    {
        private SpaceParkContext _dbContext;
        public ParkingsController(SpaceParkContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public IActionResult GetParkingHistory()
        {
            var parkings = from parking in _dbContext.Parkings
                           join spaceport in _dbContext.SpacePorts on parking.SpacePortId equals spaceport.Id
                           where parking.EndTime != null
                           select new
                           {
                               Id = parking.Id,
                               SpacePort = spaceport.Name,
                               Traveller = parking.Traveller,
                               Starship = parking.StarShip,
                               StartTime = parking.StartTime,
                               EndTime = parking.EndTime
                           };
            return Ok(parkings);
        }

        [HttpGet("[action]")]
        public IActionResult GetActiveParkings()
        {
            var parkings = from parking in _dbContext.Parkings
                           join spaceport in _dbContext.SpacePorts on parking.SpacePortId equals spaceport.Id
                           where parking.EndTime == null
                           select new
                           {
                               Id = parking.Id,
                               SpacePort = spaceport.Name,
                               Traveller = parking.Traveller,
                               Starship = parking.StarShip,
                               StartTime = parking.StartTime,
                           };
            return Ok(parkings);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddParking([FromBody] Parking parkingObj)
        {
            //Added this line to Parse double values to not mix "." and ","
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            //Fetch starwars characters from Swapi
            var personSwapi = Swapi.Fetch.People();
            await personSwapi;

            //Fetch starwars starships from Swapi
            var starshipSwapi = Swapi.Fetch.Starships();
            await starshipSwapi;

            //Select the starship input name.
            var starshipApiMatch = starshipSwapi.Result.Where(x => x.Name.ToLower() == parkingObj.StarShip.ToLower()).FirstOrDefault();
            var starshipLength = double.TryParse(starshipApiMatch.Length, out double result);

            //EDIT FirstOrDefault ovan så att vi kan välja att skriva in ett starship namn, därefter matcha det och kolla så att det finns i Swapi.

            string travellerName = "";
            int travellerHasStarship = 0;

            var travellerApiMatch = personSwapi.Result.Where(x => x.Name.ToLower() == parkingObj.Traveller.ToLower()).FirstOrDefault();

            if (travellerApiMatch != null)
            {
                travellerName = travellerApiMatch.Name.ToLower();
                travellerHasStarship = travellerApiMatch.Starships.Count;
            }

            if (travellerName == null || travellerHasStarship == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (travellerName == parkingObj.Traveller.ToLower() && travellerHasStarship > 0 && result < 15)
            {
                parkingObj.Traveller = parkingObj.Traveller.ToLower();
                parkingObj.StarShip = parkingObj.StarShip.ToLower();
                parkingObj.StartTime = DateTime.Now;
                _dbContext.Parkings.Add(parkingObj);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return NotFound("We could not find a starwars character by that name");
            }
        }
    }
}
