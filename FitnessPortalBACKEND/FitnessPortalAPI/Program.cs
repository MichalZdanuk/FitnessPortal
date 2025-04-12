using FitnessPortalAPI;
using FitnessPortalAPI.DependencyInjection;
using FitnessPortalAPI.Middleware;
using FitnessPortalAPI.Seeding;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthyHabitHub_API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

/*added CORS*/
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Value ?? ""; // Provide a default empty string if it's null
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", builderCors =>
        builderCors.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(allowedOrigins.Split(",", StringSplitOptions.RemoveEmptyEntries)) // No null reference warning here
    );
});

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
