using School.Application.Common.Mappings;
using School.Application.Interfaces;
using School.Application;
using School.Persistence;
using System.Reflection;
using School.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using School.Application.Interfaces.Repository;
using School.WebApi.Repository;

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
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ILessonRepository, LessonRepository>();
            builder.Services.AddControllers();

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", policy => // TODO: Ограничить CORS
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", opts =>
                {
                    opts.Authority = "https://localhost:44393";
                    opts.Audience = "CoursesWebApi";
                    opts.RequireHttpsMetadata = false; // TODO: HTTPS????
                });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen(opts =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opts.IncludeXmlComments(xmlPath);
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
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCustomExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                //opts.RoutePrefix = string.Empty;
                //opts.SwaggerEndpoint("swagger/v1/swagger.json", "School API");
            });
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
