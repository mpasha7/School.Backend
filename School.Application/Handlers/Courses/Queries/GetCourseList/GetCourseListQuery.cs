using MediatR;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQuery : IRequest<CourseListVm>
    {
        public string? UserGuid { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
