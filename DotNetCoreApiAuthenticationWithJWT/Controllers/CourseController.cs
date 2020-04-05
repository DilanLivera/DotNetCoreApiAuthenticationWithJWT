using System;
using System.Collections.Generic;
using DotNetCoreApiAuthenticationWithJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApiAuthenticationWithJWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public CourseController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }

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
                    StartDate = DateTime.Now
                }
            };
        }

        // POST: api/Course
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            string token = _jwtAuthenticationManager.Authenticate(user.UserName, user.Password);

            if(token is null)
            {
                return Unauthorized("Username or Password is not valid. Please try again.");
            }

            return Ok(token);
        }

    }
}
