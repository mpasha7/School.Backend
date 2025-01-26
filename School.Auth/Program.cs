using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
using School.Auth.Configuration;
using School.Auth.Data;
using School.Auth.Extensions;
using School.Auth.Services;
using System.Reflection;

namespace School.Auth
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UsersDbContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection"));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters 
                        = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
                        + "абвгдеЄжзийклмнопрстуфхцчшщьыъэю€јЅ¬√ƒ≈®∆«»… ЋћЌќѕ–—“”‘’÷„Ўў№џЏЁёя";
            })
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer(opts =>
            {
                opts.Authentication.CookieLifetime = TimeSpan.FromMinutes(60);
            })
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential()      // TODO: действительный сертификат
                .AddProfileService<CustomProfileService>()
                .AddConfigurationStore(opts =>
                {
                    opts.ConfigureDbContext = c => c.UseSqlServer(
                        builder.Configuration.GetConnectionString("IdentityConnection"),
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opts =>
                {
                    opts.ConfigureDbContext = o => o.UseSqlServer(
                        builder.Configuration.GetConnectionString("IdentityConnection"),
                        sql => sql.MigrationsAssembly(migrationAssembly));
                });

            //builder.Services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", opts =>
            //    {
            //        opts.RequireHttpsMetadata = false; // TODO: HTTPS????
            //        opts.Authority = "https://localhost:7171"; // URL IdentityServer
            //        opts.Audience = "StudentsWebApi";
            //    });

            builder.Services.AddControllers();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", policy => // TODO: ќграничить CORS
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
                    var context = serviceProvider.GetRequiredService<UsersDbContext>();
                    UsersDbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while app initialization");
                }
            }
            await app.MigrateDatabase();

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");

            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/branch"), 
            //    branch =>
            //    {
            //        app.UseAuthentication();
            //    });
            //app.UseWhen(context => !context.Request.Path.StartsWithSegments("/branch"),
            //    branch =>
            //    {
            //        app.UseIdentityServer();
            //    });

            app.UseIdentityServer();
            app.UseAuthorization();

            //app.MapDefaultControllerRoute();
            app.MapControllers();
            //app.MapControllerRoute(
            //    name: "branch_ids",                
            //    pattern: "branch/api/{controller=Students}/byids/{ids}");
            //app.MapControllerRoute(
            //    name: "branch_search",
            //    pattern: "branch/api/{controller=Students}/bysearch/{search}");
            app.MapRazorPages();
            app.Run();
        }
    }
}
