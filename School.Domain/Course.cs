using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        //public virtual IEnumerable<StudentOfCourse> Students { get; set; }

        public Course()
        {
            Lessons = new List<Lesson>();
            //Students = new List<StudentOfCourse>();
        }
    }
}
