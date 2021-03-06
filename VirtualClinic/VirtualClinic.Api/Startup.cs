using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using VirtualClinic.Domain.Interfaces;
using VirtualClinic.Domain.Repositories;
using VirtualClinic.DataModel;
using Okta.AspNetCore;


namespace VirtualClinic.Api
{
    public class Startup
    {
        /// <summary>
        /// Read only string to name the CORS policy
        /// </summary>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0"/>
        readonly private static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped <IClinicRepository, ClinicRepository>();

            services.AddControllers(options =>
            {
                // make asp.net core forget about text/plain so swagger ui uses json as the default
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                // teach asp.net core to be able to serialize & deserialize XML
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                options.ReturnHttpNotAcceptable = true;
            });

            services.AddDbContext<ClinicDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VirtualClinic.Api", Version = "v1" });
            });

            services.AddCors(options =>
            {
                
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {                               
                                      builder.WithOrigins("https://localhost",
                                            "http://dev-7862904.okta.com",
                                            "https://virtual-clinic.azurewebsites.net",
                                            "http://localhost:5000",
                                            "http://localhost:5001",
                                            "http://localhost:4200",
                                            "http://localhost:4200/",
                                            "http://localhost:44317");
                                      builder.AllowAnyHeader();
                                  });
            });





            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-7862904.okta.com/oauth2/default";
                    options.Audience = "api://default";
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                }).AddOktaMvc( new OktaMvcOptions
                    {
                        OktaDomain = "https://dev-7862904.okta.com/oauth2/default",
                        ClientId = "0oa2rljvgj0RRay1e5d6",
                        ClientSecret = "aIZI2AeQqruJ2QfHLzvsZdDwuodPgKawGvveJgwI",
                    }
                );

                


            services.AddScoped<IClinicRepository, ClinicRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtualClinic.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
