using MediatR;

namespace PETRA.Infrastructure.Mediator
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest, Unit> where TRequest : IRequest<Unit>
    {
    }
}