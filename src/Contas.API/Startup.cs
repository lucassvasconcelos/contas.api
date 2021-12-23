using System.Text.Json.Serialization;
using Contas.Commands;
using Contas.DomainServices;
using Contas.Events;
using Contas.Infra.Repositories;
using Contas.Infra.Repositories.Context;
using Contas.Queries;
using CoreBox.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Contas.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public static readonly ILoggerFactory _loggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
            => _configuration = configuration.GetEnvironmentConfiguration(env);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opts
                => opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

            services.AddSwaggerGen(sw
                => sw.SwaggerDoc("v1", new OpenApiInfo { Title = "Contas", Version = "v1" }));

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins(_configuration["AllowedOrigins"].Split(';'));
                    policy.SetIsOriginAllowedToAllowWildcardSubdomains();
                    policy.AllowCredentials();
                });
            });

            services.AddResponseCompression();
            services.AddCommands();
            services.AddDomainServices();
            services.AddEvents();
            services.AddQueries();
            services.AddContext(_configuration, _loggerFactory);
            services.AddAutoMapper(typeof(Startup));
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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contas v1"));
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
