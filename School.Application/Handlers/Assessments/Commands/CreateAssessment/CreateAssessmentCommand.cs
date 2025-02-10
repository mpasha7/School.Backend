using MediatR;

namespace School.Application.Handlers.Assessments.Commands.CreateAssessment
{
    public class CreateAssessmentCommand : IRequest<int>
    {
        public string CoachGuid { get; set; }

        public string StudentGuid { get; set; }
        public string Text { get; set; }
        public int CourseId { get; set; }
    }
}
