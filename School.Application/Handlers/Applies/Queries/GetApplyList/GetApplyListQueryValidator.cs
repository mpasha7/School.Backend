using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Applies.Queries.GetApplyList
{
    public class GetApplyListQueryValidator : AbstractValidator<GetApplyListQuery>
    {
        public GetApplyListQueryValidator()
        {
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
