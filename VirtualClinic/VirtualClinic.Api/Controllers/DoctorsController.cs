using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualClinic.Api.Controllers
{




    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(ILogger<DoctorsController> logger)
        {
            _logger = logger;
        }

        // GET: api/Doctors?search=searchString
        /// <summary>
        /// Gets a list of all doctors.
        /// </summary>
        /// <param name="search">
        /// optional search term to filter names to only names containing this string.
        /// </param>
        /// <returns>A list of all doctors.</returns>
        [HttpGet]
        public IEnumerable<string> Get([FromQuery] string search = null)
        {
            //if search isn't null, filter list of all doctors by name
            if(search is not null)
            {

            }
            return new string[] { "value1", "value2" };
        }

        // GET api/Doctors/5
        /// <summary>
        /// Get's the doctor with the given ID. Requires DR level authorization, or to be one of 
        /// the doctor's patients.
        /// </summary>
        /// <param name="id">The doctor's ID</param>
        /// <returns>Information about the Doctor, or 403 unauthorized, or 404 not found</returns>
        [HttpGet("{id}")]
        public string Get( [FromRoute] int id)
        {
            //try to find the dr by id, if not then 404 not found

            //if exists
            //  check if dr or one of the dr's patients for authorization.
            //  return unauthroirzed or the dr's data if authorized

            return "value";
        }

    }
}
