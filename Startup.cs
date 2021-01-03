using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonHotelAPI.Filters;
using LandonHotelAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using LandonHotelAPI.Services;
using AutoMapper;
using LandonHotelAPI.Infrastructure;

namespace LandonHotelAPI
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
            
            services.AddScoped<IRoomService, DefaultRoomService>();

            //Use In memory Database for Development
            //TODO swap out for REal Database
            services.AddDbContext<HotelAPIDbContext>(
                options =>
                options.UseInMemoryDatabase("landonDB"));


            services.Configure<HotelInfo>(
                Configuration.GetSection("Info"));

            services.AddMvc(options =>
            {
                options.Filters
                .Add<JsonExceptionFilter>();
                options.Filters
                .Add<RequireHttpsOrCloseAttribute>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(options =>
            { 
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader =
                new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector
                = new CurrentImplementationApiVersionSelector
                (options);
               });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                    policy => policy
                    .AllowAnyOrigin());
            });

            services.AddAutoMapper(
                options => options.AddProfile<MappingProfile>());
        }

        private void DefaultRoomService<T>()
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUi3WithApiExplorer(options =>
                {
                    options.GeneratorSettings.DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowMyApp");
            app.UseMvc();
        }
    }
}
