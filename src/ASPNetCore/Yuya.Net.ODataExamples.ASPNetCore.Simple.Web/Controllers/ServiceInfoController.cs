using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Yuya.Net.ODataExamples.ASPNetCore.Simple.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ServiceInfoController : ControllerBase
  {
    // GET api/values
    [HttpGet]
    public ActionResult<Dictionary<string, object>> Get()
    {
      return new Dictionary<string, object> {
                { "ServerDateTime", DateTimeOffset.Now }
            };
    }
  }
}
