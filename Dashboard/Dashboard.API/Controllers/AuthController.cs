using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers {
  [EnableCors("MyPolicy")]
  [Route("[controller]")]
  public class AuthController : Controller {
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger) {
      _logger = logger;
    }

    [HttpPost]
    public IActionResult Index() {
      return Ok();
    }
  }
}
