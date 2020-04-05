using System;

namespace DotNetCoreApiAuthenticationWithJWT.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

    }
}
