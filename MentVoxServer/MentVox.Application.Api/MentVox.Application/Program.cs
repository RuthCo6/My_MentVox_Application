﻿using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using MentVox.Core.Interfaces;
using MentVox.Data;
using MentVox.Service.Services;
using MentVox_Application_.Controllers;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MentVox_Application.Api.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using MentVox.Core.Mappings;

var builder = WebApplication.CreateBuilder(args);

// טעינת הגדרות AWS מה-Configuration
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>(); // 🔥 הוספת שירות S3



builder.Services.AddAutoMapper(typeof(ConversationProfile));

builder.Services.AddScoped<IWhisperService, WhisperService>(); 
builder.Services.AddScoped<IChatGptService, ChatGptService>(); 
builder.Services.AddScoped<IElevenLabsService, ElevenLabsService>(); 
builder.Services.AddScoped<IConversationService, ConversationService>();



// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// הרשמה למחלקה שמפיקה את הטוקן
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});


// הוספת DbContext עם מחרוזת החיבור
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("Data Source = DESKTOP-P6FFS03; Initial Catalog = MentVox.Application; Integrated Security = true; Trusted_Connection = SSPI; MultipleActiveResultSets = true; TrustServerCertificate = true;");
});

// Add services to the container.

builder.Services.AddControllers();
// הוספת HttpClient עבור ElevenLabsService
builder.Services.AddHttpClient<ElevenLabsService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();