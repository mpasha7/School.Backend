using System.Text.Json.Serialization;

namespace School.Domain
{
    public class Lesson
    {
        public int Id { get; set; }

        public int? Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public virtual IEnumerable<FileObject> Files { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Report> Reports { get; set; }

        public virtual Course? Course { get; set; }
        public int CourseId { get; set; }

        public Lesson()
        {
            Files = new List<FileObject>();
            Reports = new List<Report>();
        }
    }
}
