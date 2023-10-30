using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        [HttpGet(template: "getServerTime")]
        public ActionResult GetServerTime()
        {
            return Ok(new { Date = DateTime.Now });
        }

        [HttpGet(template: "getServerVersion")]
        public ActionResult GetServerVersion([FromServices] IConfiguration configuration)
        {
            return Ok(new { Version = configuration["Version"]! });
        }

        [HttpGet(template: "getAll")]
        public ActionResult GetServerAll([FromServices] IConfiguration configuration)
        {
            return Ok(new 
            {
                Date = DateTime.Now,
                Version = configuration["Version"]! 
            });
        }
    }
}
