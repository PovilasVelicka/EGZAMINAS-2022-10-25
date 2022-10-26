using RegistrationSystem.AccessData.Extensions;
using RegistrationSystem.BusinessLogic.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

var app = builder.Build( );

// Configure the HTTP request pipeline.
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
