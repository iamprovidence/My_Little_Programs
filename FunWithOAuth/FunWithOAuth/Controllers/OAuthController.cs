using FunWithOAuth.OAuth;
using Microsoft.AspNetCore.Mvc;

namespace FunWithOAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
    {
        private readonly OAuthConnectionServiceFactory _oAuthConnectionServiceFactory;

        public OAuthController(OAuthConnectionServiceFactory oAuthConnectionServiceFactory)
        {
            _oAuthConnectionServiceFactory = oAuthConnectionServiceFactory;
        }

        // single endpoint to return unified response for different apps
        [Route("oauth/connect/{appType}")]
        public async Task<IActionResult> OAuthConnection(
            [FromRoute] OAuthAppType appType,
            [FromQuery] string code,
            [FromQuery] string state,
            [FromQuery] string error,
            [FromQuery] string error_description)
        {
            if (string.IsNullOrEmpty(error))
            {
                var oAuthRedirectUri = $"{Request.Scheme}://{Request.Host}/{Request.Path}";

                var token = await _oAuthConnectionServiceFactory
                    .Create(appType)
                    .RequestAccessToken(code, oAuthRedirectUri);

                /*
                // redirect to client url with error or success
                if (token is not null)
                {
                    if (!string.IsNullOrEmpty(oauthResponse.Token.LastError))
                    {
                        return Redirect($"/oauth/{oauthType}/error/?error={oauthResponse.Token.LastError}&description={oauthResponse.Token.LastErrorDescription}");
                    }
                    else
                    {
                        return Redirect($"/oauth/{oauthType}/success");
                    }
                }
                */
            }

            // show error message
            // somewhere on the client next endpoint should exist
            return Redirect($"/oauth/{appType}/error/?error={error}&description={error_description}");
        }

    }
}