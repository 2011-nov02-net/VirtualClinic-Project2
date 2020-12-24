using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Domain.Interfaces;
using System.Security;
using System.Security.Claims;

namespace VirtualClinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IClinicRepository _repo;

        public AuthenticationController(ILogger<AuthenticationController> logger, IClinicRepository clinicRepository)
        {
            _logger = logger;
            _repo = clinicRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuth([FromQuery] string search_email)
        {
            try
            {
                var type = await _repo.GetAuthTypeAsync(search_email);
                return Ok(type);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                return NotFound();
            }
        }


        private static readonly string EmailKey = "email";
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutNewPatient()
        {
            IEnumerable<Claim> userClaims = HttpContext.User.Claims;
            Claim claim = userClaims.FirstOrDefault();

            if(claim is not null)
            {
                if (claim.Properties.ContainsKey(EmailKey))
                {
                    var newuser = await _repo.AddAuthorizedPatientAsync(claim.Properties[EmailKey]);

                    return CreatedAtAction(nameof(GetAuth), new { id = newuser.Id }, newuser);
                } else
                {
                    return this.UnprocessableEntity(claim);
                }
                
            } else
            {

                _logger.LogError("Could not get user claims.");
                return Unauthorized();
            }
        }
    }
}
