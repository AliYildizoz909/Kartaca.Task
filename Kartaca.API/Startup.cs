using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kartaca.API.Hubs;
using Kartaca.API.LogWatch;
using Kartaca.API.Middleware;
using Kartaca.API.Models;
using Microsoft.OpenApi.Models;

namespace Kartaca.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsAll", builder => builder.WithOrigins("http://localhost:5000","https://localhost:5001", "http://localhost:5001").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
            services.AddControllers();
            services.AddSingleton<IUserService,UserManager>();
            services.AddSignalR();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kartaca.Task API",
                    Description = "Buradan apimize isteklerimizi yapıp, aşağıdaki bağlantıdan bu istekleri izleyebiliriz.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ali Yıldızöz",
                        Email = "aliyildizoz909@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apiyi izlemek için tıklayınız.",
                        Url = new Uri("http://localhost:5000"),
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2"))
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kartaca.Task.Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseCors("CorsAll");
            app.UseLogMiddleware();
            app.UseHttpsRedirection();
            
            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RequestHub>("/requesthub");
            });
        }
    }
}
