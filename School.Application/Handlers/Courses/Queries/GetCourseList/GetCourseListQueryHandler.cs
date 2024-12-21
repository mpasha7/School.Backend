using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, CourseListVm>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper mapper;

        public GetCourseListQueryHandler(ICourseRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this.mapper = mapper;
        }

        public async Task<CourseListVm> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var courses = (await _repository.GetAllAsync(cancellationToken, filter: c => c.CoachGuid == request.CoachGuid))
                .AsQueryable()
                .ProjectTo<CourseLookupDto>(mapper.ConfigurationProvider)
                .ToList();

            return new CourseListVm { Courses = courses };
        }
    }
}
