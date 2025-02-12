using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Application.Handlers.Reports.Queries.GetReportDetails;
using School.Application.Handlers.Reports.Queries.GetReportList;
using School.Domain;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class LessonDetailsVm : IMapWith<Lesson>
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public CourseDetailsVm Course { get; set; }
        public ReportDetailsVm? Report { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LessonDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(les => les.Id))
                .ForMember(vm => vm.Number, opt => opt.MapFrom(les => les.Number))
                .ForMember(vm => vm.Title, opt => opt.MapFrom(les => FixNull(les.Title)))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(les => FixNull(les.Description)))
                .ForMember(vm => vm.VideoLink, opt => opt.MapFrom(les => FixNull(les.VideoLink)));
        }

        private string FixNull(string? value)
        {
            return (value == null || value == "null") ? "" : value;
        }
    }
}
