using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Assessments.Queries.GetAssessmentDetails
{
    public class GetAssessmentDetailsQueryValidator : AbstractValidator<GetAssessmentDetailsQuery>
    {
        public GetAssessmentDetailsQueryValidator()
        {
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
