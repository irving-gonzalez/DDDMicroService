using MediatR;
using PETRA.Infrastructure.Mediator;
using PETRA.Infrastructure.Mediator.Attributes;

namespace PETRA.Application.Commands
{
    [OutOfBand]
    public class SendMsgCommandHandler : ICommandHandler<SendMsgCommand>
    {
        public SendMsgCommandHandler()
        {    
        }

        public Task<Unit> Handle(SendMsgCommand request, CancellationToken cancellationToken)
        {
           Console.WriteLine($"Sending msg {request._msg}");
           return Unit.Task;
        }
    }
}