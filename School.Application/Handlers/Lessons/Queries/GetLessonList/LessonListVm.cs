using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class LessonListVm
    {
        public IList<LessonLookupDto> Lessons { get; set; } = new List<LessonLookupDto>();
    }
}
