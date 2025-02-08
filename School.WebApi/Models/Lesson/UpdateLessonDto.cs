using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Lessons.Commands.UpdateLesson;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Lesson
{
    public class UpdateLessonDto : IMapWith<UpdateLessonCommand>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int? Number { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateLessonDto, UpdateLessonCommand>()
                .ForMember(comm => comm.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId))
                .ForMember(comm => comm.Number, opt => opt.MapFrom(dto => dto.Number))
                .ForMember(comm => comm.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(comm => comm.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(comm => comm.VideoLink, opt => opt.MapFrom(dto => dto.VideoLink));
        }
    }
}
