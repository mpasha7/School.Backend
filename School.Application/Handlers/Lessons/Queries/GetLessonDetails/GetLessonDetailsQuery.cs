﻿using MediatR;
using School.Domain;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class GetLessonDetailsQuery : IRequest<LessonDetailsVm>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? UserGuid { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
