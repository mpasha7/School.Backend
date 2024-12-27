using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Files;
using School.Application.Interfaces;
using School.Application.Interfaces.Repository;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailsVm>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(ICourseRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<CourseDetailsVm> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await _repository.GetByIdAsync(request.Id, cancellationToken, includeProperty: "Photo");

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (course.CoachGuid != request.CoachGuid)
                throw new NoAccessException(nameof(Course), request.Id);

            var vm = _mapper.Map<CourseDetailsVm>(course);
            vm.Photo = _mapper.Map<FileLookupDto>(course.Photo);
            return vm;
        }
    }
}
