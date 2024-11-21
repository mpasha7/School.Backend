using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest
    {
        public int Id { get; set; }
        public string? CoachGuid { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? PublicDescription { get; set; }
        public string? PhotoPath { get; set; }
        public string? BeginQuestionnaire { get; set; }
        public string? EndQuestionnaire { get; set; }
    }
}
