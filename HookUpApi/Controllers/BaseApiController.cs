using HookUpApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
