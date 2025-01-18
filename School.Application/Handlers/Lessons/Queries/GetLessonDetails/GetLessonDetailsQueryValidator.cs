using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class GetLessonDetailsQueryValidator : AbstractValidator<GetLessonDetailsQuery>
    {
        public GetLessonDetailsQueryValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
        }
    }
}
