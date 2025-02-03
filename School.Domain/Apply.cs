namespace School.Domain
{
    public class Apply
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }

        public bool IsAssepted { get; set; }

        public virtual Course? Course { get; set; }
        public int CourseId { get; set; }
    }
}
