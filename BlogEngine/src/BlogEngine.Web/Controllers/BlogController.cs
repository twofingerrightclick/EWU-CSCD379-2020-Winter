using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlogEngine.Web.Api;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngine.Web.Controllers
{
    public class BlogController : Controller
    {
        public BlogController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new System.ArgumentNullException(nameof(clientFactory));
            }

            ClientFactory = clientFactory;
        }

        public IHttpClientFactory ClientFactory { get; }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("BlogApi");

            var client = new PostClient(httpClient);
            ICollection<Post> posts = await client.GetAllWithAuthorsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Post(int year, int month, int day, string slug)
        {
            HttpClient httpClient = ClientFactory.CreateClient("BlogApi");
            var client = new PostClient(httpClient);

            //var blogPost = await PostService.GetPostByPostedAndSlug(year, month, day, slug);
            //if (blogPost != null)
            //{
            //    return View(blogPost);
            //}
            return NotFound();
        }
    }
}
