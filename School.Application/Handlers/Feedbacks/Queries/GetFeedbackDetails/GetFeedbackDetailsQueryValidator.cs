using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Feedbacks.Queries.GetFeedbackDetails
{
    public class GetFeedbackDetailsQueryValidator : AbstractValidator<GetFeedbackDetailsQuery>
    {
        public GetFeedbackDetailsQueryValidator()
        {
            RuleFor(comm => comm.ReportId).GreaterThan(0);
            RuleFor(comm => comm.LessonId).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.StudentGuid).NotEmpty();
        }
    }
}
