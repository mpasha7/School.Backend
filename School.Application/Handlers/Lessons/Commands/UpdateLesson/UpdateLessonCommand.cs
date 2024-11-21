using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommand : IRequest
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }

        public int? Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }
    }
}
