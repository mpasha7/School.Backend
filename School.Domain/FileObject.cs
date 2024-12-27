using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        //[JsonIgnore]
        //public virtual Lesson? Lesson { get; set; }
        //public int? LessonId { get; set; }
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
