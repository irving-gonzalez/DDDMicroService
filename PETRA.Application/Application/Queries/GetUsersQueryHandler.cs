using System.Linq.Expressions;
using Hangfire;
using MediatR;
using PETRA.Domain.AggregatesModel;

namespace PETRA.Application.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository, IBackgroundJobClient backgroundJobs)
        {
            backgroundJobs.Enqueue(() => Console.WriteLine("test from hangfire 2"));
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
           return await _userRepository.GetAll(query.Filter);
        }
    }
}