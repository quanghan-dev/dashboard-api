using System.Text;
using Application.Models.Contacts;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("contacts")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
        {
            return Ok(await _contactService.CreateContact(request));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            return Ok(await _contactService.GetContactById(id));
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetContacts([FromQuery] string? keyword)
        {
            return Ok(await _contactService.GetContacts(keyword));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactById(Guid id, [FromBody] UpdateContactRequest request)
        {
            return Ok(await _contactService.UpdateContactById(id, request));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactById(Guid id)
        {
            return Ok(await _contactService.DeleteContactById(id));
        }

        [Authorize]
        [HttpPost("import")]
        public async Task<IActionResult> ImportContacts([FromForm] IFormFile file)
        {
            return Ok(await _contactService.ImportContacts(file));
        }

        [Authorize]
        [HttpGet("export")]
        public async Task<FileResult> ExportContacts()
        {
            string fileName = "contact.csv";
            byte[] content = Encoding.ASCII.GetBytes(await _contactService.ExportContacts());
            return File(content, "text/csv", fileName);
        }
    }
}