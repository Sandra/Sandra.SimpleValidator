using Mindscape.Raygun4Net;
using Nancy.Bootstrapper;

namespace WebApplication1
{
public class RaygunNancyRegistration : IApplicationStartup
{
    public void Initialize(IPipelines pipelines)
    {
        var client = new RaygunClient();

        pipelines.OnError.AddItemToEndOfPipeline((context, exception) =>
        {
            client.Send(exception);

            return null;
        });
    }
}
}