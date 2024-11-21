using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQuery : IRequest<CourseDetailsVm>
    {
        public int Id { get; set; }
        public string? CoachGuid { get; set; }
    }
}
