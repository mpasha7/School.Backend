using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class GetLessonListQuery : IRequest<LessonListVm>
    {
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }
    }
}
