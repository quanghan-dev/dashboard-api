using System.Security.Claims;
using Application.Models.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace API.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _taskService.CreateTask(userId, request));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _taskService.GetTaskById(userId, id));
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetTasks([FromQuery] string? keyword)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _taskService.GetTasks(userId, keyword));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskById(Guid id, [FromBody] TaskRequest request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _taskService.UpdateTaskById(userId, id, request));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById(Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _taskService.DeleteTaskById(userId, id));
        }
    }
}