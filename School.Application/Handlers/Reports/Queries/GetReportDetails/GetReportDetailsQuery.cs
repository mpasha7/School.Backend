using MediatR;

namespace School.Application.Handlers.Reports.Queries.GetReportDetails
{
    public class GetReportDetailsQuery : IRequest<ReportDetailsVm>
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public string CoachGuid { get; set; }
    }
}
