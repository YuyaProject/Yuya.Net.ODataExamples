using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
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

        foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
          outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
        foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
          inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
      })
        .AddAuthorization()
        .AddDataAnnotations()
        .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new CamelCaseNamingStrategy()
        })
        .AddJsonFormatters(options => {
          options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        })
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
      builder.EnableLowerCamelCase();
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
