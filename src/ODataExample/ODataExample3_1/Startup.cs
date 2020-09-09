using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using NorthwindEFCore;
using NorthwindEFCore.Entities;

namespace ODataExample3_1
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

			services.AddDbContext<NorthwindDbContext>(o => o.UseSqlite("Data Source=NorthwindDB.sqlite"));

			services.AddOData();


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			});

			services.AddMvcCore(options =>
			{
				foreach (var outputFormatter in options.OutputFormatters.OfType<OutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
				{
					outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
				}

				foreach (var inputFormatter in options.InputFormatters.OfType<InputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
				{
					inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
				}
			})
			.AddApiExplorer();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.Select().Filter().Count().Expand().MaxTop(100).OrderBy().SkipToken();
				endpoints.MapODataRoute("odata", "odata", GetEdmModel());

				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

		}

		IEdmModel GetEdmModel()
		{
			var odataBuilder = new ODataConventionModelBuilder();

			odataBuilder.EntitySet<Category>("Categories");
			odataBuilder.EntitySet<Customer>("Customers");
			odataBuilder.EntitySet<Employee>("Employees");
			odataBuilder.EntitySet<Order>("Orders");
			odataBuilder.EntitySet<OrderDetail>("OrderDetails")
				.EntityType
				.HasKey(x=>new { x.OrderId, x.ProductId });
			odataBuilder.EntitySet<Product>("Products");
			odataBuilder.EntitySet<Region>("Regions");
			odataBuilder.EntitySet<Shipper>("Shippers");
			odataBuilder.EntitySet<Supplier>("Suppliers");
			odataBuilder.EntitySet<Territory>("Territories");

			return odataBuilder.GetEdmModel();
		}
	}
}
