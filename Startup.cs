using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using H7A_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace H7A_Api
{
  public class Startup
  {
    private readonly IConfiguration _config;
    private readonly IHostEnvironment _env;

    public Startup(IConfiguration config, IHostEnvironment env)
    {
      _config = config;
      _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseMySql(_config.GetConnectionString("Default"));
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
