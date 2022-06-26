using MediatR;

namespace PETRA.Application.Commands
{
    public class SendMsgCommand : IRequest<bool>
    {
        public string _msg { get; set; }
        public SendMsgCommand(string msg)
        {
            _msg = msg;
        }
    }
}