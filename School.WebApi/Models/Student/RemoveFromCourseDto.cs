using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Students.Commands.RemoveStudentFromCourse;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Student
{
    public class RemoveFromCourseDto : IMapWith<RemoveStudentFromCourseCommand>
    {
        [Required]
        public string StudentGuid { get; set; }
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RemoveFromCourseDto, RemoveStudentFromCourseCommand>()
                .ForMember(comm => comm.StudentGuid, opt => opt.MapFrom(dto => dto.StudentGuid))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
