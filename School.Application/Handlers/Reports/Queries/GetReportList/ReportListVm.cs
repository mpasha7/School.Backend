namespace School.Application.Handlers.Reports.Queries.GetReportList
{
    public class ReportListVm
    {
        public IList<ReportLookupDto> Reports { get; set; } = new List<ReportLookupDto>();
        public int MaxLessonNumber { get; set; }
    }
}
