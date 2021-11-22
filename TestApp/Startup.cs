//using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using Newtonsoft.Json.Serialization;
//using SampleWebApiAspNetCore.Helpers;
//using SampleWebApiAspNetCore.MappingProfiles;
//using SampleWebApiAspNetCore.Repositories;
//using SampleWebApiAspNetCore.Services;
//using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestApp
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
            //JWTToken

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = Configuration["Jwt:Issuer"],
         ValidAudience = Configuration["Jwt:Issuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
     };
 });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //To do
            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});


            services.AddOptions();
            //services.AddDbContext<FoodDbContext>(opt => opt.UseInMemoryDatabase("FoodDatabase"));
           // services.AddCustomCors("AllowAllOrigins");

           // services.AddSingleton<ISeedDataService, SeedDataService>();
           // services.AddScoped<IFoodRepository, FoodSqlRepository>();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            //services.AddControllers()
            //       .AddNewtonsoftJson(options =>
            //           options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
            //                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddVersioning();
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            //services.AddSwaggerGen();

            //services.AddAutoMapper(typeof(FoodMappings));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseHsts();
            //    app.AddProductionExceptionHandling(loggerFactory);
            //}
            app.UseAuthentication();
          //  app.UseMvc();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller}/{action=Index}/{id?}");
            });

        //    app.UseSpa(spa =>
        //    {
        //        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        //        // see https://go.microsoft.com/fwlink/?linkid=864501

        //        spa.Options.SourcePath = "ClientApp";

        //        if (env.IsDevelopment())
        //        {
        //            spa.UseAngularCliServer(npmScript: "start");
        //        }
        //    });
        //}
        ////  app.UseSwagger();
        // // app.UseSwaggerUI(
        //      options =>
        //      {
        //          foreach (var description in provider.ApiVersionDescriptions)
        //          {
        //              options.SwaggerEndpoint(
        //                  $"/swagger/{description.GroupName}/swagger.json",
        //                  description.GroupName.ToUpperInvariant());
        //          }
        //      });
    }
    }
}
