using System;
using System.Linq;
using AutoMapper;
using FluentValidation.AspNetCore;
using GameAPI.Data.Context;
using GameAPI.Data.Mappers.Profiles;
using GameAPI.Data.OData.Configuration;
using GameAPI.Data.Request.PlayerRequest;
using GameAPI.ExceptionHandler;
using GameAPI.Filters;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GameAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(typeof(ValidatorActionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<PlayerValidator>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddAuthorization();

            string connection = Configuration["SqliteConnection:SqliteConnectionString"];
            services.AddDbContext<DataContext>(option =>
            {

                option.UseSqlite(connection);
            });

            services.AddOData();

            var assembly = AppDomain.CurrentDomain.Load("GameAPI.Data");
            services.AddMediatR(assembly);
            services.AddAutoMapper(typeof(GameProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseCors("MyPolicy");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseGlobalExceptionHandler(loggerFactory);

            app.UseMvc(builder =>
            {
                builder.Select().Expand().Filter().OrderBy().Count().MaxTop(100);
                builder.MapODataServiceRoute("odata", "odata", ODataConfiguration.GetEdmModel());
                builder.EnableDependencyInjection();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }


        }
    }
}
