using PETRA.Infrastructure.Mediator;

namespace PETRA.Application.Commands
{
    public class SendMsgCommand : ICommand
    {
        public string _msg { get; set; }
        public SendMsgCommand(string msg)
        {
            _msg = msg;
        }
    }
}