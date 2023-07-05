using Asp.Versioning;
using DDDMicroservice.Application.Config;
using DDDMicroservice.Application.Configuration;
using DDDMicroservice.Infrastructure.Jobs;

var builder = WebApplication.CreateBuilder();

var environment = builder.Environment.EnvironmentName;
var configuration = AppConfigurationManager.Build(environment);

//enable development settings for every environment except prod
builder.Environment.EnvironmentName = environment != "Prod" ? "Development" : environment;

builder.Host.AddLogging();
builder.Host.AddApplicationServices(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();


app.UseCors("CorsPolicy");
app.UseAuthentication();
app.Configure();
app.RunRecurrentJobs();

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

app.MapControllers();
app.Run();
