using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Domain.Interfaces;
using VirtualClinic.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {

        private readonly ILogger<AppointmentsController> _logger;
        private readonly IClinicRepository _ApointmentRepo;

        public AppointmentsController(ILogger<AppointmentsController> logger, IClinicRepository clinicRepository)
        {
            _logger = logger;
            _ApointmentRepo = clinicRepository;
        }




        // GET: api/Apointment?after=datetime/
        /// <summary>
        /// Gets a list of all apointments for the user. This list may be empty.
        /// </summary>
        /// <param name="after">
        /// Filter apointments to only be the ones after this date using Linq.
        /// </param>
        /// <returns>A list of all appointments.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] DateTime? after = null)
        {
            //todo: authentication

            //todo: get user id and of dr or patient via auth
            bool isDr = true;
            int id = -1;

            Task<IEnumerable<Timeslot>> apointmentsTask;
            if (isDr)
            {
                apointmentsTask = _ApointmentRepo.GetDoctorTimeslotsAsync(id);
            } else
            {
                apointmentsTask = _ApointmentRepo.GetPatientTimeslotsAsync(id);
            }

            IEnumerable<Timeslot> apointments;

            // check for if the person's ID cannot be found.
            // see https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/exception-handling-task-parallel-library#attached-child-tasks-and-nested-aggregateexceptions
            try
            {
                apointments = await apointmentsTask;
            } catch(AggregateException e)
            {
                var exception = e.Flatten();

                if(exception.InnerExceptions is not null && exception.InnerException is ArgumentException)
                {
                    _logger.LogError(exception.InnerException.Message);
                    _logger.LogError(e.StackTrace);

                    return NotFound();

                } else
                {
                    //don't want to catch other kinds
                    _logger.LogError(e.StackTrace);
                    throw e.InnerException;
                }
            }

            if(after != null)
            {
                apointments = apointments.Where(apoint => apoint.Start > after);
            }

            return Ok(apointments.ToList());
        }

        // GET api/<AppointmentsController>/5
        /// <summary>
        /// Gets the information about an Appointment.   
        /// </summary>
        /// <param name="id"> The Appointment's ID</param>
        /// <returns>The appointments information, 404 not found, or 403 unauthorized</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //todo: get the specific apointment.
            Timeslot timeslot = null;

            //await the result + check for error

            //TODO: check authorization before rturning
            if (true)
            {
                return Ok(timeslot);
            }
            else
            {
                return Forbid();
            }
        }

        // POST api/<AppointmentsController>
        /// <summary>
        /// If patient, create an Appointment, if dr create an open timeslot?
        /// </summary>
        /// <returns>403 unauthorized, some other error for id collision, or CreatedAt</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Domain.Models.Timeslot value)
        {
            //check authorization
            Task<Domain.Models.Timeslot> apointmentTask = _ApointmentRepo.AddTimeslotAsync(value);

            //try catch for errors
            Timeslot createdApointment = await apointmentTask;

            //TODO: replace with ID
            return CreatedAtAction(nameof(Get), createdApointment);
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
        public IActionResult Put([FromRoute]int id, [FromBody] string value)
        {

            //get the one with the id

            //check auth

            //replace stuff with new values

            //update in DB

            throw new NotImplementedException("Not Yet Implemented");
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
        [ProducesResponseType(403, Type = typeof(IActionResult))]
        public IActionResult Delete(int id)
        {
            //tood: check auth
            bool authorized = false;

            if (authorized)
            {
                //todo: create a delete method in repo.
                _logger.LogInformation($"Tried to delete Apointment/Timeslot {id}");
                
                return Ok(); 
            } else
            {
                return Forbid();
            }
        }
    }
}
