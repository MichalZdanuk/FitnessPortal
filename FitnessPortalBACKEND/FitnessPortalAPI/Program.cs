using FitnessPortalAPI.DAL;
using FitnessPortalAPI.DependencyInjection;
using FitnessPortalAPI.Middleware;
using FitnessPortalAPI.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddSwaggerDocs()
    .AddCustomCors(builder.Configuration);

var app = builder.Build();

app.UseCors("FrontEndClient");

if (app.Environment.IsDevelopment())
{
    using(var servicesScope = app.Services.CreateScope())
    {
        var services = servicesScope.ServiceProvider;
        var dbContext = services.GetRequiredService<FitnessPortalDbContext>();

        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FitnessPortalSeeder>();
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessPortal API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
