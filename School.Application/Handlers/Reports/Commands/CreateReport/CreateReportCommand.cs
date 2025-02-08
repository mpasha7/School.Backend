using MediatR;
using Microsoft.AspNetCore.Http;

namespace School.Application.Handlers.Reports.Commands.CreateReport
{
    public class CreateReportCommand : IRequest<int>
    {
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }

        public string Text { get; set; }
        public IEnumerable<IFormFile> FormFiles { get; set; } = new List<IFormFile>();

        public int LessonId { get; set; }
        public int CourseId { get; set; }
    }
}
