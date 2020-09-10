using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Text;

namespace ODataExample
{
	/// <summary>
	/// Http Request Extension Methods
	/// </summary>
	internal static class HttpRequestExtensions
	{
		/// <summary>
		/// Reads the entity from body.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		internal static T ReadEntityFromBody<T>(this HttpRequest request)
		{
			T entity = default(T);
			using (StreamReader sw = new StreamReader(request.Body, Encoding.UTF8))
			{
				entity = JsonConvert.DeserializeObject<T>(sw.ReadToEnd());
			}
			return entity;
		}

		/// <summary>
		/// Reads the entity from body.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns></returns>
		internal static JObject ReadEntityFromBody(this HttpRequest request)
		{
			var entity = default(JObject);
			using (StreamReader sw = new StreamReader(request.Body, Encoding.UTF8))
			{
				entity = JObject.Parse(sw.ReadToEnd());
			}
			return entity;
		}
	}
}