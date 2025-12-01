using HireAI.Data.Models.Identity;
using HireAI.Infrastructure.Context;
using HireAI.Infrastructure.Intrefaces;
using HireAI.Infrastructure.Mappings;
using HireAI.Infrastructure.Repositories;
using HireAI.Service.Implementation;
using HireAI.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HireAI.Service.Interfaces;
using HireAI.Infrastructure.GenericBase;
using HireAI.API.Extensions;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon;
using Microsoft.Extensions.DependencyInjection;

namespace HireAI.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            // Register DbContext
            builder.Services.AddDbContext<HireAIDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("HireAiDB"));
            });

            // Register Identity
            builder.Services.AddApplicationIdentity();

            // Register repositories and services using extension methods
            builder.Services.AddApplicationRepositories();
            builder.Services.AddApplicationServices();

            // Add AutoMapper service
            builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationProfile).Assembly);

            // Configure AWS S3 with credentials from appsettings
            var awsAccessKey = builder.Configuration["AWS:AccessKey"];
            var awsSecretKey = builder.Configuration["AWS:SecretKey"];
            var awsRegion = builder.Configuration["AWS:Region"];

            // Replace the incorrect AddAWSService registration with the correct usage of AWSOptions
            var awsOptions = builder.Configuration.GetAWSOptions();
            awsOptions.Credentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
            awsOptions.Region = RegionEndpoint.GetBySystemName(awsRegion);
            builder.Services.AddDefaultAWSOptions(awsOptions);
            builder.Services.AddAWSService<IAmazonS3>();

            var app = builder.Build();

            // Seed the database
            await app.SeedDatabaseAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
