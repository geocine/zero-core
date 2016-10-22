using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict;
using Zervo.Data.Models;
using Zervo.ViewModels;

namespace Zervo.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthorizationController(
           SignInManager<User> signInManager,
           UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Note: to support non-interactive flows like password,
        // you must provide your own token endpoint action:
        /*
         * username
         * password
         * grant_type
         * scope -> offline_access
         * resource -> audience
         */
        [HttpPost("token")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateToken(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return BadRequest(new OpenIdConnectResponse()
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                if (!await _signInManager.CanSignInAsync(user))
                {
                    return BadRequest(new OpenIdConnectResponse()
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                //if (!await _userManager.CheckPasswordAsync(user, request.Password))
                //{
                //    return BadRequest(new OpenIdConnectResponse()
                //    {
                //        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                //        ErrorDescription = "The username/password couple is invalid."
                //    });
                //}

                var scopes = new[]
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess
                }.Intersect(request.GetScopes()).ToList();

                var principal = await _signInManager.CreateUserPrincipalAsync(user);

                foreach (var claim in principal.Claims)
                {
                    if (claim.Type == ClaimTypes.NameIdentifier)
                    {
                        claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
                                              OpenIdConnectConstants.Destinations.IdentityToken);
                    }
                    else if (claim.Type == ClaimTypes.Name && scopes.Contains(OpenIdConnectConstants.Scopes.Profile))
                    {
                        claim.SetDestinations(OpenIdConnectConstants.Destinations.IdentityToken);
                    }
                    else if (claim.Type == ClaimTypes.Role && scopes.Contains(OpenIddictConstants.Scopes.Roles))
                    {
                        claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
                                              OpenIdConnectConstants.Destinations.IdentityToken);
                    }
                }

                var ticket = new AuthenticationTicket(
                    principal,
                    new AuthenticationProperties(),
                    OpenIdConnectServerDefaults.AuthenticationScheme);

                ticket.SetResources(request.GetResources());
                ticket.SetScopes(request.GetScopes());

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return BadRequest(new OpenIdConnectResponse()
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }
    }
}
