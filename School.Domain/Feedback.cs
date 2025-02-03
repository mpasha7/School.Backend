namespace School.Domain
{
    public class Feedback
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public virtual Report? Report { get; set; }
        public int ReportId { get; set; }
    }
}
