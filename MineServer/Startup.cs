﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MineServer.Models;
using static MineServer.Models.MineSweeperContext;

namespace MineServer
{
    //add
    // dotnet ef migrations add MyCommand1 -v
    //remove
    //dotnet ef migrations remove

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
            services.AddDbContext<MineSweeperContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            //string connectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb").ToString();

            //services.AddDbContext<MineSweeperContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<Player, IdentityRole>(options => { options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultProvider; }).AddEntityFrameworkStores<MineSweeperContext>().AddDefaultTokenProviders();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
