using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;
using System.Linq.Expressions;

namespace School.Application.Handlers.Messages.Queries.GetMessageList
{
    public class GetMessageListQueryHandler : IRequestHandler<GetMessageListQuery, MessageListVm>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetMessageListQueryHandler(
            IMessageRepository messageRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            this._messageRepository = messageRepository;
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }

        public async Task<MessageListVm> Handle(GetMessageListQuery request, CancellationToken cancellationToken)
        {
            if (request.CourseId != null)
            {
                var course = await _courseRepository.GetByIdAsync(
                    request.CourseId.Value,
                    cancellationToken);

                if (course == null)
                    throw new NotFoundException(nameof(Course), request.CourseId.Value);
                else if (request.UserRole == UserRoles.Coach
                    && course.CoachGuid != request.UserGuid)
                        throw new NoAccessException(nameof(Course), request.CourseId.Value);

                Expression<Func<Message, bool>> questionsFilter;
                Expression<Func<Message, bool>> answersFilter;
                switch (request.UserRole)
                {
                    case UserRoles.Admin:
                        throw new ArgumentNullException(nameof(request.UserRole));
                    case UserRoles.Coach:
                        questionsFilter = m => m.CourseId == request.CourseId && m.RecipientGuid == request.UserGuid;
                        answersFilter = m => m.CourseId == request.CourseId && m.SenderGuid == request.UserGuid;
                        break;
                    case UserRoles.Student:
                        questionsFilter = m => m.CourseId == request.CourseId && m.SenderGuid == request.UserGuid;
                        answersFilter = m => m.CourseId == request.CourseId && m.RecipientGuid == request.UserGuid;
                        break;
                    default:
                        throw new ArgumentNullException(nameof(request.UserRole));
                }

                var questions = (await _messageRepository.GetAllAsync(
                    cancellationToken,
                    filter: questionsFilter,
                    orderBy: x => x.OrderByDescending(m => m.CreatedAt)
                    ))
                    .AsQueryable()
                    .ProjectTo<MessageLookupDto>(_mapper.ConfigurationProvider)
                    .ToList();

                var answers = await _messageRepository.GetAllAsync(
                    cancellationToken,
                    filter: answersFilter
                    );

                foreach (var question in questions)
                {
                    question.Answers = answers.Where(a => a.QuestionId == question.Id)
                        .AsQueryable()
                        .ProjectTo<MessageLookupDto>(_mapper.ConfigurationProvider)
                        .ToList();
                }

                return new MessageListVm { Messages = questions };
            }

            throw new NotImplementedException();
        }
    }
}
