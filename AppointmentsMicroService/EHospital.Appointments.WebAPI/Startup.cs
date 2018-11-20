using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using EHospital.Appointments.Model;
using EHospital.Appointments.BusinessLogic.Services;
using EHospital.Appointments.BusinessLogic.Contracts;
using EHospital.Appointments.Data;
using AutoMapper;
using EHospital.Appointments.WebApi;

namespace AppointmentsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "Appointments",
                });
            });
            services.AddScoped<IGenericRepository<Appointment>, GenericRepository<Appointment>>();
            services.AddScoped<IGenericRepository<AppointmentBill>, GenericRepository<AppointmentBill>>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IAppointmentBillService, AppointmentBillService>();
            services.AddDbContext<EHospitalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EHospitalDB")));
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfig>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
        }
    }
}