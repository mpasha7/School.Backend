namespace School.Domain
{
    public class Assessment
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public virtual Course? Course { get; set; }
        public int CourseId { get; set; }
    }
}
