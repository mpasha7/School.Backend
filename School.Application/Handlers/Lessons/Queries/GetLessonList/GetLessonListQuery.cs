using MediatR;
using School.Domain;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class GetLessonListQuery : IRequest<LessonListVm>
    {
        public int CourseId { get; set; }
        public string? UserGuid { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
