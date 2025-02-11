using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;

namespace School.Application.Handlers.Comments.Queries.GetCommentList
{
    public class CommentLookupDto : IMapWith<Comment>
    {
        public int Id { get; set; }

        public string StudentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public bool IsPublic { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.StudentName, opt => opt.MapFrom(c => c.StudentName))
                .ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(c => c.CreatedAt))
                .ForMember(dto => dto.Text, opt => opt.MapFrom(c => c.Text))
                .ForMember(dto => dto.IsPublic, opt => opt.MapFrom(c => c.IsPublic));
        }
    }
}
