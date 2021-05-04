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
    public class SpaceportController : ControllerBase
    {
        private SpaceParkContext _dbContext;
        public SpaceportController(SpaceParkContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public IActionResult GetParkingHistoryInSpaceport(int spaceportId)
        {
            var parkings = from parking in _dbContext.Parkings
                           join spaceport in _dbContext.SpacePorts on parking.SpacePortId equals spaceport.Id
                           where parking.EndTime != null
                           select new
                           {
                               Id = parking.Id,
                               spacePortId = spaceport.Id,
                               SpacePort = spaceport.Name,
                               Traveller = parking.Traveller,
                               Starship = parking.StarShip,
                               StartTime = parking.StartTime,
                               EndTime = parking.EndTime,
                               TotalSum = parking.TotalSum
                           };
            var spaceportById = parkings.Where(x => x.spacePortId == spaceportId);

            if (spaceportById == null || spaceportById.Count() == 0)
            {
                return NotFound("We can't find any spaceports matching this ID");
            }
            return Ok(parkings.Where(x => x.spacePortId == spaceportId).OrderBy(x => x.Traveller));
        }

        [HttpGet("[action]")]
        public IActionResult GetActiveParkingsInSpacePort(int spaceportId)
        {
            var parkings = from parking in _dbContext.Parkings
                           join spaceport in _dbContext.SpacePorts on parking.SpacePortId equals spaceport.Id
                           where parking.EndTime == null
                           select new
                           {
                               Id = parking.Id,
                               spacePortId = spaceport.Id,
                               SpacePort = spaceport.Name,
                               Traveller = parking.Traveller,
                               Starship = parking.StarShip,
                               StartTime = parking.StartTime
                           };
            var spaceportById = parkings.Where(x => x.spacePortId == spaceportId);

            if (spaceportById == null || spaceportById.Count() == 0)
            {
                return NotFound("We can't find any spaceports matching this ID");
            }
            return Ok(parkings.Where(x => x.spacePortId == spaceportId).OrderBy(x => x.StartTime));
        }

        [HttpGet("[action]")]
        public IActionResult GetAllSpacePorts()
        {
            var spaceports = from spaceport in _dbContext.SpacePorts
                             select new
                             {
                                 Id = spaceport.Id,
                                 Name = spaceport.Name
                             };
            return Ok(spaceports);
        }

        [HttpGet("[action]/{name}")]
        public IActionResult GetSpacePortByName(string name)
        {
            var findExistingSpacePort = _dbContext.SpacePorts.SingleOrDefault(x => x.Name == name);
            if (_dbContext.SpacePorts == null || findExistingSpacePort == null)
            {
                return BadRequest("We can't find the requested spaceport.");
            }

            var spaceports = (from spaceport in _dbContext.SpacePorts
                              where spaceport.Name == name
                              select new
                              {
                                  Id = spaceport.Id,
                                  Name = spaceport.Name
                              }).FirstOrDefault();
            return Ok(spaceports);
        }

        [HttpPost("[action]/{name}")]
        public IActionResult AddNewSpacePort(string name)
        {
            var spacePort = new SpacePort()
            {
                Name = name
            };

            var findExistingSpacePort = _dbContext.SpacePorts.Any(x => x.Name == name);
            if (findExistingSpacePort)
            {
                return BadRequest($"There is already and existing spaceport named: {name}, try with another one.");
            }

            _dbContext.Add(spacePort);
            _dbContext.SaveChanges();
            return Ok($"'{name}' spaceport has been created");
        }

        [HttpPut("[action]/{name}")]
        public IActionResult UpdateSpacePort(string name, string newName)
        {
            var findSpacePort = _dbContext.SpacePorts.SingleOrDefault(x => x.Name == name);

            if (findSpacePort != null)
            {
                findSpacePort.Name = newName;
                _dbContext.SaveChanges();
                return Ok($"Spaceport name updated to {newName}");
            }
            else
            {
                return NotFound($"We can't find any spaceports named: {name}");
            }

        }

        [HttpDelete("[action]")]
        public IActionResult DeleteSpaceport(int id)
        {
            var spaceport = _dbContext.SpacePorts.Find(id);
            if (spaceport == null)
            {
                return NotFound("We cannot find any parking matching this ID.");
            }
            else
            {
                _dbContext.SpacePorts.Remove(spaceport);
                _dbContext.SaveChanges();
                return Ok("Spaceport deleted with all its historical and active parkings.");
            }
        }
    }
}
