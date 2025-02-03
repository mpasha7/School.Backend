using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Courses.Commands.CreateCourse;
using School.Application.Handlers.Messages.Commands.CreateMessage;
using School.Domain;
using School.WebApi.Models.Course;

namespace School.WebApi.Models.Message
{
    public class CreateMessageDto : IMapWith<CreateMessageCommand>
    {
        public string RecipientGuid { get; set; }

        public string Theme { get; set; }
        public string Text { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? QuestionId { get; set; }

        public int? CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMessageDto, CreateMessageCommand>()
                .ForMember(comm => comm.RecipientGuid, opt => opt.MapFrom(dto => dto.RecipientGuid))
                .ForMember(comm => comm.Theme, opt => opt.MapFrom(dto => dto.Theme))
                .ForMember(comm => comm.Text, opt => opt.MapFrom(dto => dto.Text))
                .ForMember(comm => comm.Email, opt => opt.MapFrom(dto => dto.Email))
                .ForMember(comm => comm.Phone, opt => opt.MapFrom(dto => dto.Phone))
                .ForMember(comm => comm.QuestionId, opt => opt.MapFrom(dto => dto.QuestionId))
                .ForMember(comm => comm.CourseId, opt => opt.MapFrom(dto => dto.CourseId));
        }
    }
}
