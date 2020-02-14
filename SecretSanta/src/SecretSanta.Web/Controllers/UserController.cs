using System.Net.Http;
using System.Threading.Tasks;
using SecretSanta.Web.Api;
using Microsoft.AspNetCore.Mvc;


namespace SecretSanta.Web.Controllers
{
    public class UserController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }

        public UserController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null) {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }
            ClientFactory = clientFactory;
        }

        public async Task<ActionResult> Index() => View(await new UserClient(ClientFactory.CreateClient("SecretSanta")).GetAllAsync());
    }
}