using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Files;
using School.Domain;

namespace School.Application.Handlers.Reports.Queries.GetReportDetails
{
    public class ReportDetailsVm : IMapWith<Report>
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public virtual IEnumerable<FileLookupDto> Photos { get; set; } = new List<FileLookupDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Report, ReportDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(r => r.Id))
                .ForMember(vm => vm.Text, opt => opt.MapFrom(r => r.Text));
        }
    }
}
