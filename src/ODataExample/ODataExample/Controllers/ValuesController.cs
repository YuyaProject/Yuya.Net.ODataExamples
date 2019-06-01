using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ODataExample.Controllers
{
	/// <summary>
	/// Values Example Controller
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		/// <summary>
		/// Gets the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		/// <summary>
		/// Posts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		/// <summary>
		/// Puts the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="value">The value.</param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
