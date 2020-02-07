using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;


namespace SecretSanta.Api.Controllers
{
	//https://localhost/api/Group
	[Route("api/[controller]")]
	[ApiController]
	public class GiftController : EntityController<Gift>
	{


		#region Constructor
		public GiftController(IGiftService giftService) : base(giftService)
		{

		}
		#endregion


	}
}