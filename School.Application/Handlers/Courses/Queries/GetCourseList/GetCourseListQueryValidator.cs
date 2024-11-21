using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryValidator : AbstractValidator<GetCourseListQuery>
    {
        public GetCourseListQueryValidator()
        {
            //RuleFor(q => q.CoachGuid).NotEmpty();
        }
    }
}
