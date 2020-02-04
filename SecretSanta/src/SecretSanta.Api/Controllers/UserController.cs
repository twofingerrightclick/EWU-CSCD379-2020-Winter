using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SecretSanta.Api.Controllers
{
	//https://localhost/api/User
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : EntityController<User>
	{
		

		#region Constructor
		public UserController(IUserService userService): base (userService)
		{

		}
		#endregion


	}
}