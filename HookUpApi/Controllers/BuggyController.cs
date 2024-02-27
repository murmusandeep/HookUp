using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly IBuggyBLL _buggyBLL;

        public BuggyController(IBuggyBLL buggyBLL)
        {
            _buggyBLL = buggyBLL;
        }

        [Authorize]
        [HttpGet("auth")]
        public IActionResult GetSecret()
        {
            return Ok("Secret Text");
        }

        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var thing = await _buggyBLL.GetUser();
            if (thing == null) return NotFound();
            return Ok(thing);
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var thing = await _buggyBLL.GetUser();

            var thingToReturn = thing.ToString();
            return Ok(thingToReturn);
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
