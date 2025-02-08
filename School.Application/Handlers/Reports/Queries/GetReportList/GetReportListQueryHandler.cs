﻿using AutoMapper;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Reports.Queries.GetReportList
{
    public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, ReportListVm>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetReportListQueryHandler(
            ILessonRepository lessonRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<ReportListVm> Handle(GetReportListQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken,
                includeCollection: "Lessons");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.CourseId);

            var reports = (await _lessonRepository.GetAllAsync(
                cancellationToken,
                filter: l => l.CourseId == request.CourseId,
                includeProperties: "Reports"))
                .AsQueryable()
                .SelectMany(
                    l => l.Reports,
                    (l, r) => _mapper.Map<ReportLookupDto>(r))
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return new ReportListVm 
            { 
                Reports = reports,
                MaxLessonNumber = course.Lessons.Max(les => les.Number) ?? 0
            };
        }
    }
}
