using Application.Models.Accounts;
using Application.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AuthController(ILogger<AuthController> logger, IAccountService accountService,
        ITokenService tokenService)
        {
            _logger = logger;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost()]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
        {
            return Ok(await _accountService.RegisterAccount(request));
        }

        [HttpGet("activation")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string code)
        {
            return Ok(await _accountService.ActivateAccount(code));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAccount loginAccount)
        {
            return Ok(await _tokenService.CreateToken(_accountService.GetUserId(loginAccount)));
        }


    }
}
