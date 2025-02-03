using System.Text.Json.Serialization;

namespace School.Domain
{
    public class Course
    {
        public int Id { get; set; }
        public string? CoachGuid { get; set; }

        public DateTime CreatedDate { get; set; }        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? PublicDescription { get; set; }
        public string? BeginQuestionnaire { get; set; }
        public string? EndQuestionnaire { get; set; }

        public virtual FileObject? Photo { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Lesson> Lessons { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<StudentOfCourse> Students { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Apply> Applies { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Assessment> Assessments { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Comment> Comments { get; set; }

        public Course()
        {
            Lessons = new List<Lesson>();
            Students = new List<StudentOfCourse>();
            Messages = new List<Message>();
            Applies = new List<Apply>();
            Assessments = new List<Assessment>();
            Comments = new List<Comment>();
        }
    }
}
