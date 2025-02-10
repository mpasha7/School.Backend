using AutoMapper;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Assessments.Queries.GetAssessmentDetails
{
    public class GetAssessmentDetailsQueryHandler : IRequestHandler<GetAssessmentDetailsQuery, AssessmentDetailsVm>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetAssessmentDetailsQueryHandler(
            ICourseRepository courseRepository,
            IAssessmentRepository assessmentRepository,
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            this._courseRepository = courseRepository;
            this._assessmentRepository = assessmentRepository;
            this._studentRepository = studentRepository;
            this._mapper = mapper;
        }

        public async Task<AssessmentDetailsVm> Handle(GetAssessmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(
                request.CourseId,
                cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.CourseId);
            else if (!(await _studentRepository.IsStudentOfThisCourse(
                request.StudentGuid,
                request.CourseId,
                cancellationToken)))
                throw new NoAccessException(nameof(Course), request.CourseId);

            var assessment = (await _assessmentRepository.GetAllAsync(
                cancellationToken,
                filter: a => a.StudentGuid == request.StudentGuid && a.CourseId == request.CourseId))
                .SingleOrDefault();

            if (assessment == null)
                throw new NotFoundException(nameof(Assessment), 0);

            return _mapper.Map<AssessmentDetailsVm>(assessment);
        }
    }
}
