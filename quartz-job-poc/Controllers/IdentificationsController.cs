using Microsoft.AspNetCore.Mvc;
using quartz_job_poc.Models;
using quartz_job_poc.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace quartz_job_poc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentificationsController : ControllerBase
    {
        private readonly IdentificationService _identificationService;

        public IdentificationsController(IdentificationService identificationService)
        {
            _identificationService = identificationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Identification>>> Get()
        {
            var identifications = await _identificationService.GetAllAsync();
            return Ok(identifications);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Identification>> Get(string id)
        {
            var identification = await _identificationService.GetByIdAsync(id);

            if (identification == null)
                return NotFound();

            return Ok(identification);
        }

        [HttpPost]
        public async Task<ActionResult<Identification>> Create([FromBody] CreateIdentificationRequest request)
        {
            var identification = await _identificationService.CreateAsync(request.Name);
            return CreatedAtAction(nameof(Get), new { id = identification.Id }, identification);
        }
    }

    public class CreateIdentificationRequest
    {
        public string Name { get; set; }
    }
} 