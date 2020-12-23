using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Domain.Repositories;
using VirtualClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly ILogger<ReportsController> _logger;
        private readonly IClinicRepository _clinicRepository;

        public ReportsController(ILogger<ReportsController> logger, IClinicRepository clinicRepository)
        {
            _logger = logger;
            _clinicRepository = clinicRepository;
        }
        // GET: api/<ReportsController>
        /// <summary>
        /// Get all reports for the logged in Patient.
        /// </summary>
        /// <param name="afterDate">
        /// Optional argument to filter reports to only ones from apointments after the given date.
        /// </param>
        /// <returns>The report details, or 403 Forbidden if unauthorized </returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int PatientId, [FromQuery] DateTime? afterDate = null)
        {
            if (await _clinicRepository.GetPatientReportsAsync(PatientId) is IEnumerable<PatientReport> reports)
            {

                return Ok(reports);
            }
            else
            {

                return NotFound();
            }
        }

        // GET api/<ReportsController>/5
        /// <summary>
        /// Get the details of this specific report
        /// </summary>
        /// <param name="id">The apointment who's details are to be retrieved</param>
        /// <returns>The details of report, 403 forbidden if not authorized, or 404 not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (await _clinicRepository.GetPatientReportByIDAsync(id) is PatientReport report)
            {

                return Ok(report);
            }
            else
            {

                return NotFound();
            }
        }
    

        // POST api/<ReportsController>
        /// <summary>
        /// Create a new report
        /// </summary>
        /// <param name="value">The details of the report to be created</param>
        /// <returns>
        /// Either forbidden, or OK?
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientReport report)
        {
            //figure out if the user has permission to accsess the apointment the report will
            // be associated with, and if not return 403 forbidden

            //if they do, create the report with the given details.

            if(await _clinicRepository.AddPatientReportAsync(report) is Domain.Models.PatientReport)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Request could not be processed.");
            }
        }

        // PUT api/<ReportsController>/5
        /// <summary>
        /// Edit the details of the reprot.
        /// </summary>
        /// <param name="id">The ID of the report to be edited.</param>
        /// <param name="value">The edits to make</param>
        /// <returns>
        /// OK + object details after changes, 403 not authorized, or 404 not found.
        /// </returns>
        [HttpPut("{id}")]
        public void Put([FromRoute]int id, [FromBody] string value)
        {

        }

        // DELETE api/<ReportsController>/5
        /// <summary>
        /// Deletes the report if it exists, and the user is allowed to?
        /// </summary>
        /// <param name="id">The id of the apointment to delete</param>
        /// <returns>
        /// Either forbidden, not found, or OK
        /// </returns>
        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
        }
    }
}
