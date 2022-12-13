namespace Jx.Gateway.Acme;

public static class AcmeExtensions
{
    public static IApplicationBuilder UseAcmeChallengeHandler(this IApplicationBuilder app)
    {
        app.Map($"/{AcmeChallengeHandler.AcmeHttp01PathPrefix}", appBuilder => {
            appBuilder.Run(AcmeChallengeHandler.HandleHttp01ChallengeRequest);
        });

        return app;
    }
}