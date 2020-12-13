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
    public class PatientsController : ControllerBase
    {
        // GET: api/<PatientsController>
        /// <summary>
        /// Requires DR Level Authentication. Gets a list of all the DR's Patients.
        /// </summary>
        /// <param name="search">
        /// search term to filter names to only names containing this string.
        /// </param>
        /// <returns>Information on the patient, or error 403 not authorized.</returns>
        [HttpGet]
        public IEnumerable<string> Get([FromQuery] string search = null)
        {
            //check if logged in as dr, if not, then return not authorized

            //get all the patients of the currently logged in doctor.

            if(search is not null)
            {
                //filter by name based on search string

            }


            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientsController>/5
        /// <summary>
        /// Returns details of the specific patient. 
        /// Must either be this patient, or
        /// </summary>
        /// <param name="id">The id of the patient who's information is to be retrieved</param>
        /// <returns>Information on the patient, 404 not found, or 403 not authorized</returns>
        [HttpGet("{id}")]
        public string Get([FromRoute] int id)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user


            return "value";
        }


        [HttpGet("{Patientid}/Reports")]
        public string GetReports([FromRoute] int Patientid)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            return "value";
        }


        [HttpGet("{PatientID}/Reports/id")]
        public string GetReport([FromRoute] int PatientID, [FromRoute] int id)
        {
            // check if the patient with that id exists


            //if they exist, check authorization of the user

            //probably then forward this request to the reports controller
            return "value";
        }

        // POST api/<PatientsController>
        /// <summary>
        /// Adds the patient to the currently logged in dr's list of patients.
        /// Requires DR level authorization.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public void Put([FromRoute] int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
        }
    }
}
