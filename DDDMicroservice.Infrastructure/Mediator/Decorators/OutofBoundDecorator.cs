using Hangfire;
using MediatR;
using DDDMicroservice.Infrastructure.Mediator;

namespace DDDMicroservice.Infrastructure.Decorators
{
    public class OutofBoundDecorator<TRequest> : ICommandHandler<TRequest> where TRequest : ICommand
    {
        private readonly IRequestHandler<TRequest, Unit> _inner;
        private readonly IBackgroundJobClient _backgroundJobs;

        public OutofBoundDecorator(IRequestHandler<TRequest, Unit> inner, IBackgroundJobClient backgroundJobs)
        {
            _inner = inner;
            _backgroundJobs = backgroundJobs;
        }

        Task<Unit> IRequestHandler<TRequest, Unit>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            _backgroundJobs.Enqueue(() => _inner.Handle(request, cancellationToken));
            return Unit.Task;
        }
    }
}