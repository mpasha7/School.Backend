namespace School.Application.Handlers.Courses.Queries.PublicCourseList
{
    public class PublicCourseListVm
    {
        public IList<PublicCourseLookupDto> Courses { get; set; } = new List<PublicCourseLookupDto>();
    }
}
