﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest
    {
        public int Id { get; set; }
        public string? CoachGuid { get; set; }
    }
}
