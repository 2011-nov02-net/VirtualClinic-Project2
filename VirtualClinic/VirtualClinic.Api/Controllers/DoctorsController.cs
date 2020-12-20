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
    public class DoctorsController : ControllerBase
    {
        private readonly ILogger<DoctorsController> _logger;
        private readonly IClinicRepository _clinicRepository;

        public DoctorsController(ILogger<DoctorsController> logger, IClinicRepository clinicRepository)
        {
            _logger = logger;
            _clinicRepository = clinicRepository;
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
        public async Task<IActionResult> Get([FromQuery] string search = null)
        {
            if (await _clinicRepository.GetDoctorsAsync() is IEnumerable<Doctor> doctors)
            {
                //if search isn't null, filter list of all doctors by name
                if (search is not null)
                {
                    var searchDoctor = doctors.FirstOrDefault(d => d.Name.ToLower() == search.ToLower());

                    return Ok(searchDoctor);
                }
                else
                {

                    return Ok(doctors);
                }

            }
            else
            {

                return NotFound();
            }

        }

        // GET api/Doctors/5
        /// <summary>
        /// Get's the doctor with the given ID. Requires DR level authorization, or to be one of 
        /// the doctor's patients.
        /// </summary>
        /// <param name="id">The doctor's ID</param>
        /// <returns>Information about the Doctor, or 403 unauthorized, or 404 not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get( [FromRoute] int id)
        {
            //try to find the dr by id, if not then 404 not found

            //if exists
            //  check if dr or one of the dr's patients for authorization.
            //  return unauthroirzed or the dr's data if authorized

        if ( await _clinicRepository.GetDoctorByIDAsync(id) is Doctor doctor)
            {

                return Ok(doctor);

            }
        else
            {

                return NotFound();
            }
        }


        //PUT: api/Doctors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int doctorId, [FromBody] Doctor doctor)
        {
            if (await _clinicRepository.GetDoctorByIDAsync(doctorId) is Doctor)
            {
                // _clinicRepository.UpdateDoctorAsync(doctor);

                return NoContent();
            }


            return NotFound();
        }

        //GET: api/Doctors/{id}/Patients
        /// <summary>
        /// Requires DR Level Authentication. Gets a list of all the DR's Patients.
        /// </summary>
        /// <param name="search">
        /// search term to filter names to only names containing this string.
        /// </param>
        /// <returns>Information on the patient, or error 403 not authorized.</returns>
        [HttpGet("{id}/Patients")]
        public async Task<IActionResult> Get([FromQuery] int id, [FromQuery] string search = null)
        {
            //check if logged in as dr, if not, then return not authorized
            //get all the patients of the currently logged in doctor.


            if (await _clinicRepository.GetDoctorPatientsAsync(id) is IEnumerable<Patient> patients)
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
    }
}
