using System.Text.Json.Serialization;

namespace School.Domain
{
    public class FileObject
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? UniqueFileName { get; set; }
        public string? FileName { get; set; }
        public string? FileNameExt { get; set; }
        public long FileSize { get; set; }
        public FileTypes FileType { get; set; }
        public FileOwners FileOwner { get; set; }

        [JsonIgnore]
        public virtual Course? Course { get; set; }
        public int? CourseId { get; set; }

        [JsonIgnore]
        public virtual Lesson? Lesson { get; set; }
        public int? LessonId { get; set; }

        [JsonIgnore]
        public virtual Report? Report { get; set; }
        public int? ReportId { get; set; }
    }

    public enum FileTypes
    {
        Photo,
        Audio
    }

    public enum FileOwners
    {
        Course,
        Lesson,
        Report
    }
}
