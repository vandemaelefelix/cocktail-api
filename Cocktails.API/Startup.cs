using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktails.API.Config;
using Cocktails.API.DataContext;
using Cocktails.API.Repositories;
using Cocktails.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Cocktails.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddDbContext<CocktailContext>();

            services.AddControllers();
            

            // services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cocktails.API", Version = "v1" });
            });

            services.AddTransient<ICocktailContext, CocktailContext>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<ICocktailRepository, CocktailRepository>();
            
            services.AddTransient<ICocktailService,CocktailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Docker")) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cocktails.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
