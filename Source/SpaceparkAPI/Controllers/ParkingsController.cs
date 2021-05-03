using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceparkAPI.Models;
using SpaceparkAPI.Objects;
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

        [HttpPost("[action]/{traveller}/{starship}/{spaceportId}")]
        public async Task<IActionResult> AddParking(string traveller, string starship, int spaceportId)
        {
            //Added this line to Parse double values to not mix "." and ","
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Parking parkingObj = new()
            {
                Traveller = traveller,
                StarShip = starship,
                SpacePortId = spaceportId
            };

            //Fetch starwars characters from Swapi
            var personSwapi = Swapi.Fetch.People();
            await personSwapi;

            //Fetch starwars starships from Swapi
            var starshipSwapi = Swapi.Fetch.Starships();
            await starshipSwapi;

            //We have this variable set to 0 if we cant parse starshipLength variable below. 0 acts therefore as a null value.
            double result = 0;

            var travellerApiMatch = personSwapi.Result.Where(x => x.Name.ToLower() == parkingObj.Traveller.ToLower()).FirstOrDefault();
            if (travellerApiMatch != null)
            {
                var starships = starshipSwapi.Result.Join(travellerApiMatch.Starships, ssS => ssS.URL, tAM => tAM, (ssS, tAM) => ssS).ToList();
                var starshipApiMatch = starships.Where(x => x.Name.ToLower() == parkingObj.StarShip.ToLower()).FirstOrDefault();
                if (starshipApiMatch != null)
                {
                    var starshipLength = double.TryParse(starshipApiMatch.Length, out result);
                }

                //Check if traveller already has an active parking. if endtime == null then it is not active.
                var findActiveParking = _dbContext.Parkings.Any(x => x.Traveller == parkingObj.Traveller && x.EndTime == null);

                if (travellerApiMatch.Name != null && result < 15 && result > 0 && !findActiveParking)
                {
                    parkingObj.Traveller = parkingObj.Traveller.ToLower();
                    parkingObj.StarShip = parkingObj.StarShip.ToLower();
                    parkingObj.StartTime = DateTime.Now;
                    _dbContext.Parkings.Add(parkingObj);
                    _dbContext.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }

                if (findActiveParking)
                {
                    return BadRequest($"{parkingObj.Traveller} allready have an ongoing parking.");
                }
                else if (starshipApiMatch == null)
                {
                    return NotFound($"We can't find this starship registered under {parkingObj.Traveller}");
                }
                else if (result > 15)
                {
                    return BadRequest($"Starship too big: {result}, max length = 15m");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }

            //If we cant find any character match from the API
            else
            {
                return NotFound("Sorry, but we can't match you as a starwars character");
            }
        }

        [HttpPut]

        [HttpDelete("[action]")]
        public IActionResult DeleteParking(int id)
        {
            var parking = _dbContext.Parkings.Find(id);
            if (parking == null)
            {
                return NotFound("We cannot find any parking matching this ID.");
            }
            else
            {
                _dbContext.Parkings.Remove(parking);
                _dbContext.SaveChanges();
                return Ok("Parking deleted.");
            }
        }

    }
}

