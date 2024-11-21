using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class CourseListVm
    {
        public IList<CourseLookupDto> Courses { get; set; } = new List<CourseLookupDto>();
    }
}
