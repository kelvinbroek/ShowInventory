using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TvShow.Inventory.Application.Contracts.Repositories;
using TvShow.Inventory.Application.Contracts.Services;
using TvShow.Inventory.Application.Profiles;
using TvShow.Inventory.Infrastructure;
using TvShow.Inventory.Infrastructure.Services;
using TvShow.Inventory.Infrastructure.Settings;
using TvShow.Inventory.Persistence;
using TvShow.Inventory.Persistence.Repositories;

namespace TvShow.Inventory.Service
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TvShow.Inventory.Service", Version = "v1" });
            });

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TvShowDatabase")));

            services.AddScoped<IInventoryRepository, InventoryRepository>();

            services.AddHttpClient<ITvMazeService, TvMazeService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["TvMazeBaseUrl"]);
            });

            services.AddAutoMapper(typeof(CommandToDomainProfile));
            services.AddAutoMapper(typeof(DomainToDtoProfile));
            services.AddAutoMapper(typeof(ResponseToDtoProfile));

            services.AddOptions();
            services.Configure<TvMazeSettings>(Configuration.GetSection("TvMazeSettings"));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, InventoryDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvShow.Inventory.Service v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbContext.Database.Migrate();
        }
    }
}
