using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceparkAPI.Attributes;
using SpaceparkAPI.Models;
using SpaceParkAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAdmin] //See appsettings.json for api key 
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
            if (_dbContext.SpacePorts == null || _dbContext.SpacePorts.Count() == 0)
            {
                return NotFound("We can't find any currently active spaceports.");
            }
            var spaceports = from spaceport in _dbContext.SpacePorts
                             select new
                             {
                                 Id = spaceport.Id,
                                 Name = spaceport.Name
                             };
            return Ok(spaceports);
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetSpacePortByName(string name)
        {
            var findExistingSpacePort = await _dbContext.SpacePorts.SingleOrDefaultAsync(x => x.Name == name);
            if (_dbContext.SpacePorts == null || findExistingSpacePort == null)
            {
                return BadRequest("We can't find the requested spaceport.");
            }

            var spaceports = await (from spaceport in _dbContext.SpacePorts
                                    where spaceport.Name == name
                                    select new
                                    {
                                        Id = spaceport.Id,
                                        Name = spaceport.Name
                                    }).FirstOrDefaultAsync();
            return Ok(spaceports);
        }

        [HttpPost("[action]/{name}")]
        public async Task<IActionResult> AddNewSpacePort(string name)
        {
            var spacePort = new SpacePort()
            {
                Name = name
            };

            var findExistingSpacePort = await _dbContext.SpacePorts.AnyAsync(x => x.Name == name);
            if (findExistingSpacePort || name == null)
            {
                return BadRequest($"There is already and existing spaceport named: {name}, try with another one.");
            }

            await _dbContext.AddAsync(spacePort);
            await _dbContext.SaveChangesAsync();
            return Ok($"'{name}' spaceport has been created");
        }

        [HttpPut("[action]/{name}")]
        public async Task<IActionResult> UpdateSpacePort(string name, string newName)
        {
            var findSpacePort = await _dbContext.SpacePorts.SingleOrDefaultAsync(x => x.Name == name);

            if (name.ToLower() == newName.ToLower())
            {
                return BadRequest($"Spaceport already have the name {newName}");
            }
            else if (findSpacePort != null)
            {
                findSpacePort.Name = newName;
                await _dbContext.SaveChangesAsync();
                return Ok($"Spaceport name updated to {newName}");
            }
            else
            {
                return NotFound($"We can't find any spaceports named: {name}");
            }

        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteSpaceport(int id)
        {
            var spaceport = await _dbContext.SpacePorts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (spaceport == null)
            {
                return NotFound("We cannot find any parking matching this ID.");
            }
            _dbContext.SpacePorts.Remove(spaceport);
            await _dbContext.SaveChangesAsync();
            return Ok($"'{spaceport.Name}' Spaceport deleted with all its historical and active parkings.");
        }

        [HttpDelete("[action]/{ParkingID}")]
        public async Task<IActionResult> DeleteParking(int id)
        {
            var parking = await _dbContext.Parkings.Where(x => x.Id == id).FirstOrDefaultAsync();
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
