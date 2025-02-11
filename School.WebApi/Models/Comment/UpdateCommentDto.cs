using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Comments.Commands.UpdateComment;
using System.ComponentModel.DataAnnotations;

namespace School.WebApi.Models.Comment
{
    public class UpdateCommentDto : IMapWith<UpdateCommentCommand>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCommentDto, UpdateCommentCommand>()
                .ForMember(comm => comm.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
