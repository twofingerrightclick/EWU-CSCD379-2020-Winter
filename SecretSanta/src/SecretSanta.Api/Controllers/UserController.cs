using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;


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