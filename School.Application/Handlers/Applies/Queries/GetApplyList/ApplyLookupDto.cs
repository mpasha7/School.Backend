using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;

namespace School.Application.Handlers.Applies.Queries.GetApplyList
{
    public class ApplyLookupDto : IMapWith<Apply>
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Apply, ApplyLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(dto => dto.StudentGuid, opt => opt.MapFrom(a => a.StudentGuid))
                .ForMember(dto => dto.StudentName, opt => opt.MapFrom(a => a.StudentName));
        }
    }
}
