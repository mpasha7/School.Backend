namespace School.Auth.Models
{
    public class RemoveStudentFromCourseDto
    {
        public string studentGuid { get; set; }
        public int courseId { get; set; }
    }
}
