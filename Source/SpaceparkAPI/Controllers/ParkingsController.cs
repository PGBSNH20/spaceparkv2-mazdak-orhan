using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("[action]/{traveller}")]
        public IActionResult GetTravellerHistoricalParkings(string traveller)
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
                               EndTime = parking.EndTime,
                               Totalsum = parking.TotalSum
                           };
            var historicalParking = parkings.Where(x => x.Traveller == traveller.ToLower());

            if (historicalParking == null || historicalParking.Count() == 0)
            {
                return NotFound($"We can't find any historical parkings for {traveller.ToLower()}");
            }
            return Ok(parkings.Where(x => x.Traveller == traveller.ToLower()));
        }

        [HttpGet("[action]/{traveller}")]
        public async Task<IActionResult> GetTravellerActiveParking(string traveller)
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
            var ongoingParking = parkings.Where(x => x.Traveller == traveller.ToLower());

            if(ongoingParking == null || ongoingParking.Count() == 0)
            {
                return NotFound($"We can't find any active parkings for {traveller.ToLower()}");
            }
            return Ok(await parkings.Where(x => x.Traveller == traveller.ToLower()).FirstOrDefaultAsync());
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
            var personSwapi = await Swapi.Fetch.People();

            //Fetch starwars starships from Swapi
            var starshipSwapi = await Swapi.Fetch.Starships();

            //We have this variable set to 0 if we cant parse starshipLength variable below. 0 acts therefore as a null value.
            double result = 0;

            var travellerApiMatch = personSwapi.Where(x => x.Name.ToLower() == parkingObj.Traveller.ToLower()).FirstOrDefault();
            if (travellerApiMatch != null)
            {
                var starships = starshipSwapi.Join(travellerApiMatch.Starships, ssS => ssS.URL, tAM => tAM, (ssS, tAM) => ssS).ToList();
                var starshipApiMatch = starships.Where(x => x.Name.ToLower() == parkingObj.StarShip.ToLower()).FirstOrDefault();
                if (starshipApiMatch != null)
                {
                    var starshipLength = double.TryParse(starshipApiMatch.Length, out result);
                }

                //Check if traveller already has an active parking. if endtime == null then it is not active.
                var findActiveParking = await _dbContext.Parkings.AnyAsync(x => x.Traveller == parkingObj.Traveller && x.EndTime == null);

                if (travellerApiMatch.Name != null && result < 15 && result > 0 && !findActiveParking)
                {
                    parkingObj.Traveller = parkingObj.Traveller.ToLower();
                    parkingObj.StarShip = parkingObj.StarShip.ToLower();
                    parkingObj.StartTime = DateTime.Now;
                    await _dbContext.Parkings.AddAsync(parkingObj);
                    await _dbContext.SaveChangesAsync();
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

        [HttpPut("[action]/{traveller}")]
        public async Task<IActionResult> EndParking(string traveller)
        {
            var findActiveParking = await _dbContext.Parkings.SingleOrDefaultAsync(x => x.Traveller == traveller && x.EndTime == null);

            if(findActiveParking != null)
            {
                findActiveParking.EndTime = DateTime.Now;
                var duration = findActiveParking.EndTime - findActiveParking.StartTime;
                findActiveParking.TotalSum = Math.Round(Convert.ToDecimal(duration.Value.TotalMinutes) * 10m, 2);
                await _dbContext.SaveChangesAsync();
                return Ok($"Parking ended. Total cost: {findActiveParking.TotalSum}");
            }
            return NotFound($"We can't find any active parkings for {traveller}");
        } 

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteParking(int id)
        {
            var parking = await _dbContext.Parkings.FindAsync(id);
            if (parking == null)
            {
                return NotFound("We cannot find any parking matching this ID.");
            }
            else
            {
                _dbContext.Parkings.Remove(parking);
                await _dbContext.SaveChangesAsync();
                return Ok("Parking deleted.");
            }
        }

    }
}

