using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using School.Auth.Configuration;

namespace School.Auth.Services
{
    public class CustomProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = InMemoryConfig.GetUsers()
                .Find(u => u.SubjectId.Equals(subjectId));

            context.IssuedClaims.AddRange(user.Claims); // TODO: Null-Safety
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = InMemoryConfig.GetUsers()
                .Find(u => u.SubjectId.Equals(subjectId));

            context.IsActive = user != null;
            return Task.CompletedTask;
        }
    }
}
