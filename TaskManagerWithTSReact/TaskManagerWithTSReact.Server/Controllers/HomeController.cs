using Microsoft.AspNetCore.Mvc;

namespace TaskManagerWithTSReact.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("gethello")]
        public string Get()
        {
            return "hello";
        }
    }
}
