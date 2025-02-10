using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;

namespace School.Application.Handlers.Assessments.Queries.GetAssessmentDetails
{
    public class AssessmentDetailsVm : IMapWith<Assessment>
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }
        public string Text { get; set; }
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Assessment, AssessmentDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(a => a.Id))
                .ForMember(vm => vm.StudentGuid, opt => opt.MapFrom(a => a.StudentGuid))
                .ForMember(vm => vm.Text, opt => opt.MapFrom(a => a.Text))
                .ForMember(vm => vm.CourseId, opt => opt.MapFrom(a => a.CourseId));
        }
    }
}
