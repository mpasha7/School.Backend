using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Courses.Commands.CreateCourse;
using School.Application.Handlers.Courses.Commands.UpdateCourse;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Course
{
    public class UpdateCourseDto : IMapWith<UpdateCourseCommand>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? PublicDescription { get; set; }
        public string? PhotoPath { get; set; }
        public string? BeginQuestionnaire { get; set; }
        public string? EndQuestionnaire { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCourseDto, UpdateCourseCommand>()
                .ForMember(comm => comm.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(comm => comm.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(comm => comm.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(comm => comm.ShortDescription, opt => opt.MapFrom(dto => dto.ShortDescription))
                .ForMember(comm => comm.PublicDescription, opt => opt.MapFrom(dto => dto.PublicDescription))
                .ForMember(comm => comm.PhotoPath, opt => opt.MapFrom(dto => dto.PhotoPath))
                .ForMember(comm => comm.BeginQuestionnaire, opt => opt.MapFrom(dto => dto.BeginQuestionnaire))
                .ForMember(comm => comm.EndQuestionnaire, opt => opt.MapFrom(dto => dto.EndQuestionnaire));
        }
    }
}
