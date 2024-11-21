using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Queries.GetLessonDetails
{
    public class LessonDetailsVm : IMapWith<Lesson>
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LessonDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(les => les.Id))
                .ForMember(vm => vm.Number, opt => opt.MapFrom(les => les.Number))
                .ForMember(vm => vm.Title, opt => opt.MapFrom(les => les.Title))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(les => les.Description))
                .ForMember(vm => vm.VideoLink, opt => opt.MapFrom(les => les.VideoLink));
        }
    }
}
