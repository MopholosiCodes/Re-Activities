using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
/* Add services to the container. Services we use in our application logic. 
They addd more functionality to our project Added in our app using dependency injection */
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    // persistance service using sqlite
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
/* Configure the HTTP request pipeline. Middleware. Interact with 
HTTP requests on their way in/out of the api referred to as a pipeline */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// directs request to relevent controller
app.MapControllers();
/* dependency injection - the using stateent allows for the 
 function to be destroyed from memory after use */
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // create a database if it does not already ex
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    // seed data into db
    await SeedDb.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
