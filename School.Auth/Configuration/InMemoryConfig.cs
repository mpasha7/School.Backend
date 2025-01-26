using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace School.Auth.Configuration
{
    public class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" })
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "8bed14b6-1158-4ea9-a838-3c1c4bb75fee",
                    Username = "Павел",
                    Password = "Poul123$",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Павел"),
                        new Claim("family_name", "Мелюхин"),
                        new Claim("email", "poul@mail.ru"),
                        new Claim("phone", "+79111111111"),
                        new Claim("role", "Admin")
                    }
                },
                new TestUser
                {
                    SubjectId = "3a3b611c-1185-445d-99c5-7f347675ec6e",
                    Username = "Ирина",
                    Password = "Irina123$",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Ирина"),
                        new Claim("family_name", "Панькова"),
                        new Claim("email", "irina@mail.ru"),
                        new Claim("phone", "+79222222222"),
                        new Claim("role", "Coach")
                    }
                },
                new TestUser
                {
                    SubjectId = "0b869d28-ab51-4310-ba0c-a3934e1de6de",
                    Username = "Ольга",
                    Password = "Olga123$",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Ольга"),
                        new Claim("family_name", "Ольгина"),
                        new Claim("email", "olga@mail.ru"),
                        new Claim("phone", "+79333333333"),
                        new Claim("role", "Coach")
                    }
                },
                new TestUser
                {
                    SubjectId = "77be0187-1d57-42dd-8d76-145c36c51bed",
                    Username = "Том",
                    Password = "Tom123$",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Том"),
                        new Claim("family_name", "Томин"),
                        new Claim("email", "tom@mail.ru"),
                        new Claim("phone", "+79444444444"),
                        new Claim("role", "Student")
                    }
                },
                new TestUser
                {
                    SubjectId = "acc53bf2-c3f6-442b-99c0-da2cf971516e",
                    Username = "Алекс",
                    Password = "Alex123$",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Алекс"),
                        new Claim("family_name", "Алексин"),
                        new Claim("email", "alex@mail.ru"),
                        new Claim("phone", "+79555555555"),
                        new Claim("role", "Student")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "School Angular",
                    ClientId = "school-angular",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:49824/signin-callback",       // URI Angular Client
                        "https://localhost:49824/assets/silent-callback.html"
                    },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        "roles",
                        "SchoolWebApi",
                        "StudentsWebApi"
                    },
                    AllowedCorsOrigins = { "https://localhost:49824" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:49824/signout-callback"
                    },
                    RequireConsent = false,
                    AccessTokenLifetime = 10 * 60
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("SchoolWebApi", "School Web API"),
                new ApiScope("StudentsWebApi", "Students Web API")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("SchoolWebApi", "School Web API")
                {
                    Scopes = { "SchoolWebApi" }
                },
                new ApiResource("StudentsWebApi", "Students Web API")
                {
                    Scopes = { "StudentsWebApi" }
                }
            };
        }
    }
}
