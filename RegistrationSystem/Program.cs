using Microsoft.OpenApi.Models;
using RegistrationSystem.AccessData.Extensions;
using RegistrationSystem.BusinessLogic.Extensions;
using RegistrationSystem.Controllers.Middleware;
using System.Text.Json.Serialization;

[assembly: System.Runtime.Versioning.SupportedOSPlatformAttribute("windows")]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers( );

builder.Services
    .AddDatabase(builder.Configuration)
    .AddAuthorization(builder.Configuration)
    .AddServices( )
    .AddControllers( )
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter( );
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

builder.Services.AddSwaggerGen(opions =>
{
    opions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Prasome ivesti validu tokena",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>( )
        }
    });
});

builder.Services.AddEndpointsApiExplorer( );

var app = builder.Build( );


if (app.Environment.IsDevelopment( ))
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseMiddleware<ExceptionHandlingMiddleware>( );

app.UseHttpsRedirection( );

app.UseAuthentication( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );
