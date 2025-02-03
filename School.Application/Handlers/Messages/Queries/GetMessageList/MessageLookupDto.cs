using AutoMapper;
using School.Application.Common.Mappings;
using School.Application.Handlers.Lessons.Queries.GetLessonList;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Messages.Queries.GetMessageList
{
    public class MessageLookupDto : IMapWith<Message>
    {
        public int Id { get; set; }
        public string SenderGuid { get; set; }
        public string SenderName { get; set; }
        public string RecipientGuid { get; set; }

        public string Theme { get; set; }
        public string Text { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsRead { get; set; }

        public IEnumerable<MessageLookupDto>? Answers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(dto => dto.SenderGuid, opt => opt.MapFrom(m => m.SenderGuid))
                .ForMember(dto => dto.SenderName, opt => opt.MapFrom(m => m.SenderName))
                .ForMember(dto => dto.RecipientGuid, opt => opt.MapFrom(m => m.RecipientGuid))
                .ForMember(dto => dto.Theme, opt => opt.MapFrom(m => m.Theme))
                .ForMember(dto => dto.Text, opt => opt.MapFrom(m => m.Text))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(m => m.Email))
                .ForMember(dto => dto.Phone, opt => opt.MapFrom(m => m.Phone))
                .ForMember(dto => dto.IsRead, opt => opt.MapFrom(m => m.IsRead));
        }
    }
}
