using BHD.ApiUser.Middlewares;
using BHD.Application;
using BHD.Application.Repositories;
using BHD.Application.Services.Users;
using BHD.Application.Validator;
using BHD.Models.Models;
using BHD.Persistence;
using BHD.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BHD.Application.Security.Models;
using BHD.Application.Security.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using BHD.Application.Security.Authentication;
using BHD.Application.Security.Password;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(opt => {

    var authorizePolicy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()
                              .Build();
    opt.Filters.Add(new AuthorizeFilter(authorizePolicy));

})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Usa camelCase
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase; // Usa camelCase para claves de diccionario
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Convierte enums a strings
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Ignora mayúsculas/minúsculas en nombres de propiedades
    options.JsonSerializerOptions.DefaultBufferSize = 16 * 1024; // Tamaño de búfer por defecto
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("V1", new OpenApiInfo()
    {
        Version = "V1",
        Title = "BHD API",
    });



    string xmlFile = "BHD.ApiUserDoc.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if(File.Exists(xmlPath))
        opt.IncludeXmlComments(xmlPath);



    opt.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Ingrese el token JWT en el formato 'Bearer {token}'"
        });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });

});

string issuer = builder.Configuration.GetValue<string>("JWT:Issuer");
string audience = builder.Configuration.GetValue<string>("JWT:Audience");
TimeSpan validFor = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("JWT:TokenTimeout"));
IJwtKeyProvider jwtKeyProvider = new JwtKeyProvider(issuer, audience);
builder.Services.Configure<JwtIssuerOptions>(opt =>
{
    opt.Issuer = issuer;
    opt.Audience = audience;
    opt.SigningCredentials = jwtKeyProvider.GetSigningCredentials();
    opt.ValidFor = validFor;
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IValidator<User>, UserValidator>();

builder.Services.AddDbContext<BHDContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BHDConnection"));
});


builder.Services.AddAutoMapper(typeof(BHDMapper));


#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
#endregion



#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtFactory, JwtFactory>();
builder.Services.AddScoped<IPasswordService, PasswordService>();


#endregion





var tokenValidationParameters = jwtKeyProvider.GetValidationParameters();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Audience = audience;
        options.ClaimsIssuer = issuer;
        options.TokenValidationParameters = tokenValidationParameters;
        options.SaveToken = true;
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V1/swagger.json", "BHD API v1");
    });
}



app.UseMiddleware<ResponseHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
