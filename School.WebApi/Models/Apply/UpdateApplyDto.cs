using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Applies.Commands.UpdateApply;

namespace School.WebApi.Models.Apply
{
    public class UpdateApplyDto : IMapWith<UpdateApplyCommand>
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }

        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateApplyDto, UpdateApplyCommand>()
                .ForMember(comm => comm.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(comm => comm.StudentGuid, opt => opt.MapFrom(dto => dto.StudentGuid))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
