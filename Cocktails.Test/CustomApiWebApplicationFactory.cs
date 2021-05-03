using System;
using Cocktails.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktails.Test
{
    public class CustomApiWebApplicationFactory : WebApplicationFactory<Cocktails.API.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services => {
                services.AddTransient<IBlobService, FileBlobService>();
            });
        }
    }
}
