using MediatR;

namespace School.Application.Handlers.Feedbacks.Queries.GetFeedbackDetails
{
    public class GetFeedbackDetailsQuery : IRequest<FeedbackDetailsVm>
    {
        public int ReportId { get; set; }
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public string StudentGuid { get; set; }
    }
}
