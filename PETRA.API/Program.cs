using Microsoft.EntityFrameworkCore;
using PETRA.API.Config;
using PETRA.Domain.AggregatesModel;
using PETRA.Infrastructure.DataAccess;
using PETRA.Infrastructure.DataAccess.Extesions;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{environment}.json",optional: false, reloadOnChange: true);
var configuration = builder.Configuration.Get<AppConfiguration>();

builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("UserManager"));
 

var app = builder.Build();

// var dbContext = app.Services.GetService<DatabaseContext>();
// dbContext.Database.EnsureCreated();

app.MapGet("/", async (IUserRepository userRepository,DatabaseContext db) => {
      db.Database.Migrate();
      return await userRepository.GetAll();
    });

app.Run();
