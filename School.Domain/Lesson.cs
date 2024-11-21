using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class Lesson
    {
        public int Id { get; set; }

        public int? Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public virtual Course? Course { get; set; }
        public int CourseId { get; set; }

        public Lesson()
        {

        }
    }
}
