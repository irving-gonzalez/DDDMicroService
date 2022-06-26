using MediatR;

namespace PETRA.Infrastructure.Mediator
{
    public interface ICommand :  IRequest<Unit>
    {
    }
}