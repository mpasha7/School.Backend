using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Messages.Queries.GetMessageList
{
    public class GetMessageListQueryValidator : AbstractValidator<GetMessageListQuery>
    {
        public GetMessageListQueryValidator()
        {
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.UserGuid).NotEmpty();
        }
    }
}
