using FitnessPortalAPI;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Middleware;
using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models.UserActions;
using FitnessPortalAPI.Models.Validators;
using FitnessPortalAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static FitnessPortalAPI.Services.TokenStore;

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
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddDbContext<FitnessPortalDbContext>();
builder.Services.AddScoped<FitnessPortalSeeder>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IBMICalculatorService, BMICalculatorService>();
builder.Services.AddScoped<IBMRCalculatorService, BMRCalculatorService>();
builder.Services.AddScoped<IBodyFatCalculatorService, BodyFatCalculatorService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddSingleton<ITokenStore, TokenStore>(); // added to check if token is invalid (on blacklist)
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<ArticleQuery>, ArticleQueryValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
builder.Services.AddHttpContextAccessor();


//builder.Services.AddSwaggerGen(); // adding Swagger to proj
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", builderCors =>
        builderCors.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Value)
    );
});
/**/


var app = builder.Build();
// Configure the HTTP request pipeline.

/*configuring CORS*/
app.UseCors("FrontEndClient");
/**/


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<FitnessPortalSeeder>();
seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();// before each request from API client there is need to authenticate with JWT

app.UseHttpsRedirection();

// Adding Swagger To Project
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessPortal API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
