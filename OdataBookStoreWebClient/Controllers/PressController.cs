using Microsoft.AspNetCore.Mvc;
using OdataBookStore.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace OdataBookStoreWebClient.Controllers
{
    public class PressController : Controller
    {
        private readonly HttpClient client = null;
        public string ProductApiUrl = "";

        public PressController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7191/odata/Presses";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JsonObject.Parse(strData);
            var list = temp.value;
            List<Press> items = ((JsonArray)temp.value).Select(x => new Press
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
            }).ToList();
            return View(items);
        }
    }
}