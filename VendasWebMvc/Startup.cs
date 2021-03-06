﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using VendasWebMvc.Data;
using VendasWebMvc.Services;

namespace VendasWebMvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<VendasWebMvcContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("VendasWebMvcContext")));

            //registro do servico no sistema de injenção de independencia da aplicação
            services.AddScoped<ServicoPopularDados>();
            services.AddScoped<ServicoVendedor>();
            services.AddScoped<ServicoDepartamento>();
            services.AddScoped<ServicoRecordeVendas>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ServicoPopularDados servicoPopularDados)
        {
            if (env.IsDevelopment())
            {
                //Caso esteja no perfil de desenvolvimento, vou chamar o servicoSeeding, assim vou popular minha base de dados se ela não estiver populada ainda
                app.UseDeveloperExceptionPage();
                servicoPopularDados.PopularDados();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
