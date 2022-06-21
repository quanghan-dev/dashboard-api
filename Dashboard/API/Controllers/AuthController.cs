using System.Security.Claims;
using Application.Models.Accounts;
using Application.Models.Tokens;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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

        [Authorize]
        [HttpPut("logout")]
        public async Task<IActionResult> Logout()
        {
            string JWTtoken = Request.Headers[HeaderNames.Authorization];

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity!.Claims;

            //get user id from token
            string claimName = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.ToString();
            string userId = claimName[(claimName.LastIndexOf(':') + 2)..];

            return Ok(await _tokenService.Logout(Guid.Parse(userId), JWTtoken[7..]));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            return Ok(await _tokenService.RefreshToken(tokenDto));
        }
    }
}
