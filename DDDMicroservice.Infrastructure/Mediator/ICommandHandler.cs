using MediatR;

namespace DDDMicroservice.Infrastructure.Mediator
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest, Unit> where TRequest : ICommand
    {
    }
}