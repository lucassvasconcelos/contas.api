using Contas.Infra.Repositories;
using Contas.Infra.Repositories.Context;
using CoreBox.Extensions;
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

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
            => _configuration = configuration.GetEnvironmentConfiguration(env);

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
            contasContext.Migrate();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors();
            app.UseGlobalExceptionHandler();
            app.UseRouting();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
