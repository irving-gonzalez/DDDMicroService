using System.Linq.Expressions;
using MediatR;
using PETRA.Domain.AggregatesModel;

namespace PETRA.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext dbContext, IMediator mediator) : base(dbContext, mediator)
        {
        }
    }
    public class UserRepositoryDecorator :  IUserRepository
    {
        IUserRepository _inner;
        public UserRepositoryDecorator(IUserRepository inner)
        {
            _inner = inner;
        }

        public async Task<User>? Add(User model)
        {
            return await _inner.Add(model);
        }

        public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> filter)
        {
            return await _inner.GetAll();
        }
    }
}