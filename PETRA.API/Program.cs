using PETRA.Application.Config;
using PETRA.Application.Configuration;

var builder = WebApplication.CreateBuilder();

var environment = builder.Environment.EnvironmentName;
var configuration = AppConfigurationManager.Build(environment);
builder.Host.AddApplicationServices(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();


var app = builder.Build();

app.Configure();

 // var userrep =  app.Services.GetRequiredService<IUserRepository>();

// app.MapGet("/users", async (IMediator mediator) => {
//        //  db.Database.Migrate();
//         var query  = new GetUsersQuery();
//         return await mediator.Send(query);
//     });

// app.MapPost("/", async ([FromBody] User user, IUserRepository userRepository, DatabaseContext db) => {
//        //user.UpdateName("test","Test");
//        return await userRepository.Add(user);
//     });
    
// app.MapPost("/message", async (string message, IBus bus, ISendEndpointProvider provider) => {
//        //await provider.Send<Message>(new Message {Text = message});
//        await bus.Publish(new Message { Text = message });
//     });


app.MapControllers();
app.Run();