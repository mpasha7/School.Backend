using Microsoft.EntityFrameworkCore;
using School.Auth.Configuration;
using School.Auth.Extensions;
using School.Auth.Services;
using System.Reflection;

namespace School.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddIdentityServer(opts =>
            {
                opts.Authentication.CookieLifetime = TimeSpan.FromMinutes(60);
            })
                .AddTestUsers(InMemoryConfig.GetUsers())
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

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            var app = builder.Build();


            app.MigrateDatabase();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();
            app.Run();
        }
    }
}
