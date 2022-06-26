using MediatR;
using PETRA.Infrastructure.Mediator.Attributes;

namespace PETRA.Application.Commands
{
    [OutOfBand]
    public class SendMsgCommandHandler : IRequestHandler<SendMsgCommand, bool>
    {
        public SendMsgCommandHandler()
        {
            
        }

        public Task<bool> Handle(SendMsgCommand request, CancellationToken cancellationToken)
        {
           Console.WriteLine($"Sending msg {request._msg}");
           return Task.FromResult(true);
        }
    }
}