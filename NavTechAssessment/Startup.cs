using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NavTechAssesment.DataAccess.Entities;
using NavTechAssesment.DataAccess.EntityFramework;
using NavTechAssesment.DataAccess.Repositories;
using NavTechAssesment.DataAccess.Repositories.Interfaces;
using NavTechAssesment.Domain.Clients.DataProvider.Field;
using NavTechAssesment.Domain.Clients.DataProvider.Interface;
using NavTechAssesment.Domain.Services;
using NavTechAssesment.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavTechAssessment
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NavTechAssessment", Version = "v1" });
            });

            var connectionString = Configuration.GetConnectionString("DefaultDbConnectionString");
            services.AddDbContext<NavTechDbContext>(options => options
                .UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddHttpClient();
            services.AddTransient<INavTechConfigurationService, NavTechConfiguationService>();
            services.AddTransient<IRepository<ConfigEntity, Guid>, ConfigRepository<ConfigEntity, Guid>>();
            services.AddTransient<IRepository<ConfigEntityMetadata, Guid>, ConfigRepository<ConfigEntityMetadata, Guid>>();
            services.AddSingleton<IDataProvider, FieldDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NavTechAssessment v1"));
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
