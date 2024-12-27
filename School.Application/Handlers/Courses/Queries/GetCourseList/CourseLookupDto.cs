using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Files;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class CourseLookupDto : IMapWith<Course>
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        public FileLookupDto? Photo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(c => c.Description));
        }
    }
}
