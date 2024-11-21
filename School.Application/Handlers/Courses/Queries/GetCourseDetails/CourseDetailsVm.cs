using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class CourseDetailsVm : IMapWith<Course>
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? PublicDescription { get; set; }
        public string? PhotoPath { get; set; }
        public string? BeginQuestionnaire { get; set; }
        public string? EndQuestionnaire { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(vm => vm.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(vm => vm.ShortDescription, opt => opt.MapFrom(c => c.ShortDescription))
                .ForMember(vm => vm.PublicDescription, opt => opt.MapFrom(c => c.PublicDescription))
                .ForMember(vm => vm.PhotoPath, opt => opt.MapFrom(c => c.PhotoPath))
                .ForMember(vm => vm.BeginQuestionnaire, opt => opt.MapFrom(c => c.BeginQuestionnaire))
                .ForMember(vm => vm.EndQuestionnaire, opt => opt.MapFrom(c => c.EndQuestionnaire));
        }
    }
}
