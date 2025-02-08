using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Applies.Commands.CreateApply;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Apply
{
    public class CreateApplyDto : IMapWith<CreateApplyCommand>
    {
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateApplyDto, CreateApplyCommand>()
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
