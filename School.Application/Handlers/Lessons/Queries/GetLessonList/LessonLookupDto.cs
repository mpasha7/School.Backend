using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class LessonLookupDto : IMapWith<Lesson>
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string? Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LessonLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(les => les.Id))
                .ForMember(dto => dto.Number, opt => opt.MapFrom(les => les.Number))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(les => les.Title));
        }
    }
}
