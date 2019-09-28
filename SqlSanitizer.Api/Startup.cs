using System;
using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SqlSanitizer.Api
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
            var influxUrl = Environment.GetEnvironmentVariable("INFLUX_URL") ?? throw new ArgumentNullException("Environment.GetEnvironmentVariable(\"INFLUX_URL\")");
            var influxDb = Environment.GetEnvironmentVariable("INFLUX_DB") ?? throw new ArgumentNullException("Environment.GetEnvironmentVariable(\"INFLUX_DB\")");
            var influxUser = Environment.GetEnvironmentVariable("INFLUX_USER") ?? throw new ArgumentNullException("Environment.GetEnvironmentVariable(\"INFLUX_USER\")");
            var influxPwd = Environment.GetEnvironmentVariable("INFLUX_PWD") ?? throw new ArgumentNullException("Environment.GetEnvironmentVariable(\"INFLUX_PWD\")");
            
            var metrics = AppMetrics.CreateDefaultBuilder()
                .Report.ToInfluxDb(options =>
                {
                    options.InfluxDb.BaseUri = new Uri(influxUrl);
                    options.InfluxDb.Database = influxDb;
                    options.InfluxDb.UserName = influxUser;
                    options.InfluxDb.Password = influxPwd;
                    options.FlushInterval = TimeSpan.FromSeconds(10);
                })
                .Build();
            
            
            services.AddMetrics(metrics);
            services.AddMetricsTrackingMiddleware();
            services.AddMetricsReportingHostedService();
            services.AddHttpClient();
            services.AddControllers().AddNewtonsoftJson().AddMetrics();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(options =>
                options.WithOrigins(new[] {"http://localhost:4200", "https://sql.jhell.dev"}).AllowAnyMethod().AllowAnyHeader());
            
            app.UseMetricsAllMiddleware();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}