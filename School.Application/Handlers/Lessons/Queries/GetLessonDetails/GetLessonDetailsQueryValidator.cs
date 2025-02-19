﻿using FluentValidation;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class GetLessonDetailsQueryValidator : AbstractValidator<GetLessonDetailsQuery>
    {
        public GetLessonDetailsQueryValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.UserGuid).NotEmpty();
        }
    }
}
