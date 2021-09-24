using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CleanArchWeb.WebUI.Areas.Identity.IdentityHostingStartup))]
namespace CleanArchWeb.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}