using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Files;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.PublicCourseList
{
    public class PublicCourseLookupDto : IMapWith<Course>
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? ShortDescription { get; set; }

        public FileLookupDto? Photo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, PublicCourseLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(dto => dto.ShortDescription, opt => opt.MapFrom(c => c.ShortDescription));
        }
    }
}
