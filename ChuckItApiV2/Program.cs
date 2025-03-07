using ChuckItApiV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Amazon.CognitoIdentityProvider;
using ChuckIt.Core.Interfaces.IServices;
using ChuckIt.Core.Services;
using ChuckIt.Core.Interfaces.IRepositories;
using ChuckIt.Infrastructure.Repositories;
using Amazon.S3;


DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
//var environment = builder.Environment;

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var jwtKey = Environment.GetEnvironmentVariable("JwtKey");
var cognitoUserPoolId = Environment.GetEnvironmentVariable("COGNITO_USER_POOL_ID");
var cognitoClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID");
var AWSRegion = Environment.GetEnvironmentVariable("AWS_REGION");
var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
var secretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword)
    || string.IsNullOrEmpty(dbPort))
{
    throw new ArgumentNullException("Database environment variable not set");
}

var connectionString = $"Host={dbHost}; Port={dbPort}; UserId={dbUser}; Password={dbPassword}; Database={dbName}";

//Create ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging()
);

builder.Services.AddAuthorization();

//Cognito Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://cognito-idp.{AWSRegion}.amazonaws.com/{cognitoUserPoolId}";
        options.Audience = cognitoClientId;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://cognito-idp.{AWSRegion}.amazinaws.com/{cognitoUserPoolId}",
            ValidateAudience = true,
            ValidAudience = cognitoClientId,
            ValidateLifetime = true,
            RoleClaimType = "cognito:groups"
        };
    });

builder.Services.AddAuthentication();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//AWS Services
var awsOptions = new AWSOptions
{
    Region = Amazon.RegionEndpoint.CACentral1
};

builder.Services.AddSingleton(awsOptions);

//AWS Services
builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();
builder.Services.AddAWSService<IAmazonS3>();

//App Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IListingService, ListingService>();

//App Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();

var app = builder.Build();



//Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Chuck It V2 API")
            .Theme = ScalarTheme.Solarized;

    });
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Apply migrations
using (var scope = app.Services.CreateScope())
{
    try
    {
        Console.WriteLine("Applying Migrations...");
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }

    Console.WriteLine("Migrations Completed");   
}

app.Run();