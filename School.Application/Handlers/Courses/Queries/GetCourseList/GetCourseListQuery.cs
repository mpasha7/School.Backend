using MediatR;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQuery : IRequest<CourseListVm>
    {
        public string? CoachGuid { get; set; }
    }
}
