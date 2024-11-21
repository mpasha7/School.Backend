﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class GetLessonDetailsQuery : IRequest<LessonDetailsVm>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }
    }
}