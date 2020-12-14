using System;
using System.Collections.Generic;
using VirtualClinic.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerscriptionsController : ControllerBase
    {
        // GET: api/Prescriptions
        /// <summary>
        /// Get all prescriptions for the logged in user.
        /// </summary>
        /// <param name="search">
        /// Optional argument to filter prescription by the patient name
        /// </param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get([FromQuery] string search = null)
        {
            //if logged in user is a DR, get all prescritpions for this doctor


            //else if user is a patient, get all prescriptions for this patient
            return new string[] { "value1", "value2" };
        }

        // GET api/Prescription/5
        /// <summary>
        /// Get details of this specific Prescription
        /// </summary>
        /// <param name="id">The id of the prescription to be retrieved </param>
        /// <returns>Information on the prescription, 404 not found or 403 not auhtprized</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public void Post([FromBody] Prescription prescription)
        {
            //If user is a DR, create a new prescription for a patient

            //If user is a patient, return 403 forbidden
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
        }
    }
}
