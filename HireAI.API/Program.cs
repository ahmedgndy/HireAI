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
