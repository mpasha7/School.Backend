using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using School.Auth.Configuration;

namespace School.Auth.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CustomProfileService(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();

            //var user = InMemoryConfig.GetUsers()
            //    .Find(u => u.SubjectId.Equals(subjectId));
            var user = await _userManager.FindByIdAsync(subjectId);
            var claims = await _userManager.GetClaimsAsync(user);

            context.IssuedClaims.AddRange(claims); // TODO: Null-Safety
            //return Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            //var user = InMemoryConfig.GetUsers()
            //    .Find(u => u.SubjectId.Equals(subjectId));
            var user = await _userManager.FindByIdAsync(subjectId);

            context.IsActive = user != null;
            //return Task.CompletedTask;
        }
    }
}
