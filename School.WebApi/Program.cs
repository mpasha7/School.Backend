using School.Application.Common.Mappings;
using School.Application.Interfaces;
using School.Application;
using School.Persistence;
using System.Reflection;
using School.WebApi.Middleware;

namespace School.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(opts =>
            {
                opts.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                opts.AddProfile(new AssemblyMappingProfile(typeof(ISchoolDbContext).Assembly));
            });

            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddControllers();

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", policy => // TODO: Ограничить CORS перед развертыванием
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<SchoolDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    throw; // TODO: Обработка исключения
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.MapControllers();
            app.Run();
        }
    }
}
