using System.Linq.Expressions;
using MediatR;
using DDDMicroservice.Domain.AggregatesModel;

namespace DDDMicroservice.Application.Queries;

public class GetUsersQuery : IRequest<IEnumerable<User>>
{
    public Expression<Func<User, bool>>? Filter { get; set; }

    public GetUsersQuery(Expression<Func<User, bool>>? filter = null)
    {
        Filter = filter;
    }
}
