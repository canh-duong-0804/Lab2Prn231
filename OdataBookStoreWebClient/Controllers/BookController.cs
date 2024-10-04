using Microsoft.AspNetCore.Mvc;
using OdataBookStore.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OdataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;
        public string ProductApiUrl = "";
        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7191/odata/Books";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);


            string strData = await response.Content.ReadAsStringAsync();

            using (JsonDocument jsonDocument = JsonDocument.Parse(strData))
            {
                var jsonElement = jsonDocument.RootElement.GetProperty("value");
                var items = JsonSerializer.Deserialize<List<Book>>(jsonElement.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(items);
            }
        }



        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + $"/{id}");
            var book = await response.Content.ReadFromJsonAsync<Book>();


            return View(book);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, book);
            response.EnsureSuccessStatusCode();


            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}({id})");
            response.EnsureSuccessStatusCode();

            var item = await response.Content.ReadFromJsonAsync<Book>();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{ProductApiUrl}({id})", book);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {

            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}" + $"/{id}");

            return RedirectToAction("Index");
        }
    }
}