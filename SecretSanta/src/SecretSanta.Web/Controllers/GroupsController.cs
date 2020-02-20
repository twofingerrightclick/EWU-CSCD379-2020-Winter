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
    public class GroupsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }

        public GroupsController(IHttpClientFactory clientFactory)
        {
            //HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            //Client = new GroupClient(httpClient);

            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            return View(await new GroupClient(httpClient).GetAllAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GroupInput groupInput)
        {
            ActionResult result = View(groupInput);

            if (ModelState.IsValid) {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");
                await new GroupClient(httpClient).PostAsync(groupInput);
                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            var client = new GroupClient(ClientFactory.CreateClient("SecretSantaApi"));

            ActionResult result = View(id);

            if (ModelState.IsValid) {
                var fetchedGroup = await client.GetAsync(id);
                result = View(fetchedGroup);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput groupInput)
        {
            Group updatedGroup = await new GroupClient(ClientFactory.CreateClient("SecretSantaApi")).PutAsync(id, groupInput);

            return RedirectToAction(nameof(Index));
        }
    }
}
