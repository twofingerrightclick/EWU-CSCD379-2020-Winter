using BlogEngine.Web.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogEngine.Web.Controllers
{
    public class AuthorController : Controller
    {
        public AuthorController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }

            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }

        // GET: Author
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

            var client = new AuthorClient(httpClient);
            ICollection<Author> authors = await client.GetAllAsync();
            return View(authors);
        }
    }
}