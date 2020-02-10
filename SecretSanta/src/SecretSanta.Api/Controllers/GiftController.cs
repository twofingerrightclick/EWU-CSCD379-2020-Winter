using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;


namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : BaseApiController<GiftInput, Business.Dto.Gift>
    {
        public GiftController(IGiftService giftService)
            : base (giftService)
        { }
    }
}