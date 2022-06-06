using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TimeCore.API.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IRequestModulService _requestModulService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IRequestModulService requestModulService)
            : base(options, logger, encoder, clock)
        {
            _requestModulService = requestModulService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Init
            AccountModel accountModel = null;

            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");
            try
            {
                //Init
                RequestModel requestModel = new RequestModel();

                //var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                //var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                //var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                //if (credentials.Length < 2)
                //    return AuthenticateResult.Fail("Invalid Parameter");
                //else if (credentials.Length >= 2)
                //{
                //    requestModel.requestUserName = credentials[0];
                //    if (string.IsNullOrEmpty(requestModel.requestUserName))
                //        return AuthenticateResult.Fail("Invalid Parameter");
                //    requestModel.requestPassword = credentials[1];
                //    if (string.IsNullOrEmpty(requestModel.requestPassword))
                //        return AuthenticateResult.Fail("Invalid Parameter");

                //}
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (!string.IsNullOrEmpty(authHeader.Parameter))
                {
                    requestModel.requestGUID = authHeader.Parameter;
                    if (string.IsNullOrEmpty(requestModel.requestGUID))
                        return AuthenticateResult.Fail("Invalid Parameter");
                    accountModel = await _requestModulService.GetAccountByGUIDAsync(requestModel).ConfigureAwait(false);
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (accountModel == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, accountModel.GUID.ToString()),
                new Claim(ClaimTypes.Name, accountModel.Username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            Context.User = new ClaimsPrincipal(new[] { identity });

            return AuthenticateResult.Success(ticket);
        }
    }
}
