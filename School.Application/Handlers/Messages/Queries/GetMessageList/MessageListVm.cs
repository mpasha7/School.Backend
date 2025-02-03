using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Application.Handlers.Lessons.Queries.GetLessonList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Messages.Queries.GetMessageList
{
    public class MessageListVm
    {
        public IList<MessageLookupDto> Messages { get; set; } = new List<MessageLookupDto>();
    }
}
