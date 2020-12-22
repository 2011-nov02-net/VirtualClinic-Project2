using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Domain.Interfaces;

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
            catch
            {
                return NotFound();
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
