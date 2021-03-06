﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Domain.Repositories;
using VirtualClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Domain.Models;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] string search = null)
        {
            //check if logged in as dr, if not, then return not authorized
            //get all the patients

            if (await _clinicRepository.GetPatientsAsync() is IEnumerable<Patient> patients)
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
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user
            if( await _clinicRepository.GetPatientByIDAsync(id) is Patient patient)
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
        public async Task<IActionResult> GetPresctiptions([FromRoute] int id)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            if (await _clinicRepository.GetPatientPrescriptionsAsync(id) is IEnumerable<Prescription> prescriptions)
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
        public async Task<IActionResult>  GetReports([FromRoute] int id)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            if (await _clinicRepository.GetPatientReportsAsync(id) is IEnumerable<PatientReport> reports)
            { 

                return Ok(reports);
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
            var new_patient = await _clinicRepository.AddPatientAsync(patient);
            if (new_patient is Domain.Models.Patient)
            {
                return CreatedAtAction(nameof(Get), new { id = patient.Id }, new_patient);
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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var patient = await _clinicRepository.GetPatientByIDAsync(id);

            if (patient != null)
            {

                try
                {
                    await _clinicRepository.DeletePatientAsync(id);

                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
           
        }
    }
}
