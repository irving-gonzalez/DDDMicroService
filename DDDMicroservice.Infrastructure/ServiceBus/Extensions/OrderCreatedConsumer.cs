using MassTransit;
using Microsoft.Extensions.Logging;

namespace DDDMicroservice.Infrastructure;


public class OrderCreated
{
    public string Text { get; set; } = string.Empty;
}

public class CreateOrder
{
    public string Text { get; set; } = string.Empty;
}

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    readonly ILogger<OrderCreatedConsumer> _logger;
    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger) => _logger = logger;
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        _logger.LogInformation("Received Text: {Text}", context.Message.Text);
        return Task.CompletedTask;
    }
}
public class CreateOrderConsumer : IConsumer<CreateOrder>
{
    readonly ILogger<CreateOrderConsumer> _logger;
    public CreateOrderConsumer(ILogger<CreateOrderConsumer> logger) => _logger = logger;
    public Task Consume(ConsumeContext<CreateOrder> context)
    {
        _logger.LogInformation("Received Text: {Text}", context.Message.Text);
        return Task.CompletedTask;
    }
}

// class MessageConsumerDefinition : ConsumerDefinition<MessageConsumer>
// {
//     public MessageConsumerDefinition()
//     {
//         // override the default endpoint name
//         EndpointName = "Message2";
//         // limit the number of messages consumed concurrently
//         // this applies to the consumer only, not the endpoint
//         ConcurrentMessageLimit = 8;
//     }
//     protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
//         IConsumerConfigurator<MessageConsumer> consumerConfigurator)
//     {
//         // configure message retry with millisecond intervals
//         //endpointConfigurator.UseMessageRetry(r => r.Intervals(100,200,500,800,1000));
//         // use the outbox to prevent duplicate events from being published
//         //endpointConfigurator.UseInMemoryOutbox();
//     }
// }
