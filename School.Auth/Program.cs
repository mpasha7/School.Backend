using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
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

            builder.Services.Configure<WebEncoderOptions>(opts =>
            {
                opts.TextEncoderSettings = new System.Text.Encodings.Web
                        .TextEncoderSettings(System.Text.Unicode.UnicodeRanges.All);
                    
            });
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

            //app.MapGet("/", async (context, next) =>
            //{
            //    context.Response.Headers["Content-Type"] = "text/plain; charset=utf-8";
            //    return await next.Invoke(context);
            //});
            app.MapDefaultControllerRoute();
            app.MapRazorPages();
            app.Run();
        }
    }
}
