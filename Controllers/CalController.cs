using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalController : ControllerBase
    {
        [HttpGet("sum")]
        public IActionResult Sum(int a, int b)
        {
            int result = a + b;
            return Ok(result);
        }
    }
}