namespace School.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; }

        public virtual Course? Course { get; set; }
        public int CourseId { get; set; }
    }
}
