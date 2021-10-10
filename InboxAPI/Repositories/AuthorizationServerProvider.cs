using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using InboxAPI.Models;

namespace InboxAPI.Repositories
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private DataLayer _dataLayer = new DataLayer();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var user = _dataLayer.Validateuser(context.UserName, Helper.GetHash(context.Password));

            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullNames));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Username));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {"client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                    {"userName", context.UserName}
                });
            var ticket = new AuthenticationTicket(identity, props);

            context.Validated(ticket);
        }


    }
}