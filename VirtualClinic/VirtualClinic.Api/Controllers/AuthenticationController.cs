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
        [Authorize]
        public async Task<IActionResult> GetAuth()
        {
            ClaimsPrincipal userClaims = HttpContext.User;

            string search_email = userClaims.FindFirstValue(EmailClaimType);

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


        private static readonly string EmailClaimType = "sub";
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutNewPatient()
        {
            ClaimsPrincipal userClaims = HttpContext.User;

            string email = userClaims.FindFirstValue(EmailClaimType);

            if (! string.IsNullOrEmpty(email))
            {
                var newuser = await _repo.AddAuthorizedPatientAsync(email);

                return CreatedAtAction(nameof(GetAuth), new { id = newuser.Id }, newuser);

            } else
            {

                _logger.LogError("Could not get user claims.");
                return Ok();
            }
        }
    }
}
