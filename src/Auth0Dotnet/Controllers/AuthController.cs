using Microsoft.AspNetCore.Mvc;

namespace Auth0Dotnet.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("sign-in")]
        public IActionResult SignIn()
        {
            return Ok("Boa! Conseguiu");
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

        // DELETE api/auth/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
