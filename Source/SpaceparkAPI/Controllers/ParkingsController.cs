using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceparkAPI.Models;
using SpaceParkAPI.Data;
using System;
using System.Collections.Generic;
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
        public IActionResult AddParking([FromBody]Parking parkingObj)
        {
            parkingObj.StartTime = DateTime.Now;
            _dbContext.Parkings.Add(parkingObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
