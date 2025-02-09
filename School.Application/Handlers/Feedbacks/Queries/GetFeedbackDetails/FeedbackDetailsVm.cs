using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;

namespace School.Application.Handlers.Feedbacks.Queries.GetFeedbackDetails
{
    public class FeedbackDetailsVm : IMapWith<Feedback>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ReportId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Feedback, FeedbackDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(f => f.Id))
                .ForMember(vm => vm.Text, opt => opt.MapFrom(f => f.Text))
                .ForMember(vm => vm.ReportId, opt => opt.MapFrom(f => f.ReportId));
        }
    }
}
