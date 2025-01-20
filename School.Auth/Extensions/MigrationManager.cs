using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Auth.Configuration;
using School.Auth.Data;
using System.Data;

namespace School.Auth.Extensions
{
    public static class MigrationManager
    {
        public static async Task<IHost> MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();

                        if (!context.Clients.Any())
                        {
                            foreach (var client in InMemoryConfig.GetClients())
                            {
                                context.Clients.Add(client.ToEntity());
                            }
                            context.SaveChanges();
                        }

                        if (!context.IdentityResources.Any())
                        {
                            foreach (var resource in InMemoryConfig.GetIdentityResources())
                            {
                                context.IdentityResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }

                        if (!context.ApiScopes.Any())
                        {
                            foreach (var apiScope in InMemoryConfig.GetApiScopes())
                            {
                                context.ApiScopes.Add(apiScope.ToEntity());
                            }
                            context.SaveChanges();
                        }

                        if (!context.ApiResources.Any())
                        {
                            foreach (var resource in InMemoryConfig.GetApiResources())
                            {
                                context.ApiResources.Add(resource.ToEntity());
                            }
                            context.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: Logging
                        throw;
                    }
                }

                scope.ServiceProvider.GetRequiredService<UsersDbContext>().Database.Migrate();

                using (var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    try
                    {
                        foreach (var role in new string[] { "Admin", "Coach", "Student" })
                        {
                            if (await roleManager.FindByNameAsync(role) == null)
                            {
                                await roleManager.CreateAsync(new IdentityRole(role));
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: Logging
                        throw;
                    }
                }

                using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
                {
                    try
                    {
                        foreach (var testUser in InMemoryConfig.GetUsers())
                        {
                            if (!userManager.Users.Where(u => u.UserName == testUser.Username).Any())
                            {
                                IdentityUser user = new IdentityUser
                                {
                                    Id = testUser.SubjectId,
                                    UserName = testUser.Username,
                                    Email = testUser.Claims.Where(c => c.Type == "email").Select(c => c.Value).SingleOrDefault(),
                                    PhoneNumber = testUser.Claims.Where(c => c.Type == "phone").Select(c => c.Value).SingleOrDefault()
                                };
                                IdentityResult result = await userManager.CreateAsync(user, testUser.Password);
                                string? role = testUser.Claims.Where(c => c.Type == "role").Select(c => c.Value).SingleOrDefault();
                                if (role != null)
                                    result = await userManager.AddToRoleAsync(user, role);
                                IdentityResult claimsResult1 = await userManager.AddClaimsAsync(user, testUser.Claims);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // TODO: Logging
                        throw;
                    }
                }
            }

            return host;
        }
    }
}
