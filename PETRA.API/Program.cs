var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => {
    return "Hello World!";
    });

app.Run();
