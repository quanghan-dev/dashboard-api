using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Models.Dashboards;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("dashboards")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetDashboards()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _dashboardService.GetDashboards(Guid.Parse(userId)));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDashboardById(Guid id, [FromBody] UpdateDashboardRequest request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _dashboardService.UpdateDashboardById(Guid.Parse(userId), id, request));
        }
    }
}