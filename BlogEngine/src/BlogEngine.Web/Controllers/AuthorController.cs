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
        public ActionResult Index()
        {
            return View();
            //HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

            //var client = new AuthorClient(httpClient);
            //ICollection<Author> authors = await client.GetAllAsync();
            //return View(authors);
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

            var client = new AuthorClient(httpClient);
            Author author = await client.GetAsync(id);
            return View(author);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AuthorInput authorInput)
        {
            ActionResult result = View(authorInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

                var client = new AuthorClient(httpClient);
                var createdAuthor = await client.PostAsync(authorInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

            var client = new AuthorClient(httpClient);
            var fetchedAuthor = await client.GetAsync(id);

            return View(fetchedAuthor);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, AuthorInput authorInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

            var client = new AuthorClient(httpClient);
            var updatedAuthor = await client.PutAsync(id, authorInput);

            return RedirectToAction(nameof(Index));
        }
    }
}