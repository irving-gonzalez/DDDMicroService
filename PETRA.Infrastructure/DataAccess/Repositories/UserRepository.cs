using PETRA.Domain;
using PETRA.Domain.AggregatesModel;

namespace PETRA.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}