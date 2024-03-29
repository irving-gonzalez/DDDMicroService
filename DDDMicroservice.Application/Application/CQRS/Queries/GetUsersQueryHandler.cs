using MediatR;
using DDDMicroservice.Domain.AggregatesModel;

namespace DDDMicroservice.Application.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;
    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAll(query.Filter);
    }
}
