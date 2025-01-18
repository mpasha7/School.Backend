using MediatR;

namespace School.Application.Handlers.Courses.Queries.PublicCourseDetails
{
    public class PublicCourseDetailsQuery : IRequest<PublicCourseDetailsVm>
    {
        public int Id { get; set; }
    }
}
