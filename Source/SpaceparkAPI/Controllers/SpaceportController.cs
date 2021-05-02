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

        [HttpPost("[action]")]
        public IActionResult AddNewSpacePort([FromForm]SpacePort spaceportObj)
        {
            _dbContext.Add(spaceportObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("[action]")]
        public IActionResult GetSpacePorts()
        {
            var spaceports = from spaceport in _dbContext.SpacePorts
                           select new
                           {
                               Id = spaceport.Id,
                               Name = spaceport.Name
                           };
            return Ok(spaceports);
        }
    }
}
