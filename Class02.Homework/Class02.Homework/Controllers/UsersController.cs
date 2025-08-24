using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Class02.Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> GetAll()
        {
            return Ok(StaticDb.Users);
        }
        [HttpGet("{index}")]
        public ActionResult<string> GetUser(int index)
        {
            if(index == null)
            {
                return BadRequest("User index cannot be null");
            }
            if(index < 0)
            {
                return BadRequest("User index cannot be negative number");
            }
            if(index >= StaticDb.Users.Count)
            {
                return NotFound($"There is no user with index {index}");
            }
            return Ok(StaticDb.Users[index]);
        }
    }

}
