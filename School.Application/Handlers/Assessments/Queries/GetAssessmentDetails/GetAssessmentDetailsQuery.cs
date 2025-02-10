using MediatR;

namespace School.Application.Handlers.Assessments.Queries.GetAssessmentDetails
{
    public class GetAssessmentDetailsQuery : IRequest<AssessmentDetailsVm>
    {
        public string StudentGuid { get; set; }
        public int CourseId { get; set; }
    }
}
