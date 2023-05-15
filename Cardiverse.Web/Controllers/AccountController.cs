using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Cardiverse.Web.Controllers;

[Route("account")]
public class AccountController : Controller
{
    [Route("google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [Route("google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

        //var photoClaim = result.Principal
        //    .FindFirst("picture");

        //claims.Append(new
        //{
        //    photoClaim.Issuer,
        //    photoClaim.OriginalIssuer,
        //    photoClaim.Type,
        //    photoClaim.Value
        //});

        return Json(claims);
    }
}
