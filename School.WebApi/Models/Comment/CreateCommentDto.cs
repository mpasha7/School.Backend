using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Comments.Commands.CreateComment;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Comment
{
    public class CreateCommentDto : IMapWith<CreateCommentCommand>
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommentDto, CreateCommentCommand>()
                .ForMember(comm => comm.Text, opt => opt.MapFrom(dto => dto.Text))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
