using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Courses.Queries.GetCourseList;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class LessonListVm
    {
        public IList<LessonLookupDto> Lessons { get; set; } = new List<LessonLookupDto>();
        public CourseDetailsVm Course { get; set; }
        public int MaxLessonNumber { get; set; }
    }
}
