using MediatR;

namespace School.Application.Handlers.Reports.Queries.GetReportList
{
    public class GetReportListQuery : IRequest<ReportListVm>
    {
        public int CourseId { get; set; }
        public string CoachGuid { get; set; }
    }
}
