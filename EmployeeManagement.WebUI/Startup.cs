using AutoMapper;
using EmployeeManagement.Domain.Interfaces.DomainEvents;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Repositories.Common;
using EmployeeManagement.Domain.Interfaces.Services;
using EmployeeManagement.Domain.Interfaces.Services.Common;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Infrastructure.Repositories.Common;
using EmployeeManagement.Services.DomainEvents;
using EmployeeManagement.Services.Services;
using EmployeeManagement.Services.Services.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EmployeeManagement.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmployeeManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("StagingConnectionString")));
            services.AddControllersWithViews();
            services.AddMvc();

            RegisterRepositories(services);
            RegisterMappers(services);
            RegisterCoreServices(services);
            RegisterDomainEventHandlers(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ConfigurationMigration(serviceProvider);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #region Helper for ConfigureServices
        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        }
        private void RegisterMappers(IServiceCollection services)
        {
            var configurationMappings = Services.Mapper.MapperConfigurator.ConfigureMappings(Configuration);
            services.AddSingleton<IMapper>(new Mapper(configurationMappings));
        }
        private void RegisterCoreServices(IServiceCollection services)
        {
            services.AddScoped<IDropdownListService, DropdownListService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }
        private void RegisterDomainEventHandlers(IServiceCollection services)
        {
            services.AddScoped<IDomainEventManager, DomainEventManager>();
            services.AddMediatR(typeof(EmployeeManagement.Services.DomainEvents.DomainEventManager).Assembly);
        }
        private void ConfigurationMigration(IServiceProvider serviceProvider)
        {
            var employeeManagementDbContext = (EmployeeManagementContext)serviceProvider.GetService(typeof(EmployeeManagementContext));
            employeeManagementDbContext.GetService<IMigrator>().Migrate(Configuration["EmployeeManagementDbContextTargetMigration"]);

        }
        #endregion
    }
}
