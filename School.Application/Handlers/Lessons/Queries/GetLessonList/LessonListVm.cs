using School.Application.Handlers.Courses.Queries.GetCourseList;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class LessonListVm
    {
        public IList<LessonLookupDto> Lessons { get; set; } = new List<LessonLookupDto>();
        public CourseLookupDto Course { get; set; }
        public int MaxLessonNumber { get; set; }
    }
}
