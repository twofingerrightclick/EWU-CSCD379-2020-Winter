using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }

        public UsersController(IHttpClientFactory clientFactory)
        {
            //HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            //Client = new UserClient(httpClient);

            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            return View(await new UserClient(httpClient).GetAllAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserInput userInput)
        {
            ActionResult result = View(userInput);

            if (ModelState.IsValid) {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");
                await new UserClient(httpClient).PostAsync(userInput);
                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            var client = new UserClient(ClientFactory.CreateClient("SecretSantaApi"));

            ActionResult result = View(id);

            if (ModelState.IsValid) {
                
                var fetchedUser=await client.GetAsync(id);
                result = View(fetchedUser);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserInput userInput)
        {
            User updatedUser = await new UserClient(ClientFactory.CreateClient("SecretSantaApi")).PutAsync(id, userInput);

            return RedirectToAction(nameof(Index));
        }
    }
}
