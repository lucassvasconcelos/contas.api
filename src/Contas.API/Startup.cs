using Contas.Infra.Repositories;
using Contas.Infra.Repositories.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Contas.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
            => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddContext(_configuration);
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            ContasContext contasContext
        )
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            contasContext.Migrate();
            app.UseResponseCompression();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
