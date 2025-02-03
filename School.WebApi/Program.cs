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
using School.Application.Interfaces.Services;
using School.WebApi.Services;

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
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ILessonNumbersService, LessonNumbersService>();
            builder.Services.AddControllers();

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", policy => // TODO: ���������� CORS
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opts =>
                {
                    opts.RequireHttpsMetadata = false; // TODO: HTTPS????
                    opts.Authority = "https://localhost:7171"; // URL IdentityServer
                    opts.Audience = "SchoolWebApi";
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen(opts =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // TODO: ???
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
                    throw; // TODO: ��������� ����������
                }
            }

            // TEST
            app.Use(async (context, next) =>
            {
                var headers = context.Request.Headers;
                await next.Invoke();
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
