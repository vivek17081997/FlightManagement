using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FlightManagementSystem.Controllers
{
	[ApiController]
	[Authorize]
	public class BaseController : ControllerBase
	{
		
	}
}
