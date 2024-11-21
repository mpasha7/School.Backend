using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommand : IRequest
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }
    }
}
