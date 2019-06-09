using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using NorthwindEFCore;
using NorthwindEFCore.Entities;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ODataExample
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
			services.AddCors();

			services.AddDbContext<NorthwindDbContext>(opt =>
				opt.UseSqlite("Data Source=NorthwindDB.sqlite"));

			services.AddOData();

			services.AddMvc(options =>
				{
					options.EnableEndpointRouting = false;
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				;

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "OData Example",
					Description = "A OData example ASP.NET Core Web API",
					TermsOfService = "None",
					Contact = new Contact
					{
						Name = "Alper Konuralp",
						Email = "alper.konuralp@d-teknoloji.com.tr",
						Url = "https://twitter.com/alperkonuralp"
					},
					License = new License
					{
						Name = "Use under LICX",
						Url = "https://example.com/license"
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			}
			else
			{
				app.UseHsts();
			}

			app.UseMvc(b =>
			{
				b.Select().Expand().Filter().OrderBy().Count().MaxTop(100);
				b.MapODataServiceRoute("odata", "odata", GetEdmModel(app.ApplicationServices));

				b.MapRoute(
									name: "default",
									template: "{controller=Home}/{action=Index}/{id?}");

				b.EnableDependencyInjection();
			});

			if (env.IsDevelopment())
			{
				// Enable middleware to serve generated Swagger as a JSON endpoint.
				app.UseSwagger();

				// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
				// specifying the Swagger JSON endpoint.
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData Example V1");
				});
			}
		}

		private static IEdmModel GetEdmModel(IServiceProvider services)
		{
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder(services);
			//builder.EntitySet<Book>("Books");

			builder.EntitySet<Category>("Categories");
			builder.EntitySet<Customer>("Customers");
			builder.EntitySet<Employee>("Employees");

			builder.EntityType<EmployeeTerritory>().HasKey(m => new { m.EmployeeId, m.TerritoryId });

			builder.EntitySet<EmployeeTerritory>("EmployeeTerritories");

			builder.EntitySet<Order>("Orders");
			builder.EntityType<OrderDetail>().HasKey(m => new { m.OrderId, m.ProductId });
			builder.EntitySet<OrderDetail>("OrderDetails");

			builder.EntitySet<Product>("Products");
			builder.EntitySet<Region>("Regions");
			builder.EntitySet<Shipper>("Shippers");
			builder.EntitySet<Supplier>("Suppliers");
			builder.EntitySet<Territory>("Territories");

			return builder.GetEdmModel();
		}
	}
}