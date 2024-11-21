using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQuery : IRequest<CourseListVm>
    {
        public string? CoachGuid { get; set; }
    }
}
