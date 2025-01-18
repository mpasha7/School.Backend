using MediatR;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQuery : IRequest<CourseDetailsVm>
    {
        public int Id { get; set; }
        public string? CoachGuid { get; set; }
    }
}
