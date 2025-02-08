using System.Text.Json.Serialization;

namespace School.Domain
{
    public class Report
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public virtual IEnumerable<FileObject> Files { get; set; }

        public virtual Lesson? Lesson { get; set; }
        public int LessonId { get; set; }

        [JsonIgnore]
        public virtual Feedback? Feedback { get; set; }

        public Report()
        {
            Files = new List<FileObject>();
        }
    }
}
