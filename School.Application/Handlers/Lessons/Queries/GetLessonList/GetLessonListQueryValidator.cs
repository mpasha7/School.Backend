using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class GetLessonListQueryValidator : AbstractValidator<GetLessonListQuery>
    {
        public GetLessonListQueryValidator()
        {
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.UserGuid).NotEmpty();
        }
    }
}
