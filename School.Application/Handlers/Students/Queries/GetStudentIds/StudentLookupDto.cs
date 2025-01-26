namespace School.Application.Handlers.Students.Queries.GetStudentIds
{
    public class StudentLookupDto
    {
        public string StudentGuid { get; set; }
        public List<StudentCourseLookupDto> Courses { get; set; } = new List<StudentCourseLookupDto>();
    }
}
