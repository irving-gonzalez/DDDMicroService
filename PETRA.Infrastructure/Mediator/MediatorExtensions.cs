using MediatR;
using PETRA.Domain.Entities;
using PETRA.Infrastructure.DataAccess;

namespace PETRA.Infrastructure.Mediator.Extensions
{
    static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DatabaseContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            //TODO
            // domainEntities.ToList()
            //     .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}