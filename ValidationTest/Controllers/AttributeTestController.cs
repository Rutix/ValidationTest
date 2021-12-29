using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidationTest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AttributeTestController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post(Foobar foobar)
		{
			return Ok();
		}
	}
}
