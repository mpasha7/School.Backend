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
                        + "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùüûúışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÜÛÚİŞß";
            })
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer(opts =>
            {
                opts.Authentication.CookieLifetime = TimeSpan.FromMinutes(60);
            })
                //.AddTestUsers(InMemoryConfig.GetUsers())
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential()      // TODO: äåéñòâèòåëüíûé ñåğòèôèêàò
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
            //builder.Services.Configure<IdentityOptions>(opts =>
            //{
            //    opts.User.RequireUniqueEmail = true;
            //    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
            //                                        + "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùüûúışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÜÛÚİŞß";
            //});

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
