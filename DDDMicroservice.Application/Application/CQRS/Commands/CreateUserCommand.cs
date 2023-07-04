using DDDMicroservice.Domain.AggregatesModel;
using DDDMicroservice.Infrastructure.Mediator;

namespace DDDMicroservice.Application.CQRS.Commands;

public record CreateUserCommand(User User) : ICommand;
