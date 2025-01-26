namespace School.Auth.Models
{
    public class StudentIdsVm
    {
        public IList<StudentLookupDto> studentsIds { get; set; } = new List<StudentLookupDto>();
        public IList<StudentCourseLookupDto> allCourses { get; set; } = new List<StudentCourseLookupDto>();
    }
}
