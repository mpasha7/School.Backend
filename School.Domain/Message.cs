namespace School.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderGuid { get; set; }
        public string SenderName { get; set; }
        public string RecipientGuid { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsRead { get; set; }
        public UserRoles SenredRole { get; set; }
        public int? QuestionId { get; set; }

        public virtual Course? Course { get; set; }
        public int? CourseId { get; set; }
    }
}
