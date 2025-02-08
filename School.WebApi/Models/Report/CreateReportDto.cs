using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Reports.Commands.CreateReport;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Report
{
    public class CreateReportDto : IMapWith<CreateReportCommand>
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateReportDto, CreateReportCommand>()
                .ForMember(comm => comm.Text, opt => opt.MapFrom(dto => dto.Text))
                .ForMember(comm => comm.LessonId, opt => opt.MapFrom(dto => dto.LessonId))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
