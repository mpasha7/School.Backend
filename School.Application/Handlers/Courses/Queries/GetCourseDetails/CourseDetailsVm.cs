using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Files;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class CourseDetailsVm : IMapWith<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string PublicDescription { get; set; } = string.Empty;
        public string BeginQuestionnaire { get; set; } = string.Empty;
        public string EndQuestionnaire { get; set; } = string.Empty;

        public FileLookupDto? Photo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(vm => vm.Title, opt => opt.MapFrom(c => FixNull(c.Title)))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(c => FixNull(c.Description)))
                .ForMember(vm => vm.ShortDescription, opt => opt.MapFrom(c => FixNull(c.ShortDescription)))
                .ForMember(vm => vm.PublicDescription, opt => opt.MapFrom(c => FixNull(c.PublicDescription)))
                .ForMember(vm => vm.BeginQuestionnaire, opt => opt.MapFrom(c => FixNull(c.BeginQuestionnaire)))
                .ForMember(vm => vm.EndQuestionnaire, opt => opt.MapFrom(c => FixNull(c.EndQuestionnaire)));
        }

        private string FixNull(string? value)
        {
            return (value == null || value == "null") ? "" : value;
        }
    }
}
