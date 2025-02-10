using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Assessments.Commands.CreateAssessment;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Assessment
{
    public class CreateAssessmentDto : IMapWith<CreateAssessmentCommand>
    {
        [Required]
        public string StudentGuid { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAssessmentDto, CreateAssessmentCommand>()
                .ForMember(comm => comm.StudentGuid, opt => opt.MapFrom(dto => dto.StudentGuid))
                .ForMember(comm => comm.Text, opt => opt.MapFrom(dto => dto.Text))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
