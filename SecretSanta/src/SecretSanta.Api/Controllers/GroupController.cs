using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;


namespace SecretSanta.Api.Controllers
{
	//https://localhost/api/Group
	[Route("api/[controller]")]
	[ApiController]
	public class GroupController : EntityController<Group>
	{


		#region Constructor
		public GroupController(IGroupService groupService) : base(groupService)
		{

		}
		#endregion


	}
}