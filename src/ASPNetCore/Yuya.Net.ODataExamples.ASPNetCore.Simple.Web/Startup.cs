using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Models;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web
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
      services.AddDbContext<NorthwindDbContext>(m => m.UseSqlite("Data Source=App_Data/Northwind.sqlite"));

      services.AddMvcCore(options =>
      {
        options.EnableEndpointRouting = false; // TODO: Remove when OData does not causes exceptions anymore
      })
        .AddAuthorization()
        .AddDataAnnotations()
        .AddJsonFormatters(options => options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
        .AddCors()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddOData();

      //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc(b =>
      {
        b.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
        b.MapODataServiceRoute("odata", "odata", GetEdmModel());
      });
    }

    private static IEdmModel GetEdmModel()
    {
      ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
      builder.EntitySet<Category>("Categories");
      builder.EntitySet<Customer>("Customers");
      builder.EntitySet<Employee>("Employees");
      builder.EntitySet<EmployeeTerritory>("EmployeeTerritories");
      builder.EntitySet<Order>("Orders");
      builder.EntitySet<OrderDetail>("OrderDetails");
      builder.EntitySet<Product>("Products");
      builder.EntitySet<Shipper>("Shippers");
      builder.EntitySet<Supplier>("Suppliers");
      builder.EntitySet<Territory>("Territories");
      return builder.GetEdmModel();
    }
  }
}
