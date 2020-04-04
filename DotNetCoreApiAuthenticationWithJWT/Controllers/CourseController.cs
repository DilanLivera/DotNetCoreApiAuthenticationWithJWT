using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreApiAuthenticationWithJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApiAuthenticationWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        // GET: api/Course
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return new List<Course>
            {
                new Course
                {
                    CourseId = 1,
                    Name = ".Net Core API authentication with JSON Web Token.",
                    Description = "How to use JWT to authenticate api requests in .Net Core.",
                    StartDate = DateTime.UtcNow
                }
            };
        }

        // POST: api/Course
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            return Ok();
        }

    }
}
