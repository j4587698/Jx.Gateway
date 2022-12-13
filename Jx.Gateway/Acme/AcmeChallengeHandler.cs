using Furion.Logging;

namespace Jx.Gateway.Acme;

public class AcmeChallengeHandler
{
    public static readonly string AcmeHttp01PathPrefix =
        ACMESharp.Authorizations.Http01ChallengeValidationDetails.HttpPathPrefix.Trim('/');

    public static async Task HandleHttp01ChallengeRequest(HttpContext context)
    {
        var fullPath = $"{context.Request.PathBase}{context.Request.Path}".Trim('/');
        Log.Information($"Handling ACME challenge request: {fullPath}");
        await context.Response.WriteAsync("test");
    }
}