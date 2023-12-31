﻿using Api.Data;
using Api.Models.Entities;
using Api.Services.Security;
using Api.Services.Smtp;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;

//Initialize builder
var builder = WebApplication.CreateBuilder(args);

//Web application provider and configuration
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

//Web application db connection string
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    //options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>();

builder.Services.AddCors(options =>
{
    var client = configuration.GetValue<string>("Client");
    var osm = configuration.GetValue<string>("OpenStreetMap");

    options.AddPolicy("OpenStreetMap", builder =>
    {
        builder.WithOrigins(osm)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    options.AddPolicy("TravelSpotApp", builder =>
    {
        builder.WithOrigins(client)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add the configuration for MailSettings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    })
    .AddGoogle(options =>
    {
        options.ClientId = "756512269430-nsemobm9quvtn8tgu07qh6roh2kleqdq.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("OpenStreetMap");
app.UseCors("TravelSpotApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
