using MediatR;

namespace School.Application.Handlers.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommand : IRequest
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }
    }
}
