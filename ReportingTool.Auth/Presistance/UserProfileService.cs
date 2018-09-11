using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingTool.Auth.Presistance
{
    public class UserProfileService : IProfileService
    {
        protected readonly IUserStore userstore;

        public UserProfileService(IUserStore injectedUserStore)
        {
            userstore = injectedUserStore;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                var user = await userstore.FindBySubjectId(context.Subject.ToString());
                if (user != null)
                {
                    context.AddRequestedClaims(user.Claims);
                }
            }
            return;
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userstore.FindBySubjectId(context.Subject.ToString());
            context.IsActive = true;// !(user is null); // TODO check indicators like account status
            return;
        }
    }
}
