using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryValidator : AbstractValidator<GetCourseDetailsQuery>
    {
        public GetCourseDetailsQueryValidator()
        {
            RuleFor(q => q.Id).GreaterThan(0);
            //RuleFor(q => q.CoachGuid).NotEmpty();
        }
    }
}
