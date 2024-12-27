using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<int>
    {
        public string? CoachGuid { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? PublicDescription { get; set; }
        public string? BeginQuestionnaire { get; set; }
        public string? EndQuestionnaire { get; set; }

        public IFormFile? FormFile { get; set; }
    }
}
