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
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IClinicRepository _clinicRepository;

        public PatientsController(ILogger<PatientsController> logger, IClinicRepository clinicRepository)
        {
            _logger = logger;
            _clinicRepository = clinicRepository;
        }
        // GET: api/<PatientsController>
        /// <summary>
        /// Requires DR Level Authentication. Gets a list of all the DR's Patients.
        /// </summary>
        /// <param name="search">
        /// search term to filter names to only names containing this string.
        /// </param>
        /// <returns>Information on the patient, or error 403 not authorized.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int doctorId, [FromQuery] string search = null)
        {
            //check if logged in as dr, if not, then return not authorized
            //get all the patients of the currently logged in doctor.
            if (await _clinicRepository.GetDoctorPatientsAsync(doctorId) is IEnumerable<Patient> patients)
            {

                if (search is not null)
                {
                    //filter by name based on search string
                    var searchPatient = patients.FirstOrDefault(p => p.Name.ToLower() == search.ToLower());

                    return Ok(searchPatient);

                }
                else
                {

                    return Ok(patients);
                }
           
            }
            else
            {

                return NotFound();
            }

        }

        // GET api/<PatientsController>/5
        /// <summary>
        /// Returns details of the specific patient. 
        /// Must either be this patient, or
        /// </summary>
        /// <param name="id">The id of the patient who's information is to be retrieved</param>
        /// <returns>Information on the patient, 404 not found, or 403 not authorized</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int patientId)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user
            if( await _clinicRepository.GetPatientByIDAsync(patientId) is Patient patient)
            {

                return Ok(patient);
            }
            else
            {

                return NotFound();
            }

        }
        //Get api/Patients/{id}/Prescriptions
        /// <summary>
        /// Get a list prescriptions for the specific patient
        /// </summary>
        /// <param name="PatientId">The id of the patients who's prescription is to be retrieved</param>
        /// <returns>Returns a list of reports for this patient.</returns>

        [HttpGet("{id}/Prescriptions")]
        public async Task<IActionResult> GetPresctiptions([FromRoute] int PatientId)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            if (await _clinicRepository.GetPatientPrescriptionsAsync(PatientId) is IEnumerable<Prescription> prescriptions)
            {

                return Ok(prescriptions);
            }
            else
            {

                return NotFound();
            }

        }
        //Get api/Patients/{id}/Reports
        /// <summary>
        /// Get a list reports for the specific patient
        /// </summary>
        /// <param name="PatientId">The id of the patients who's report is to be retrieved</param>
        /// <returns>Returns a list of reports for this patient.</returns>

        [HttpGet("{id}/Reports")]
        public async Task<IActionResult>  GetReports([FromRoute] int PatientId)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            if (await _clinicRepository.GetPatientReportsAsync(PatientId) is IEnumerable<PatientReport> reports)
            { 

                return Ok(reports);
            }
            else
            {

            return NotFound();
            }

        }

      

        [HttpGet("{PatientId}/Reports/id")]
        public async Task<IActionResult>  GetReport([FromRoute] int id)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            if (await _clinicRepository.GetPatientReportByIDAsync(id) is PatientReport report)
            {

                return Ok(report);
            }
            else
            {

                return NotFound();
            }
        }


        // POST api/<PatientsController>
        /// <summary>
        /// Adds the patient to the currently logged in dr's list of patients.
        /// Requires DR level authorization.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            if (await _clinicRepository.AddPatientAsync(patient))
            {
                return Ok();
            }
            else
            {

                return BadRequest("Request could not be processed.");
            }
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Patient newPatient)
        {
            var check = await _clinicRepository.GetPatientByIDAsync(id);

            if (check != null)
            {

                bool updated = await _clinicRepository.UpdatePatientAsync(id, newPatient);

                return Ok(updated);
            }
            else
            {
                return BadRequest("Request could not be processed.");
            }

        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            //todo delete patient by id method in repo async
        }
    }
}
