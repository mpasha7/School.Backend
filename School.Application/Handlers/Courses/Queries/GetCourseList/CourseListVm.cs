namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class CourseListVm
    {
        public IList<CourseLookupDto> Courses { get; set; } = new List<CourseLookupDto>();
    }
}
