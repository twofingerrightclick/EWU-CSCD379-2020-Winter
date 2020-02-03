using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SecretSanta.Api.Controllers
{
	//https://localhost/api/Author
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		#region Properties
		private IUserService UserService { get; }
		#endregion

		#region Constructor
		public UserController(IUserService userService)
		{
			UserService = userService ?? throw new System.ArgumentNullException(nameof(userService));
		}
		#endregion


		#region API Methods
		// GET: https://localhost/api/User
		[HttpGet]
		public async Task<IEnumerable<User>> Get()
		{
			return await UserService.FetchAllAsync();
		}

		// GET: https://localhost/api/User/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<User>> Get(int id)
		{
			return await UserService.FetchByIdAsync(id) is User user ? Ok(user) : (ActionResult<User>)NotFound();
		}

		// POST: https://localhost/api/User
		[HttpPost]
		public void Post([FromBody] User value)
		{

		}

		// PUT: https://localhost/api/User/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] User value)
		{
		}

		// DELETE: https://localhost/api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
		#endregion
	}
}