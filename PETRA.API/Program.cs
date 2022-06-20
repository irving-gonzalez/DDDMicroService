using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PETRA.Application.Config;
using PETRA.Application.Configuration;
using PETRA.Application.Queries;
using PETRA.Domain.AggregatesModel;
using PETRA.Infrastructure;
using PETRA.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
var configuration = AppConfigurationManager.Build(environment);
builder.Services.AddApplicationServices(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.Configure();

app.MapGet("/users", async (IMediator mediator) => {
        var query  = new GetUsersQuery(u => u.FirstName == "Jorge");
        return await mediator.Send(query);
    });

app.MapPost("/", async ([FromBody] User user, IUserRepository userRepository, DatabaseContext db) => {
       user.UpdateName("test","Test");
       return await userRepository.Add(user);
    });
    
app.MapPost("/message", async (string message, IBus bus, ISendEndpointProvider provider) => {
       //await provider.Send<Message>(new Message {Text = message});
       await bus.Publish(new Message { Text = message });
    });

app.Run();