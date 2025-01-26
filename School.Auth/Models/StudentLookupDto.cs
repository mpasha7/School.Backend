namespace School.Auth.Models
{
    public class StudentLookupDto
    {
        public string studentGuid { get; set; }
        public List<StudentCourseLookupDto> courses { get; set; } = new List<StudentCourseLookupDto>();
    }
}
