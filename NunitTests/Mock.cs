using Microsoft.AspNetCore.Hosting;

namespace NunitTests
{
    public class WebApplicationFactory<TEntryPoint> : Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            var environment = "Testing";

            if (!string.IsNullOrEmpty(environment))
            {
                builder.UseEnvironment(environment);
            }
        }
    }
    

}