using Microsoft.OpenApi.Models;
using RegistrationSystem.AccessData.Extensions;
using RegistrationSystem.BusinessLogic.Extensions;
using System.Text.Json.Serialization;

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
            new string[]{ }
        }
    });
});

builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

var app = builder.Build( );


if (app.Environment.IsDevelopment( ))
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseHttpsRedirection( );

app.UseAuthentication( );
app.UseAuthorization( );

app.MapControllers( );

app.Run( );
