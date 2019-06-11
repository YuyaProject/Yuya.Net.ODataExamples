using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ODataExample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
						 .ConfigureKestrel(options =>
						 {
							 options.ListenLocalhost(5001, listenOptions =>
							 {
								 listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
								 listenOptions.UseHttps();
							 });
						 })
						.UseStartup<Startup>();
	}
}
