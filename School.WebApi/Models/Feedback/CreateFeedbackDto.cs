using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Feedbacks.Commands.CreateFeedback;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Feedback
{
    public class CreateFeedbackDto : IMapWith<CreateFeedbackCommand>
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int ReportId { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFeedbackDto, CreateFeedbackCommand>()
                .ForMember(comm => comm.Text, opt => opt.MapFrom(dto => dto.Text))
                .ForMember(comm => comm.ReportId, opt => opt.MapFrom(dto => dto.ReportId))
                .ForMember(comm => comm.LessonId, opt => opt.MapFrom(dto => dto.LessonId))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
