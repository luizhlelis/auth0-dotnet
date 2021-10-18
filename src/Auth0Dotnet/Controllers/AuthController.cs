using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Auth0Dotnet.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("sign-in")]
        public async Task SignIn()
        {
            await HttpContext
                .ChallengeAsync(
                    "Auth0",
                    new AuthenticationProperties() { RedirectUri = _configuration["AuthorizationServer:RedirectUri"] }
                );

            _logger.LogInformation("SignIn was called");
        }

        //// GET api/auth/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/auth
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/auth/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            // If the user is authenticated, then this is how you can get the access_token and id_token
            if (User.Identity.IsAuthenticated)
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                // if you need to check the access token expiration time, use this value
                // provided on the authorization response and stored.
                // do not attempt to inspect/decode the access token
                DateTime accessTokenExpiresAt = DateTime.Parse(
                    await HttpContext.GetTokenAsync("expires_at"),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);

                string idToken = await HttpContext.GetTokenAsync("id_token");

                // Now you can use them. For more info on when and how to use the
                // access_token and id_token, see https://auth0.com/docs/tokens
            }

            return Ok("Welcome Bro!");
        }

        [HttpGet("sign-out")]
        public void SignOutOidc()
        {
        }

        //[Authorize]
        //[HttpGet("response-oidc")]
        //public void ResponseOidc()
        //{
        //    _logger.LogInformation("Callback was called");
        //}
    }
}
