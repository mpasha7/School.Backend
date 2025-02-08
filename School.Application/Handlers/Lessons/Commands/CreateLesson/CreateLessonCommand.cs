using MediatR;

namespace School.Application.Handlers.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommand : IRequest<int>
    {
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }

        public int? Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }
    }
}
