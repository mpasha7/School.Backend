using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Comments.Queries.GetCommentList;
using School.Application.Handlers.Files;
using School.Domain;

namespace School.Application.Handlers.Courses.Queries.PublicCourseDetails
{
    public class PublicCourseDetailsVm : IMapWith<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string PublicDescription { get; set; } = string.Empty;

        public FileLookupDto? Photo { get; set; }
        public IList<CommentLookupDto> Comments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, PublicCourseDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(vm => vm.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(vm => vm.PublicDescription, opt => opt.MapFrom(c => c.PublicDescription));
        }
    }
}
