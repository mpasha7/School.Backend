﻿using FluentValidation;

namespace School.Application.Handlers.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandValidator : AbstractValidator<UpdateLessonCommand>
    {
        public UpdateLessonCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.Number).GreaterThan(0);
            RuleFor(comm => comm.Title).NotEmpty().MaximumLength(200);
        }
    }
}
