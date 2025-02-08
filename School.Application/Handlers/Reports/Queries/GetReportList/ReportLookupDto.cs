using AutoMapper;
using School.Application.Common.Mappings;
using School.Domain;

namespace School.Application.Handlers.Reports.Queries.GetReportList
{
    public class ReportLookupDto : IMapWith<Report>
    {
        public int Id { get; set; }
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }
        public DateTime CreatedAt { get; set; }

        public int LessonId { get; set; }
        public int LessonNumber { get; set; }
        public string LessonTitle { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Report, ReportLookupDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(r => r.Id))
                .ForMember(dto => dto.StudentGuid, opt => opt.MapFrom(r => r.StudentGuid))
                .ForMember(dto => dto.StudentName, opt => opt.MapFrom(r => r.StudentName))
                .ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(r => r.CreatedAt))
                .ForMember(dto => dto.LessonId, opt => opt.MapFrom(r => r.LessonId))
                .ForMember(dto => dto.LessonNumber, opt => opt.MapFrom(r => r.Lesson.Number))
                .ForMember(dto => dto.LessonTitle, opt => opt.MapFrom(r => r.Lesson.Title));
        }
    }
}
