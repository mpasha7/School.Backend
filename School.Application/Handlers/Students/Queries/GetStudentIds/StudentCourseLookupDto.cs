using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;

namespace School.Application.Handlers.Students.Queries.GetStudentIds
{
    public class StudentCourseLookupDto : IMapWith<Course>
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, StudentCourseLookupDto>()
                .ForMember(dto => dto.CourseId, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title));
        }
    }
}
