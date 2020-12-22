using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        // GET: api/<AppointmentsController>
        /// <summary>
        /// Gets a list of all apointments for the user. This list may be empty.
        /// </summary>
        /// <param name="after">
        /// Filter apointments to only be the ones after this date using Linq.
        /// </param>
        /// <returns>A list of all appointments.</returns>
        [HttpGet]
        public IEnumerable<string> Get([FromQuery] DateTime after)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppointmentsController>/5
        /// <summary>
        /// Gets the information about an Appointment.   
        /// </summary>
        /// <param name="id"> The Appointment's ID</param>
        /// <returns>The appointments information, 404 not found, or 403 unauthorized</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AppointmentsController>
        /// <summary>
        /// If patient, create an Appointment, if dr create an open timeslot?
        /// </summary>
        /// <returns>???</returns>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppointmentsController>/5
        /// <summary>
        /// updates an appointments details
        /// </summary>
        /// <param name="id">The ID of the Appointment</param>
        /// <param name="value">The changes to be made</param>
        /// <returns>
        /// 404 not found, 403 unauthorized or something to do with succsess
        /// </returns>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentsController>/5
        /// <summary>
        /// Cancels an Appointment freeing the timeslot.
        /// </summary>
        /// <param name="id">The id of the Appointment</param>
        /// <returns>
        /// OK, 404 not found, or 403 not authroized
        /// </returns>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
