using MediatR;

namespace School.Application.Handlers.Feedbacks.Commands.CreateFeedback
{
    public class CreateFeedbackCommand : IRequest<int>
    {
        public string CoachGuid { get; set; }

        public string Text { get; set; }

        public int ReportId { get; set; }
        public int LessonId { get; set; }
        public int CourseId { get; set; }
    }
}
