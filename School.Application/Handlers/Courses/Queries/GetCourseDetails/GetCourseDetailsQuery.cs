using MediatR;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQuery : IRequest<CourseDetailsVm>
    {
        public int Id { get; set; }
        public string? UserGuid { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
