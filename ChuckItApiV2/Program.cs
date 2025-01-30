using ChuckItApiV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Amazon.CognitoIdentityProvider;


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
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://cognito-idp.{AWSRegion}.amazinaws.com/{cognitoUserPoolId}",
            ValidateAudience = true,
            ValidAudience = cognitoClientId,
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthentication();

builder.Services.AddControllers();

/* Swwagger Implementation
 * builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "ChuckItApiV2",
        Version = "v2"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter Bearer Token"
    });



    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }

    });
}); */

//Scalar Implementation(Testing to replace Swagger)

//AWS Services
var awsOptions = new AWSOptions
{
    Region = Amazon.RegionEndpoint.CACentral1
};

builder.Services.AddSingleton(awsOptions);

//AWS Services
builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();

//App Services


var app = builder.Build();

app.MapOpenApi();

//Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    /* app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v2/swagge3r.json", "ChuckItAPI V2");
    }); */
    
    //TODO Scalar Implementation with color scheme theme
    app.MapScalarApiReference();
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
    Console.WriteLine("Applying Migrations...");
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    Console.WriteLine("Migrations Completed");
    
}