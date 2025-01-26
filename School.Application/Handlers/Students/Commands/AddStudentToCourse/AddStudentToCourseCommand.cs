using MediatR;

namespace School.Application.Handlers.Students.Commands.AddStudentToCourse
{
    public class AddStudentToCourseCommand : IRequest<int>
    {
        public string? CoachGuid { get; set; }

        public string? StudentGuid { get; set; }
        public int CourseId { get; set; }
    }
}
