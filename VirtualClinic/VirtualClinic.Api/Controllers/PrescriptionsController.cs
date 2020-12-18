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
    public class PrescriptionsController : ControllerBase
    {

        private readonly ILogger<PrescriptionsController> _logger;
        private readonly IClinicRepository _clinicRepository;

        public PrescriptionsController(ILogger<PrescriptionsController> logger, IClinicRepository clinicRepository)
        {
            _logger = logger;
            _clinicRepository = clinicRepository;
        }
        // GET: api/Prescriptions
        /// <summary>
        /// Get all prescriptions for the logged in user.
        /// </summary>
        /// <param name="search">
        /// Optional argument to filter prescription by the patient name
        /// </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int patientId,[FromQuery] string search = null)
        {
            //if logged in user is a DR, get all prescritpions for this doctor


            //else if user is a patient, get all prescriptions for this patient
            if (await _clinicRepository.GetPatientPrescriptionsAsync(patientId) is IEnumerable<Prescription> prescriptions)
            {
                return Ok(prescriptions);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/Prescription/5
        /// <summary>
        /// Get details of this specific Prescription
        /// </summary>
        /// <param name="id">The id of the prescription to be retrieved </param>
        /// <returns>Information on the prescription, 404 not found or 403 not auhtprized</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (await _clinicRepository.GetPrescriptionAsync(id) is Prescription prescription)
            {
                return Ok(prescription);
            }
            else
            {
                return NotFound();
            }
        }


        // POST api/Prescriptions
        /// <summary>
        /// Requires DR Level Authentication. Creates a new Prescrition
        /// </summary>
        /// <param name="value">The details of the Prescription to be created</param>
        /// <returns>
        /// Either forbidden, or Ok 201
        /// </returns>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] Prescription prescription)
        {
            //If user is a DR, create a new prescription for a patient

            //If user is a patient, return 403 forbidden
            if (await _clinicRepository.AddPrescriptionAsync(prescription))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // PUT api/Prescriptions/5
        /// <summary>
        /// Requires DR Level Authentication. Edit the details of prescription.
        /// </summary>
        /// <param name="id">The Id of the Prescription to be edited</param>
        /// <param name="value">The edits to make</param>
        /// <returns>
        /// Ok + objects details after change, 403 not authroized or 404 not found
        /// </returns>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //If user is a DR, edit this prescription

            //If user is a patient, return 403 forbidden

            //todo update patient by id method in repo async

        }

        // DELETE api/Prescriptions/5
        /// <summary>
        /// Requires DR Level Authentication. Deletes the prescription if it exits.
        /// </summary>
        /// <param name="id">The id of the prescription to delete</param>
        /// <returns>
        /// Returns forbidden if patient or Ok if DR.
        /// </returns>
        [HttpDelete("{id}")]
        public void Delete([FromRoute]int id)
        {
            //If user is a DR, delete this prescription

            //If user is a patient, return 403 forbidden

            //todo delete prescription by id method in repo async
        }
    }
}
