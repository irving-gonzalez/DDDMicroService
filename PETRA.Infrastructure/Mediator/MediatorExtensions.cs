using System.Reflection;
using Autofac;
using MediatR;
using PETRA.Domain.Entities;
using PETRA.Infrastructure.DataAccess;
using PETRA.Infrastructure.Decorators;
using PETRA.Infrastructure.Mediator.Attributes;

namespace PETRA.Infrastructure.Mediator.Extensions
{
    public static class MediatorExtensions
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

        public static void AddOutOfBoundDecorator(this ContainerBuilder builder)
        {
             builder.RegisterGenericDecorator(typeof(OutofBoundDecorator<>), typeof(IRequestHandler<,>), (ctx) => 
             { 
                 var attribute = ctx.CurrentInstance
                 .GetType()
                 .GetCustomAttribute<OutOfBandAttribute>();
                 return attribute is null ? false : true;
             });
        }
    }
}