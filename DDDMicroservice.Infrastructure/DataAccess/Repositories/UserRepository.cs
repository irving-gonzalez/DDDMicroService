using MediatR;
using DDDMicroservice.Domain.AggregatesModel;

namespace DDDMicroservice.Infrastructure.DataAccess.Repositories;

public class UserRepository : EFRepository<User>, IUserRepository
{
    public UserRepository(DatabaseContext dbContext, IMediator mediator) : base(dbContext, mediator)
    {
    }
}
