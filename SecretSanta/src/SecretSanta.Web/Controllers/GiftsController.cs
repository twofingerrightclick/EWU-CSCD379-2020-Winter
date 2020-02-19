using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }

        public GiftsController(IHttpClientFactory clientFactory)
        {
            //HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            //Client = new GiftClient(httpClient);

            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            return View(await new GiftClient(httpClient).GetAllAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GiftInput giftInput)
        {
            ActionResult result = View(giftInput);

            if (ModelState.IsValid) {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");
                await new GiftClient(httpClient).PostAsync(giftInput);
                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            var client = new GiftClient(ClientFactory.CreateClient("SecretSantaApi"));

            ActionResult result = View(id);

            if (ModelState.IsValid) {
                result = View(client.GetAsync(id));
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            Gift updatedGift = await new GiftClient(ClientFactory.CreateClient("SecretSantaApi")).PutAsync(id, giftInput);

            return RedirectToAction(nameof(Index));
        }
    }
}