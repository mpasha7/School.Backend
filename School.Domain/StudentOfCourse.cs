namespace School.Domain
{
    public class StudentOfCourse
    {
        public int Id { get; set; }
        public string? StudentGuid { get; set; }

        public virtual Course? Course { get; set; }
        public int CourseId { get; set; }
    }
}
