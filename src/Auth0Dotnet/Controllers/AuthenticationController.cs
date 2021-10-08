using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Auth0Dotnet.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("sign-in")]
        /// <summary>
        /// Sign in to the application (starts the authorization code flow).
        /// </summary>
        /// <param name="id"></param>
        public static void SignIn()
        {
            return;
        }

        [HttpGet]
        [Route("response-oidc")]
        public static void ResponseOidc()
        {
            return;
        }

        [HttpGet]
        [Route("sign-out")]
        public override SignOutResult SignOut()
        {
            return new SignOutResult();
        }

        [HttpGet]
        [Route("me")]
        public static void Me()
        {
            return;
        }
    }
}
