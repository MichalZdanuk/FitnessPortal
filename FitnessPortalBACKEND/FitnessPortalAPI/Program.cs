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

/*configuring CORS*/
app.UseCors("FrontEndClient");

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FitnessPortalSeeder>();
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();// before each request from API client there is need to authenticate with JWT

app.UseHttpsRedirection();

// Adding Swagger to Project
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessPortal API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
