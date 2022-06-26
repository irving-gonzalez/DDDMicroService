using Hangfire;
using MediatR;

namespace PETRA.Infrastructure.Decorators
{
    public class OutofBoundDecorator<TRequest> : IRequestHandler<TRequest, bool> where TRequest : IRequest<bool>
    {
        private readonly IRequestHandler<TRequest, bool> _inner;
        private readonly IBackgroundJobClient _backgroundJobs;
        
        public OutofBoundDecorator(IRequestHandler<TRequest, bool> inner, IBackgroundJobClient backgroundJobs)
        {
            _inner = inner;
            _backgroundJobs = backgroundJobs;
        }

        Task<bool> IRequestHandler<TRequest, bool>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            _backgroundJobs.Enqueue(() => _inner.Handle(request, cancellationToken));
             return Task.FromResult(true);
        }
    }
}