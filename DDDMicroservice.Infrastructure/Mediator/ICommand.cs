using MediatR;

namespace DDDMicroservice.Infrastructure.Mediator
{
    public interface ICommand : IRequest<Unit>
    {
    }
}